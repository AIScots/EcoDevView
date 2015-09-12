using Newtonsoft.Json;

namespace Eco.DevView.Dto
{
    /// <summary>
    /// DTO for animals.
    /// </summary>
    /// <remarks>
    /// If we sent the <see cref="IAnimal"/> directly to SignalR, it would serialize all attributes instead of only
    /// the ones we care about. Therefore, we need this DTO to minimize traffic.
    /// However, it's still compatible to <see cref="IAnimal"/>, but should not be used anywhere other than being sent
    /// to the client.
    /// </remarks>
    class Animal : Entity, IAnimal
    {
        [JsonProperty("name")]
        public string Name { get; }

        public Animal(IAnimal animal)
            : base(animal)
        {
            Name = animal.Name;
        }
    }
}
