namespace Eco.DevView
{
    /// <summary>
    /// An <see cref="IBlockProvider"/> is responsible for fetching blocks. An implementation
    /// of this interface should look up said tiles in the map.
    /// </summary>
    public interface IBlockProvider
    {
        /// <summary>
        /// Defines how many blocks there are along the x-axis. Together with <see cref="DimensionZ"/>, this should form the ground plane.
        /// </summary>
        int DimensionX { get; }

        /// <summary>
        /// Defines how many blocks there are along the z-axis. Together with <see cref="DimensionX"/>, this should form the ground plane.
        /// </summary>
        int DimensionZ { get; }

        /// <summary>
        /// Returns the top-most block at <paramref name="x"/>/<paramref name="z"/>
        /// </summary>
        /// <param name="x">x coordinate of the block</param>
        /// <param name="z">z coordinate of the block</param>
        /// <returns>An <see cref="IBlock"/> if the block exists, <c>null</c> otherwise.</returns>
        IBlock GetTopBlock(int x, int z);
    }
}