using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace Eco.DevView.Web
{
    internal class Startup
    {
        private string _webRoot;

        internal Startup(string webRoot)
        {
            _webRoot = webRoot;
        }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();

            // SignalR
            app.MapSignalR();

            //// Make sure we're shipping our own provider
            //var dependencyResolver = Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver;
            //dependencyResolver.Register(typeof(Microsoft.AspNet.SignalR.Hubs.IHubDescriptorProvider), () => new EcoHubDescriptorProvider(dependencyResolver));

            // Web API
            app.UseWebApi(configuration);

            // Static files
            var staticFilesDirectory = _webRoot.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (!Directory.Exists(staticFilesDirectory))
                throw new InvalidOperationException($"Could not find webroot directory at {staticFilesDirectory}. Please check your installation.");

            var fileSystem = new PhysicalFileSystem(staticFilesDirectory);
            var requestPath = PathString.Empty;

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new List<string> { "index.html" },
                FileSystem = fileSystem,
                RequestPath = requestPath
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = fileSystem,
                RequestPath = requestPath,
                ServeUnknownFileTypes = false
            });

            app.Run(async (context) =>
            {
                var response = context.Response;
                response.StatusCode = 404;
                response.ReasonPhrase = "Not Found";
                await response.WriteAsync(@"<!DOCTYPE html><html><head><title>404 - Not Found</title></head><body><h1>404 - Not Found</h1></body></html>");
            });
        }
    }
}
