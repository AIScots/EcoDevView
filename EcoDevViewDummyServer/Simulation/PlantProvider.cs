using System.Collections.Generic;

namespace Eco.DevView.DummyServer
{
    class PlantProvider : GenericProvider<Plant>, IPlantProvider
    {
        public PlantProvider()
            : base((id, random) => new Plant(id, random))
        {
            _log = NLog.LogManager.GetCurrentClassLogger();

            for (int i = 0; i < 100; ++i)
                CreateObject();
        }

        public IPlant GetPlant(int id) => GetObject(id);

        public ICollection<IPlant> GetPlants() => new List<IPlant>(GetObjects());

        public ICollection<IPlant> GetUpdatedPlants() => new List<IPlant>(GetUpdatedObjects());
    }
}
