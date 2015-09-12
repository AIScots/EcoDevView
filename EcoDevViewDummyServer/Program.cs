using System;
using System.IO;
using System.Threading.Tasks;

namespace Eco.DevView.DummyServer
{
    class Program
    {
        const int Width = 512;
        const int Height = 512;
        static bool stopped = false;
        static NLog.ILogger _log = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string url = args.Length > 0 ? args[0] : "http://localhost:7777/";
            string webRoot = args.Length > 1 ? args[1] : Path.Combine(Environment.CurrentDirectory, "webroot");

            var bp = new BlockProvider(Width, Height);
            var ap = new AnimalProvider();
            var pp = new PlantProvider();

            using (var server = new WebServer(bp, ap, pp, url, webRoot))
            using (var task = RunServer(server, () => { ap.Update(); pp.Update(); }))
            {
                _log.Info("Server started on {0}.", url);
                _log.Info("WebRoot: {0}", webRoot);
                Console.WriteLine("Press any key to stop.", url, webRoot);
                Console.ReadLine();
                stopped = true;
                task.Wait();
            }
        }

        static async Task RunServer(WebServer server, Action update)
        {
            try
            {
                while (!stopped)
                {
                    update();
                    server.SendUpdates();
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex, "Uncaught exception in server thread! {0}", ex.ToString());
            }
        }
    }
}
