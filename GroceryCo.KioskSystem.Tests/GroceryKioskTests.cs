using System;
using System.Collections.Generic;
using GroceryCo.KioskSystem.Core.Helpers;
using GroceryCo.KioskSystem.Core.KioskDefinitions;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL;
using GroceryCo.KioskSystem.DAL.DALDefinitions;
using GroceryCo.KioskSystem.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryCo.KioskSystem.Tests
{
    [TestClass]
    public class GroceryKioskTests
    {
        private ItemModel _apple;
        private ItemModel _orange;
        private ItemModel _banana;
        private List<ItemModel> _itemList;
        [TestInitialize]
        public void Init()
        {
            _apple = new ItemModel
            {
                ItemName = "Apple",
                ItemId = 1,
                ItemPrice = 1.5M
            };
            _orange = new ItemModel
            {
                ItemName = "Orange",
                ItemId = 2,
                ItemPrice = 2
            };
            _banana = new ItemModel
            {
                ItemName = "Banana",
                ItemId = 3,
                ItemPrice = 1
            };
            _itemList = new List<ItemModel> {_apple, _orange, _banana};
        }

        [TestMethod]
        public void TestAddingOneItemToBasket()
        {
            string[] stringBasket = {"Apple"};
            BasketModel basket = GroceryKioskHelper.GetItemAndAddOrUpdateBasket(stringBasket, _itemList);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(1.5M, basket.Total);
        }

        [TestMethod]
        public void TestAddingMultipleItemsToBasket()
        {
            string[] stringBasket = { "Apple", "Orange", "Banana" };
            BasketModel basket = GroceryKioskHelper.GetItemAndAddOrUpdateBasket(stringBasket, _itemList);
            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(4.5M, basket.Total);
        }

        [TestMethod]
        public void TestAddingMultipleItemsToBasketIgnoreCase()
        {
            string[] stringBasket = { "apple", "orange", "banana" };
            BasketModel basket = GroceryKioskHelper.GetItemAndAddOrUpdateBasket(stringBasket, _itemList);
            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(4.5M, basket.Total);
        }


        [TestMethod]
        public void TestIgnoreUnknownBasketItems()
        {
            string[] stringBasket = { "apple", "orange", "banana", "motorcycle" };
            BasketModel basket = GroceryKioskHelper.GetItemAndAddOrUpdateBasket(stringBasket, _itemList);
            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(4.5M, basket.Total);
        }
    }
}
