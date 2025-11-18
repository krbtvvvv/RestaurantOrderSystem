namespace RestaurantOrderSystem
{
    public class Client
    {
        public string Name { get; private set; }
        public ClientType Type { get; private set; }
        public string Phone { get; private set; }
        public int LoyaltyPoints { get; private set; }

        public Client(string name, ClientType type, string phone)
        {
            Name = name;
            Type = type;
            Phone = phone;
            LoyaltyPoints = 0;
        }

        public void AddLoyaltyPoints(int points)
        {
            LoyaltyPoints += points;
        }

        public decimal ApplyDiscount(decimal totalPrice)
        {
            switch (Type)
            {
                case ClientType.VIP:
                    return totalPrice * 0.85m; // 15% знижка
                case ClientType.Loyalty when LoyaltyPoints > 100:
                    return totalPrice * 0.9m; // 10% знижка
                default:
                    return totalPrice;
            }
        }

        public string GetClientInfo()
        {
            return $"{Name} ({Type}) - Бали: {LoyaltyPoints}";
        }
    }
}