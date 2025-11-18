namespace RestaurantOrderSystem
{
    public abstract class MenuItem : IMenuItem
    {
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public string Category { get; protected set; }

        protected MenuItem(string name, decimal price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
        }

        public abstract string GetDescription();
    }
}