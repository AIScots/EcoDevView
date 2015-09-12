using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Eco.DevView.Web
{
    /// <summary>
    /// The hub that serves as gateway to SignalR and connection to the clients.
    /// </summary>
    /// <remarks>
    /// It would be nice to use <see cref="Hub{T}"/> instead, but that would require us to make all consequent communication
    /// with the clients public. That's something we want to avoid; all communication with clients should strictly go over
    /// Eco.DevView, not other code.
    /// </remarks>
    public class MapHub : Hub
    {
        public override async Task OnConnected()
        {
            await base.OnConnected();

            // Send all animals to the client
            var ws = WebServer.Instance;
            ws.SendAnimalUpdates(Clients.Caller);
            ws.SendPlantUpdates(Clients.Caller);
        }

        public void RenameAnimal(int id, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("name may not be null", nameof(newName));

            WebServer.Instance.AnimalProvider.SetAnimalName(id, newName);
        }

        public void SetAnimalHealth(int id, float newHealth)
        {
            if (newHealth < 0 || newHealth > 1)
                throw new ArgumentOutOfRangeException(nameof(newHealth), newHealth, "must be between 0 and 1, inclusive");

            WebServer.Instance.AnimalProvider.SetAnimalHealth(id, newHealth);
        }
    }
}
