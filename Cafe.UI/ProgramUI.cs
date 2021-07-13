using Cafe.models;
using Cafe.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.UI
{
    class ProgramUI
    {
        private MenuItemRepository _menuItemsRepo = new MenuItemRepository();

        public void Run()
        {
            SeedContentList();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Select a menu option:\n" +
                    "1. Add a Menu Item\n" +
                    "2. Delete a Menu Item\n" +
                    "3. View all Menu Items\n" +
                    "4. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddMenuItem();
                        break;

                    case "2":
                        RemoveMenuItem();
                        break;

                    case "3":
                        DisplayMenuList();
                        break;

                    case "4":
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;
                }

                Console.WriteLine("Please press any key to continue... ");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void AddMenuItem()
        {
            Console.Clear();
            MenuItem newContent = new MenuItem();

            Console.WriteLine("Enter the Meal Name for the menu item:");
            newContent.MealName = Console.ReadLine();

            Console.WriteLine("Enter the Description of the menu item:");
            newContent.Description = Console.ReadLine();

            Console.WriteLine("Enter the list of Ingredients for the menu item below: " +
                "(Please seperate items using a comma ,)");
            string userInput = Console.ReadLine();
            newContent.Ingredients = SeperateStringWithCommaToList(userInput);

            Console.WriteLine("Enter the Price of the menu item:");
            string userPriceAsString = Console.ReadLine();
            newContent.Price = decimal.Parse(userPriceAsString);

            _menuItemsRepo.AddMenuItem(newContent);
            DisplayMenuList();
        }
        private void RemoveMenuItem()
        {

            DisplayMenuList();

            Console.WriteLine("\nEnter the Number of the menu item you'd like to remove:");
            string input = Console.ReadLine();
            int inputAsInt = int.Parse(input);

            bool wasDeleted = _menuItemsRepo.DeleteMenuItem(inputAsInt);

            if (wasDeleted)
            {
                Console.WriteLine("Developer was successfully deleted.");
                DisplayMenuList();
            }
            else
            {
                Console.WriteLine("The developer could not be deleted.");
                DisplayMenuList();
            }
        }

        private void DisplayMenuList()
        {
            Console.Clear();
            List<MenuItem> _listOfMenuItems = _menuItemsRepo.GetMenuItemList();

            foreach (MenuItem content in _listOfMenuItems)
            {
                Console.WriteLine($"----------------------------\n" +
                    $"Meal Number: {content.MealNumber}\n" +
                    $"Meal Name: {content.MealName}\n" +
                    $"Description: {content.Description}\n" +
                    $"List of Ingredients:");
                foreach (string ingredient in content.Ingredients)
                {
                    Console.WriteLine($"{ingredient}");
                }
                Console.WriteLine($"Price: {content.Price}$\n" +
                    $"----------------------------\n");
            }
        }

        private List<string> SeperateStringWithCommaToList(string input)
        {
            string[] subs = input.Split(',');

            List<string> _userInputAsList = new List<string>();

            foreach (string sub in subs)
            {
                _userInputAsList.Add(sub);
            }

            return _userInputAsList;
        }
        private void SeedContentList()
        {
            MenuItem grilledSalad = new MenuItem("Grilled Chicken Salad", "A grilled chicken, seasoned in lemon and italian dressing",new List<string>() {"chicken", "Lemon", "Italian"},34.5m);
            MenuItem steakAndFries = new MenuItem("Steak and Fries", "A grilled steak, lathered in gravy and a side of fries", new List<string>() { "Gravy", "Steak", "Fries" }, 50m);

            _menuItemsRepo.AddMenuItem(grilledSalad);
            _menuItemsRepo.AddMenuItem(steakAndFries);
        }
    }
}
