using System;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryCo.KioskSystem.Tests
{
    [TestClass]
    public class BaskItemModelTests
    {
        [TestMethod]
        public void TestBaskteItemModelCreation()
        {
            ItemModel itemModel = new ItemModel
            {
                ItemName = "Apple",
                ItemId = 1,
                ItemPrice = 1
            };
            var basketItem = new BasketItemModel(itemModel);
            Assert.AreEqual(itemModel.ItemName, basketItem.ItemName);
            Assert.AreEqual(itemModel.ItemId, basketItem.ItemId);
            Assert.AreEqual(itemModel.ItemPrice, basketItem.ItemPrice);
            Assert.AreEqual(1, basketItem.ItemQuantity);
            Assert.IsTrue(basketItem.Discount == null);
        }
    }
}
