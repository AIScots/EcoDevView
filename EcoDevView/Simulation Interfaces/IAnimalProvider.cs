using System.Collections.Generic;

namespace Eco.DevView
{
    public interface IAnimalProvider
    {
        /// <summary>
        /// Returns the animal with the unqiue identifier <paramref name="id"/>
        /// </summary>
        /// <param name="id">Unique identifier of the <see cref="IAnimal"/></param>
        /// <returns><see cref="IAnimal"/> if said animal exists, <c>null</c> otherwise.</returns>
        IAnimal GetAnimal(int id);

        /// <summary>
        /// Returns all animals that currently exist.
        /// </summary>
        /// <returns></returns>
        ICollection<IAnimal> GetAnimals();

        /// <summary>
        /// Returns all animals that have changed any attribute since the last time this function was called.
        /// </summary>
        /// <returns></returns>
        ICollection<IAnimal> GetUpdatedAnimals();

        /// <summary>
        /// Sets the health of the animal with id <paramref name="animalId"/> to <paramref name="health"/>.
        /// </summary>
        /// <param name="animalId">Unique identifier of the animal</param>
        /// <param name="health">New health of the animal</param>
        void SetAnimalHealth(int animalId, float health);

        /// <summary>
        /// Sets the name of the animal with id <paramref name="animalId"/> to <paramref name="newName"/>
        /// </summary>
        /// <param name="animalId">Unique identifier of the animal</param>
        /// <param name="name">New name of the animal</param>
        void SetAnimalName(int animalId, string name);
    }
}
