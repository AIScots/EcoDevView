using Newtonsoft.Json;

namespace Eco.DevView
{
    /// <summary>
    /// Represents the top block of the map
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// X-position of the block
        /// </summary>
        [JsonProperty("x")]
        int X { get; }

        /// <summary>
        /// Z-position of the block
        /// </summary>
        [JsonProperty("z")]
        int Z { get; }

        /// <summary>
        /// Defines the pollution of the block, from 0 (no pollution) to 1 (completely polluted)
        /// </summary>
        [JsonProperty("pollution")]
        float Pollution { get; }

        /// <summary>
        /// Used by <see cref="ITileRenderer"/> to determine how the block should be rendered.
        /// This is preferably either an enum or a string.
        /// </summary>
        [JsonProperty("type")]
        object Type { get; }
    }
}
