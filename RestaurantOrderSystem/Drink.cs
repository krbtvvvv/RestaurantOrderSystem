namespace RestaurantOrderSystem
{
    public class Drink : MenuItem
    {
        public int Volume { get; private set; }
        public bool IsAlcoholic { get; private set; }

        public Drink(string name, decimal price, string category, int volume, bool isAlcoholic)
            : base(name, price, category)
        {
            Volume = volume;
            IsAlcoholic = isAlcoholic;
        }

        public override string GetDescription()
        {
            string alcoholInfo = IsAlcoholic ? "алкогольний" : "без алкоголю";
            return $"{Name} ({Volume} мл, {alcoholInfo}) - {Price} грн";
        }
    }
}