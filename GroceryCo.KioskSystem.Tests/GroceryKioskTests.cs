using System;
using System.Collections.Generic;
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
        private Mock<IKioskDataStore> _dataStore;
        private Mock<IPromotionService> _promotionService;
        private List<ItemModel> _kioskItems;

        [TestInitialize]
        public void Init()
        {
            _dataStore = new Mock<IKioskDataStore>();
            _promotionService = new Mock<IPromotionService>();
            _kioskItems = new List<ItemModel>
            {
                new ItemModel
                {
                    ItemId = 1,
                    ItemName = "Apple",
                    ItemPrice = 1
                },
                new ItemModel
                {
                    ItemId = 2,
                    ItemName = "Orange",
                    ItemPrice = 2,
                },
                new ItemModel
                {
                    ItemId = 3,
                    ItemName = "Banana",
                    ItemPrice = 2
                }
            };

        }

        //[TestMethod]
        //public void TestKioskTransactionNoPromotionsOneItemEach()
        //{
        //    _dataStore.Setup(x => x.GetKioskItems()).Returns(_kioskItems);
        //    _dataStore.Setup(x => x.GetKioskPromotions()).Returns(new List<PromotionModel>());
        //    string[] testBasket = {"Apple", "Orange", "Banana"};
        //    GroceryKiosk kisok = new GroceryKiosk(_dataStore.Object, _promotionService.Object);
        //    BasketModel basket = kisok.BeginTransaction(testBasket);
        //    List<BasketItemModel> basketItems = new List<BasketItemModel>
        //    {
        //        new BasketItemModel(_kioskItems[0]),
        //        new BasketItemModel(_kioskItems[1]),
        //        new BasketItemModel(_kioskItems[2])
        //    };
        //    for (int i = 0; i < basketItems.Count; i++)
        //    {
        //        Assert.AreEqual(basketItems[i].ItemName, basket.Basket[i].ItemName);
        //        Assert.IsTrue(basketItems[i].Discount == null && basket.Basket[i].Discount == null);
        //        Assert.AreEqual(basketItems[i].ItemId, basket.Basket[i].ItemId);
        //        Assert.AreEqual(basketItems[i].ItemPrice, basket.Basket[i].ItemPrice);
        //        Assert.AreEqual(basketItems[i].ItemQuantity, basket.Basket[i].ItemQuantity);
        //    }
        //}
    }
}
