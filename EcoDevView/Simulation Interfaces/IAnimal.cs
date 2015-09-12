namespace Eco.DevView
{
    /// <summary>
    /// An interface that is to be implemented to communicate updates in animals to the server.
    /// </summary>
    public interface IAnimal : IObject
    {
        /// <summary>
        /// Current name of the animal.
        /// </summary>
        string Name { get; }
    }
}
