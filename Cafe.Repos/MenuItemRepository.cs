using Cafe.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repos
{
    public class MenuItemRepository
    {
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

        private int _idcounter = default;

        public List<MenuItem> GetAll() => _menuItems;

        public bool AddMenuItem(MenuItem menuItem)
        {
            if (menuItem is null)
            {
                return false;
            }
            else
            {
                _idcounter++;

                menuItem.MealNumber = _idcounter;
                
                _menuItems.Add(menuItem);
                
                return true;
            }
        }

        public MenuItem GetMenuItemByNumber(int mealNumber)
        {
            foreach(var item in _menuItems)
            {
                if (item.MealNumber == mealNumber)
                {
                    return item;
                }
            }

            return null;
        }

        public bool DeleteMenuItem(int mealNumber)
        {
            var item = GetMenuItemByNumber(mealNumber);

            if (item is null)
            {
                return false;
            }
            else
            {
                int initialCount = _menuItems.Count;

                _menuItems.Remove(item);

                foreach(MenuItem content in _menuItems)
                {
                    if(item.MealNumber<content.MealNumber)
                    {
                        content.MealNumber--;
                    }
                }

                _idcounter=_menuItems.Count;

                return initialCount>_menuItems.Count;
            }
        }

        public List<MenuItem> GetMenuItemList()
        {
            return _menuItems;
        }
    }
}
