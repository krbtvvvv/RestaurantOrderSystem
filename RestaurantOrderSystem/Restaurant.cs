using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrderSystem
{
    public class Restaurant
    {
        private Menu _menu;
        private List<Order> _orders;
        private List<Client> _clients;

        public Restaurant()
        {
            _menu = new Menu();
            _orders = new List<Order>();
            _clients = new List<Client>();
            InitializeClients();
        }

        private void InitializeClients()
        {
            _clients.Add(new Client("Іван Петренко", ClientType.VIP, "+380501234567"));
            _clients.Add(new Client("Марія Коваленко", ClientType.Loyalty, "+380671234568"));
            _clients.Add(new Client("Олександр Сидоренко", ClientType.Regular, "+380631234569"));
        }

        public Order CreateOrder(int tableNumber, Client client = null)
        {
            var order = new Order(tableNumber, client);
            _orders.Add(order);
            Console.WriteLine($"\nСтворено нове замовлення #{order.Id} для столика №{tableNumber}");
            if (client != null)
            {
                Console.WriteLine($"Клієнт: {client.GetClientInfo()}");
            }
            return order;
        }

        public Order FindOrderById(int id)
        {
            return _orders.FirstOrDefault(order => order.Id == id);
        }

        public List<Order> FindOrdersByClient(string clientName)
        {
            return _orders.Where(order =>
                order.Client?.Name.Contains(clientName, StringComparison.OrdinalIgnoreCase) == true).ToList();
        }

        public void DisplayAllOrders()
        {
            Console.WriteLine("\n--- УСІ ЗАМОВЛЕННЯ ---");
            if (!_orders.Any())
            {
                Console.WriteLine("Замовлень немає");
                return;
            }

            foreach (var order in _orders.OrderBy(o => o.Status).ThenBy(o => o.CreatedAt))
            {
                string clientInfo = order.Client != null ? $" | Клієнт: {order.Client.Name}" : "";
                Console.WriteLine($"ID: {order.Id} | Стіл: {order.TableNumber}{clientInfo} | " +
                                $"Статус: {order.Status} | Сума: {order.FinalPrice} грн");
            }
            Console.WriteLine("----------------------");
        }

        public void DisplayOrdersByStatus(OrderStatus status)
        {
            var filteredOrders = _orders.Where(o => o.Status == status).ToList();
            Console.WriteLine($"\n--- ЗАМОВЛЕННЯ ЗІ СТАТУСОМ: {status} ---");

            if (!filteredOrders.Any())
            {
                Console.WriteLine("Не знайдено");
                return;
            }

            foreach (var order in filteredOrders)
            {
                Console.WriteLine($"ID: {order.Id} | Стіл: {order.TableNumber} | Сума: {order.FinalPrice} грн");
            }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("\n=== СТАТИСТИКА РЕСТОРАНУ ===");
            Console.WriteLine($"Всього замовлень: {_orders.Count}");
            Console.WriteLine($"Загальний дохід: {_orders.Where(o => o.Status == OrderStatus.Paid).Sum(o => o.FinalPrice)} грн");
            Console.WriteLine($"Активних замовлень: {_orders.Count(o => o.Status != OrderStatus.Paid)}");

            var popularItems = _orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Name)
                .OrderByDescending(g => g.Count())
                .Take(3);

            Console.WriteLine("\nПОПУЛЯРНІ ПОЗИЦІЇ:");
            foreach (var item in popularItems)
            {
                Console.WriteLine($"  - {item.Key}: {item.Count()} замовлень");
            }
        }

        public Client FindClientByName(string name)
        {
            return _clients.FirstOrDefault(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayAllClients()
        {
            Console.WriteLine("\n--- ВСІ КЛІЄНТИ ---");
            foreach (var client in _clients)
            {
                Console.WriteLine(client.GetClientInfo());
            }
        }

        public Menu GetMenu()
        {
            return _menu;
        }

        public void DemonstrateCasting()
        {
            Console.WriteLine("\n--- ДЕМОНСТРАЦІЯ ПРИВЕДЕННЯ ТИПІВ ---");

            Dish dish = new Dish("Суп", 100m, "Перші страви", "Перше");
            IMenuItem upcastedItem = dish;
            Console.WriteLine($"Upcast: {upcastedItem.GetDescription()}");

            if (upcastedItem is Dish downcastedDish)
            {
                Console.WriteLine($"Downcast успішний: {downcastedDish.Type}");
            }

            Console.WriteLine("-----------------------------------");
        }
    }
}