using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrderSystem
{
    public class Order
    {
        private static int _nextId = 1;

        public int Id { get; private set; }
        public int TableNumber { get; private set; }
        public Client Client { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        private List<IMenuItem> _items;

        public IReadOnlyList<IMenuItem> Items => _items.AsReadOnly();
        public decimal TotalPrice => _items.Sum(item => item.Price);
        public decimal FinalPrice => Client?.ApplyDiscount(TotalPrice) ?? TotalPrice;
        public TimeSpan PreparationTime => (CompletedAt ?? DateTime.Now) - CreatedAt;

        
        public Order(int tableNumber, Client client = null)
        {
            Id = _nextId++;
            TableNumber = tableNumber;
            Client = client;
            Status = OrderStatus.New;
            CreatedAt = DateTime.Now;
            _items = new List<IMenuItem>();
        }

        
        public Order(int tableNumber) : this(tableNumber, null)
        {
        }

        public void AddItem(IMenuItem item)
        {
            _items.Add(item);
            Console.WriteLine($"Додано позицію: {item.Name}");
        }

        public bool RemoveItem(IMenuItem item)
        {
            bool removed = _items.Remove(item);
            if (removed)
            {
                Console.WriteLine($"Видалено позицію: {item.Name}");
            }
            return removed;
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            Console.WriteLine($"> Змінено статус: {Status} → {newStatus}");
            Status = newStatus;

            if (newStatus == OrderStatus.Paid)
            {
                CompletedAt = DateTime.Now;
                if (Client != null)
                {
                    int points = (int)(TotalPrice / 10);
                    Client.AddLoyaltyPoints(points);
                    Console.WriteLine($"Нараховано бонусних балів: {points}");
                }
            }
        }

        public void DisplayOrderInfo()
        {
            Console.WriteLine($"\n--- ЗАМОВЛЕННЯ #{Id} ---");
            Console.WriteLine($"Стіл: {TableNumber}");
            if (Client != null)
            {
                Console.WriteLine($"Клієнт: {Client.GetClientInfo()}");
            }
            Console.WriteLine($"Статус: {Status}");
            Console.WriteLine($"Створено: {CreatedAt:HH:mm}");
            if (CompletedAt.HasValue)
            {
                Console.WriteLine($"Завершено: {CompletedAt:HH:mm}");
                Console.WriteLine($"Час приготування: {PreparationTime:mm} хв");
            }
            Console.WriteLine("Позиції:");

            foreach (var item in _items)
            {
                Console.WriteLine($"  - {item.GetDescription()}");
            }

            Console.WriteLine($"ЗАГАЛЬНА СУМА: {TotalPrice} грн");
            if (Client != null && TotalPrice != FinalPrice)
            {
                Console.WriteLine($"ЗНИЖКА: {TotalPrice - FinalPrice} грн");
            }
            Console.WriteLine($"ДО СПЛАТИ: {FinalPrice} грн");
            Console.WriteLine("----------------------------");
        }

        public bool ContainsItem(string itemName)
        {
            return _items.Any(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public int GetItemCount(string itemName)
        {
            return _items.Count(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }
    }
}