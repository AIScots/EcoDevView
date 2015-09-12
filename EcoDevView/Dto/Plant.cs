namespace Eco.DevView.Dto
{
    class Plant : Object, IPlant
    {
        public Plant(IPlant plant)
            : base(plant)
        {
        }
    }
}
