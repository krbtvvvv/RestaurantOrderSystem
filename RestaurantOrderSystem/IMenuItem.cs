namespace RestaurantOrderSystem
{
    public interface IMenuItem
    {
        string Name { get; }
        decimal Price { get; }
        string Category { get; }
        string GetDescription();
    }
}