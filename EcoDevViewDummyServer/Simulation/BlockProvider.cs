namespace Eco.DevView.DummyServer
{
    public class BlockProvider : IBlockProvider
    {
        public int DimensionX { get; }
        public int DimensionZ { get; }

        private Block[,] _blocks;

        public BlockProvider(int dimensionX, int dimensionZ)
        {
            DimensionX = dimensionX;
            DimensionZ = dimensionZ;

            _blocks = new Block[dimensionX, dimensionZ];

            // Create some dummy tiles
            for (int x = 0; x < DimensionX; ++x)
                for (int z = 0; z < DimensionZ; ++z)
                    _blocks[x, z] = new Block(x, z, TileType.Grass, (Noise.Generate(x / 1000f, z / 1000f) + 1f) / 2f);
        }

        public IBlock GetTopBlock(int x, int z)
        {
            if (x < 0 || x >= DimensionX || z < 0 || z >= DimensionZ)
                return null;

            return _blocks[x, z];
        }
    }
}
