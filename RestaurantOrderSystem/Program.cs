using System;

namespace RestaurantOrderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Restaurant restaurant = new Restaurant();

            Console.WriteLine("=== РОЗШИРЕНА СИСТЕМА ЗАМОВЛЕНЬ РЕСТОРАНУ ===");

            DemoExtendedRestaurantSystem(restaurant);

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        static void DemoExtendedRestaurantSystem(Restaurant restaurant)
        {
            
            restaurant.DisplayAllClients();

         
            restaurant.GetMenu().DisplayMenu();

            
            restaurant.DemonstrateCasting();

            
            Client vipClient = restaurant.FindClientByName("Іван");
            Client loyaltyClient = restaurant.FindClientByName("Марія");

            Order order1 = restaurant.CreateOrder(5, vipClient);
            Order order2 = restaurant.CreateOrder(3, loyaltyClient);
            Order order3 = restaurant.CreateOrder(7); // Без клієнта

            
            Menu menu = restaurant.GetMenu();

            
            order1.AddItem(menu.FindItemByName("Борщ"));
            order1.AddItem(menu.FindItemByName("Котлета по-київськи"));
            order1.AddItem(menu.FindItemByName("Пиво"));

            
            order2.AddItem(menu.FindItemByName("Салат Цезар"));
            order2.AddItem(menu.FindItemByName("Сік апельсиновий"));
            order2.AddItem(menu.FindItemByName("Тірамісу"));

            
            order3.AddItem(menu.FindItemByName("Борщ"));
            order3.AddItem(menu.FindItemByName("Чай зелений"));

            
            order1.DisplayOrderInfo();
            order2.DisplayOrderInfo();
            order3.DisplayOrderInfo();


            order1.ChangeStatus(OrderStatus.InProgress);
            order1.ChangeStatus(OrderStatus.Ready);
            order1.ChangeStatus(OrderStatus.Paid);

            order2.ChangeStatus(OrderStatus.InProgress);
            order2.ChangeStatus(OrderStatus.Ready);

            
            restaurant.DisplayAllOrders();

           
            restaurant.DisplayOrdersByStatus(OrderStatus.Ready);

            
            restaurant.DisplayStatistics();

            
            Console.WriteLine("\n--- ДЕМОНСТРАЦІЯ ПОШУКУ ---");
            var salads = menu.FindItemsByCategory("Салати");
            Console.WriteLine("Знайдено салати:");
            foreach (var salad in salads)
            {
                Console.WriteLine($"  - {salad.GetDescription()}");
            }

            // 12. Перевірка чи містить замовлення певну страву
            Console.WriteLine($"\nПеревірка замовлення #1:");
            Console.WriteLine($"Містить 'Борщ': {order1.ContainsItem("Борщ")}");
            Console.WriteLine($"Кількість 'Борщ': {order1.GetItemCount("Борщ")}");
        }
    }
}