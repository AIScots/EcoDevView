using System;

namespace Eco.DevView.DummyServer
{
    enum TileType
    {
        Grass
    }

    class Block : IBlock
    {
        public int X { get; }
        public int Z { get; }
        public float Pollution { get; private set; }
        public object Type { get; }

        public Block(int x, int z, TileType type, float pollution)
        {
            if (pollution < 0f || pollution > 1f)
                throw new ArgumentOutOfRangeException(nameof(pollution), pollution, "0 <= value <= 1 expected");
            X = x;
            Z = z;
            Pollution = pollution;
            Type = type;
        }

        public void Update()
        {

        }
    }
}
