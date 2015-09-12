using System;

namespace Eco.DevView.DummyServer
{
    class Plant : IPlant, IUpdateable
    {
        public int Id { get; }
        private float _health = 1f;

        public float Health
        {
            get { return _health; }
            set
            {
                if (value < 0 || value > 1f)
                    throw new ArgumentOutOfRangeException();

                _health = value;
            }
        }
        public float X { get; set; }
        public float Z { get; set; }

        public Plant(int id, float x, float z)
        {
            Id = id;
            X = x;
            Z = z;
        }

        public override string ToString() => $"Plant {Id} ({Health * 100:0.00}%)";

        public UpdateOutcome Update(Random random)
        {
            if (random.NextDouble() < 0.05)
            {
                Health = Math.Max(0f, Math.Min(Health + (float)(random.NextDouble() - 0.5) / 10f, 1f)); // chance of varying up to 10% of our health

                // Dead?
                if (Health <= 0f)
                    return UpdateOutcome.Removed;

                return UpdateOutcome.Changed;
            }

            return UpdateOutcome.None;
        }
    }
}
