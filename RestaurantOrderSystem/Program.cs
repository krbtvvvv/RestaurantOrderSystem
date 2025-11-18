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
            // 1. Показуємо всіх клієнтів
            restaurant.DisplayAllClients();

            // 2. Показуємо меню
            restaurant.GetMenu().DisplayMenu();

            // 3. Демонструємо приведення типів
            restaurant.DemonstrateCasting();

            // 4. Створюємо замовлення з клієнтами
            Client vipClient = restaurant.FindClientByName("Іван");
            Client loyaltyClient = restaurant.FindClientByName("Марія");

            Order order1 = restaurant.CreateOrder(5, vipClient);
            Order order2 = restaurant.CreateOrder(3, loyaltyClient);
            Order order3 = restaurant.CreateOrder(7); // Без клієнта

            // 5. Додаємо позиції до замовлень
            Menu menu = restaurant.GetMenu();

            // Замовлення 1
            order1.AddItem(menu.FindItemByName("Борщ"));
            order1.AddItem(menu.FindItemByName("Котлета по-київськи"));
            order1.AddItem(menu.FindItemByName("Пиво"));

            // Замовлення 2
            order2.AddItem(menu.FindItemByName("Салат Цезар"));
            order2.AddItem(menu.FindItemByName("Сік апельсиновий"));
            order2.AddItem(menu.FindItemByName("Тірамісу"));

            // Замовлення 3
            order3.AddItem(menu.FindItemByName("Борщ"));
            order3.AddItem(menu.FindItemByName("Чай зелений"));

            // 6. Показуємо інформацію про замовлення
            order1.DisplayOrderInfo();
            order2.DisplayOrderInfo();
            order3.DisplayOrderInfo();

            // 7. Змінюємо статуси замовлень
            order1.ChangeStatus(OrderStatus.InProgress);
            order1.ChangeStatus(OrderStatus.Ready);
            order1.ChangeStatus(OrderStatus.Paid);

            order2.ChangeStatus(OrderStatus.InProgress);
            order2.ChangeStatus(OrderStatus.Ready);

            // 8. Показуємо всі замовлення
            restaurant.DisplayAllOrders();

            // 9. Показуємо замовлення за статусом
            restaurant.DisplayOrdersByStatus(OrderStatus.Ready);

            // 10. Показуємо статистику
            restaurant.DisplayStatistics();

            // 11. Демонстрація пошуку
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