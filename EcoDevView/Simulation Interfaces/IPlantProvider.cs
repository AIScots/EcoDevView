using System.Collections.Generic;

namespace Eco.DevView
{
    public interface IPlantProvider
    {
        /// <summary>
        /// Returns the plant with the unique identifier <paramref name="id"/>
        /// </summary>
        /// <param name="id">Unique identifier of the plant</param>
        /// <returns></returns>
        IPlant GetPlant(int id);

        /// <summary>
        /// Returns a collection of all plants.
        /// </summary>
        /// <returns></returns>
        ICollection<IPlant> GetPlants();

        /// <summary>
        /// Returns a collection of all updated plants.
        /// </summary>
        /// <returns></returns>
        ICollection<IPlant> GetUpdatedPlants();
    }
}
