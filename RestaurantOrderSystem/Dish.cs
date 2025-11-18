namespace RestaurantOrderSystem
{
    public class Dish : MenuItem
    {
        public string Type { get; private set; }

        public Dish(string name, decimal price, string category, string type)
            : base(name, price, category)
        {
            Type = type;
        }

        public override string GetDescription()
        {
            return $"{Name} ({Type}) - {Price} грн";
        }
    }
}