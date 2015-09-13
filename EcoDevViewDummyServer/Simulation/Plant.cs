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
        public string Species { get; }
        public static readonly string[] AvailableSpecies = new string[] { "Fern", "Tree", "Bush", "Tomato", "Potato" };

        public Plant(int id, Random random)
        {
            Id = id;
            X = random.NextFloat(0, 300f);
            Z = random.NextFloat(0, 300f);
            Species = AvailableSpecies[random.Next(0, AvailableSpecies.Length)];
        }

        public override string ToString() => $"Plant {Id} ({Health * 100:0.00}%)";

        public UpdateOutcome Update(Random random)
        {
            if (random.NextDouble() < 0.05)
            {
                Health = Math.Max(0f, Math.Min(Health + random.NextFloat(-0.1f, 0.1f), 1f)); // chance of varying up to 10% of our health

                // Dead?
                if (Health <= 0f)
                    return UpdateOutcome.Removed;

                return UpdateOutcome.Changed;
            }

            return UpdateOutcome.None;
        }
    }
}
