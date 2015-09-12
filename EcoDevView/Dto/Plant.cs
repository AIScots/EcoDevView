namespace Eco.DevView.Dto
{
    class Plant : Entity, IPlant
    {
        public Plant(IPlant plant)
            : base(plant)
        {
        }
    }
}
