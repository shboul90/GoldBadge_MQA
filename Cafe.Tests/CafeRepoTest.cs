using Cafe.models;
using Cafe.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Cafe.Tests
{
    [TestClass]
    public class CafeRepoTest
    {
        private readonly MenuItemRepository _repo = new MenuItemRepository();

        [TestInitialize]
        public void Arrange()
        {
            MenuItem mItem1 = new MenuItem("Cheese Sandwich", "American cheese sandwich in toast bread", new List<string>() { "Bread", "American cheese", "Salt", "Pepper" }, 200m);
            _repo.AddMenuItem(mItem1);
        }

        [TestMethod]
        public void AddMenuItem_MenuItemisNull_ReturnFalse()
        {
            MenuItem mItem = null;
            MenuItemRepository repo = new MenuItemRepository();

            bool result = repo.AddMenuItem(mItem);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddMenuItem_MenuItemisnotNull_ReturnTrue()
        {
            MenuItem mItem = new MenuItem("Cheese Sandwich", "American cheese sandwich in toast bread", new List<string>() { "Bread", "American cheese", "Salt", "Pepper" }, 200m);
            MenuItemRepository repo = new MenuItemRepository();

            bool result = repo.AddMenuItem(mItem);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMenuItemByNumber_MenuItemExists_ReturnMovie()
        {
            int id = 1;

            MenuItem result = _repo.GetMenuItemByNumber(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.MealNumber, id);
        }

        [TestMethod]
        public void GetMenuItemByNumber_MenuItemDoesntExist_ReturnNull()
        {
            int id = 1234;

            MenuItem result = _repo.GetMenuItemByNumber(id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteMenuItem_MenuItemDoesntExist_ReturnNull()
        {
            int id = 1234;

            bool result = _repo.DeleteMenuItem(id);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteMenuItem_MenuItemExist_ReturnTrue()
        {
            int id = 1;

            bool result = _repo.DeleteMenuItem(id);

            Assert.IsTrue(result);
        }
        //testm
    }
}
