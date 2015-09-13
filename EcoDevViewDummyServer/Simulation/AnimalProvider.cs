using System.Collections.Generic;

namespace Eco.DevView.DummyServer
{
    class AnimalProvider : GenericProvider<Animal>, IAnimalProvider
    {
        public AnimalProvider()
            : base((id, random) => new Animal(id, random))
        {
            _log = NLog.LogManager.GetCurrentClassLogger();

            for (int i = 0; i < 20; ++i)
                CreateObject();
        }

        public IAnimal GetAnimal(int id) => GetObject(id);

        public ICollection<IAnimal> GetAnimals() => new List<IAnimal>(GetObjects());

        public ICollection<IAnimal> GetUpdatedAnimals() => new List<IAnimal>(GetUpdatedObjects());

        public void SetAnimalHealth(int animalId, float health)
        {
            var animal = GetObject(animalId);
            animal.Health = health;
            AddToUpdatedList(animal);
        }

        public void SetAnimalName(int animalId, string name)
        {
            var animal = GetObject(animalId);
            animal.Name = name;
            AddToUpdatedList(animal);
        }
    }
}
