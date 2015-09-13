using Newtonsoft.Json;
using System;

namespace Eco.DevView.Dto
{
    abstract class Entity
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; }

        /// <summary>
        /// X-coordinate. Rounded to the next integer.
        /// </summary>
        [JsonProperty("x")]
        public float X { get; }

        /// <summary>
        /// Z-coordinate. Rounded to the next integer.
        /// </summary>
        [JsonProperty("z")]
        public float Z { get; }

        /// <summary>
        /// Health, rounded to 2 decimals
        /// </summary>
        [JsonProperty("health")]
        public float Health { get; }

        /// <summary>
        /// The species/type of entity ("deer", "fern")
        /// </summary>
        [JsonProperty("species")]
        public string Species { get; }
        
        public Entity(IEntity other)
        {
            Id = other.Id;
            X = (float)Math.Round(other.X, 0);
            Z = (float)Math.Round(other.Z, 0);
            Health = (float)Math.Round(other.Health, 2);
            Species = other.Species;
        }
    }
}
