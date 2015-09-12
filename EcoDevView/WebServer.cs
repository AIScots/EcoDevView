using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Eco.DevView
{
    public class WebServer : IDisposable
    {
        #region Constants
        /// <summary>
        /// The size of one image tile. Has to be a multiple of 2.
        /// </summary>
        // TODO: Have this configurable, sort of
        public const int TileSize = 32;
        #endregion

        #region Properties
        /// <summary>
        /// Defines the minimum zoom level required
        /// </summary>
        public int MinimumZoomLevel { get; }

        /// <summary>
        /// The source for blocks for this server.
        /// </summary>
        public IBlockProvider BlockProvider { get; set; }

        /// <summary>
        /// The source for animals for this server.
        /// </summary>
        public IAnimalProvider AnimalProvider { get; set; }

        /// <summary>
        /// The source for plants for this server.
        /// </summary>
        public IPlantProvider PlantProvider { get; set; }

        /// <summary>
        /// The renderer for this server.
        /// </summary>
        internal ITileRenderer TileRenderer { get; set; } = new DefaultTileRenderer();

        /// <summary>
        /// The current instance of <see cref="WebServer"/>
        /// </summary>
        public static WebServer Instance { get; private set; }
        #endregion

        #region Private Fields
        /// <summary>
        /// Defines the running web server, if any
        /// </summary>
        private IDisposable _webServer;

        /// <summary>
        /// The context for everyone currently listening to maps.
        /// </summary>
        IHubContext _mapHubContext;
        #endregion

        /// <summary>
        /// Creates a new <see cref="Web"/> object using a given provider and listen address.
        /// </summary>
        /// <param name="blockProvider">Provider for blocks to use.</param>
        /// <param name="url">Address that the server listens on.</param>
        public WebServer(IBlockProvider blockProvider, IAnimalProvider animalProvider, IPlantProvider plantProvider, string url, string webRoot)
        {
            if (blockProvider == null)
                throw new ArgumentNullException(nameof(blockProvider));
            if (animalProvider == null)
                throw new ArgumentNullException(nameof(animalProvider));
            if (plantProvider == null)
                throw new ArgumentNullException(nameof(plantProvider));

            if (Instance != null)
                throw new InvalidOperationException(nameof(WebServer) + " is a singleton class and may not be instantiated multiple times.");

            Instance = this;

            // Set the providers
            BlockProvider = blockProvider;
            AnimalProvider = animalProvider;
            PlantProvider = plantProvider;

            try
            {
                // Start the server (/Katana/OWIN pipeline)
                var options = new StartOptions();
                options.Urls.Add(url);
                _webServer = WebApp.Start(url, (app) => new Web.Startup(webRoot).Configuration(app));
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                // Because those exceptions usually are not helpful ("an invocation target has something something"), we'll rather throw the inner exception
                if (ex.InnerException == null)
                    throw;

                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            }

            // Get the hub contexts
            _mapHubContext = GlobalHost.ConnectionManager.GetHubContext<Web.MapHub>();
        }

        #region SignalR
        internal void SendAnimalUpdates(dynamic client, IEnumerable<IAnimal> animals = null)
        {
            client.UpdateAnimals((animals ?? AnimalProvider.GetAnimals()).Select(a => new Dto.Animal(a)));
        }

        internal void SendPlantUpdates(dynamic client, IEnumerable<IPlant> plants = null)
        {
            client.UpdatePlants((plants ?? PlantProvider.GetPlants()).Select(p => new Dto.Plant(p)));
        }
        #endregion

        /// <summary>
        /// Sends all pending updates to the clients.
        /// </summary>
        public void SendUpdates()
        {
            var animals = AnimalProvider.GetUpdatedAnimals();
            if (animals.Count > 0)
                SendAnimalUpdates(_mapHubContext.Clients.All, animals);

            var plants = PlantProvider.GetUpdatedPlants();
            if (plants.Count > 0)
                SendPlantUpdates(_mapHubContext.Clients.All, plants);
        }

        /// <summary>
        /// Draws the tile at zoom level <paramref name="zoomLevel"/> with the tile coordinate <paramref name="tileX"/>/<paramref name="tileZ"/>.
        /// </summary>
        /// <param name="zoomLevel">Zoom level of the tile, with 0 being the closest zoom available.</param>
        /// <param name="tileX">X-coordinate of the tile.</param>
        /// <param name="tileZ">Z-coordinate of the tile.</param>
        internal void DrawMap(int zoomLevel, int tileX, int tileZ)
            {
                // Closest zoom means at the moment 1px = 1 block
                if (zoomLevel == 0)
                {
                    // Create a new bitmap
                    int tileSizeX = Math.Min(TileSize, BlockProvider.DimensionX - tileX * TileSize);
                    int tileSizeZ = Math.Min(TileSize, BlockProvider.DimensionZ - tileZ * TileSize);

                    if (tileSizeX <= 0 || tileSizeZ <= 0)
                        throw new ArgumentException("Tile is out of bounds");

                    // Create the bitmap
                    using (var bmp = new Bitmap(TileSize, TileSize))
                    {
                        for (int x = 0; x < tileSizeX; ++x)
                            for (int z = 0; z < tileSizeZ; ++z)
                            {
                                var tile = BlockProvider.GetTopBlock(x + tileX * TileSize, z + tileZ * TileSize);
                                bmp.SetPixel(x, z, TileRenderer.GetColor(tile));
                            }

                        TileRenderer.SaveBitmap(GetTileName(zoomLevel, tileX, tileZ), bmp);
                    }
                }
                else
                {
                    List<Image> bitmaps = new List<Image>();

                    using (var bitmap = new Bitmap(TileSize, TileSize))
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        for (int dx = 0; dx <= 1; ++dx)
                            for (int dz = 0; dz <= 1; ++dz)
                            {
                                var previousBitmap = TileRenderer.LoadBitmap(GetTileName(zoomLevel, tileX * 2 + dx, tileZ * 2 + dz));
                                if (previousBitmap != null)
                                {
                                    bitmaps.Add(previousBitmap);
                                    graphics.DrawImage(previousBitmap, 0, 0);
                                }
                            }

                        TileRenderer.SaveBitmap(GetTileName(zoomLevel, tileX, tileZ), bitmap);
                    }

                    bitmaps.ForEach(i => i.Dispose());
                }
            }

        /// <summary>
        /// Returns the file name of a tile.
        /// </summary>
        /// <param name="zoomLevel">Zoom level of the tile, from 0 up to <see cref="MinimumZoomLevel"/></param>
        /// <param name="tileX">X coordinate of the tile</param>
        /// <param name="tileZ">Z coordinate of the tile</param>
        /// <returns></returns>
        internal string GetTileName(int zoomLevel, int tileX, int tileZ) => $"tile_{zoomLevel}_{tileX}_{tileZ}.png";

        /// <summary>
        /// Disposes the server object.
        /// </summary>
        public void Dispose()
        {
            _webServer?.Dispose();
            _webServer = null;

            Instance = null;
        }
    }
}
