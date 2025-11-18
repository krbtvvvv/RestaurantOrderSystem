using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrderSystem
{
    public class Menu
    {
        private List<IMenuItem> _menuItems;

        public Menu()
        {
            _menuItems = new List<IMenuItem>();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            _menuItems.Add(new Dish("Борщ", 120m, "Перші страви", "Перше"));
            _menuItems.Add(new Dish("Котлета по-київськи", 180m, "Основні страви", "Друге"));
            _menuItems.Add(new Dish("Салат Цезар", 150m, "Салати", "Холодна закуска"));
            _menuItems.Add(new Dish("Тірамісу", 90m, "Десерти", "Десерт"));

            _menuItems.Add(new Drink("Кава", 60m, "Гарячі напої", 200, false));
            _menuItems.Add(new Drink("Сік апельсиновий", 70m, "Холодні напої", 250, false));
            _menuItems.Add(new Drink("Пиво", 80m, "Алкогольні напої", 500, true));
            _menuItems.Add(new Drink("Чай зелений", 50m, "Гарячі напої", 300, false));
        }

        public void DisplayMenu()
        {
            Console.WriteLine("\n--- МЕНЮ РЕСТОРАНУ ---");

            var groupedMenu = _menuItems.GroupBy(item => item.Category);

            int counter = 1;
            foreach (var categoryGroup in groupedMenu)
            {
                Console.WriteLine($"\n{categoryGroup.Key}:");
                foreach (var item in categoryGroup)
                {
                    Console.WriteLine($"{counter}. {item.GetDescription()}");
                    counter++;
                }
            }
            Console.WriteLine("-----------------------");
        }

        public IMenuItem FindItemByName(string name)
        {
            return _menuItems.FirstOrDefault(item =>
                item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<IMenuItem> FindItemsByCategory(string category)
        {
            return _menuItems.Where(item =>
                item.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<IMenuItem> GetAllItems()
        {
            return new List<IMenuItem>(_menuItems);
        }
    }
}