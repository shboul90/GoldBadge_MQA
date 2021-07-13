using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.models
{
    public class MenuItem
    {
        public int MealNumber { get; set; }

        public string MealName { get; set; }

        public string Description { get; set; }

        public List<string> Ingredients { get; set; }

        public decimal Price { get; set; }

        public MenuItem()
        {

        }

        public MenuItem(
            string mealName,
            string description,
            List<string> ingredients,
            decimal price)
        {
            this.MealName = mealName;
            this.Description = description;
            this.Ingredients = ingredients;
            this.Price = price;
        }
    }
}
