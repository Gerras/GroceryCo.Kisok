using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryCo.KioskSystem.Tests
{
    [TestClass]
    public class BasketModelTests
    {
        private ItemModel _apple;
        private ItemModel _orange;
        private ItemModel _banana;

        [TestInitialize]
        public void Init()
        {
            _apple = new ItemModel
            {
                ItemName = "Apple",
                ItemId = 1,
                ItemPrice = 1
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
                ItemPrice = 3
            };
        }
        [TestMethod]
        public void TestBasketModelCreation()
        {
            BasketModel basket = new BasketModel();
            Assert.IsTrue(basket.Basket != null);
            Assert.AreEqual(basket.Total, 0);
        }

        [TestMethod]
        public void TestAddNewItemToBasket()
        {
            BasketItemModel baskteItem = new BasketItemModel(_apple);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(baskteItem);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(1, basket.Total);
        }

        [TestMethod]
        public void TestAddNewItemAndUpdateBasket()
        {
            BasketItemModel basketItem = new BasketItemModel(_apple);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(basketItem);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(1, basket.Total);
            basket.AddOrUpdateBasket(basketItem);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(2, basket.Total);
        }

        [TestMethod]
        public void TestAddThreeUniqueItemsToBasket()
        {
            BasketItemModel basketItemApple = new BasketItemModel(_apple);
            BasketItemModel basketItemBanana = new BasketItemModel(_banana);
            BasketItemModel basketItemOrange = new BasketItemModel(_orange);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(basketItemApple);
            basket.AddOrUpdateBasket(basketItemBanana);
            basket.AddOrUpdateBasket(basketItemOrange);
            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(6, basket.Total);
        }

        [TestMethod]
        public void TestAddThreeUniqueItemsToBasketAndUpdateOne()
        {
            BasketItemModel basketItemApple = new BasketItemModel(_apple);
            BasketItemModel basketItemBanana = new BasketItemModel(_banana);
            BasketItemModel basketItemOrange = new BasketItemModel(_orange);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(basketItemApple);
            basket.AddOrUpdateBasket(basketItemBanana);
            basket.AddOrUpdateBasket(basketItemOrange);
            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(6, basket.Total);
            basket.AddOrUpdateBasket(basketItemApple);
            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(7, basket.Total);
        }

        [TestMethod]
        public void TestBasketSubtractFromTotal()
        {
            BasketItemModel basketItemApple = new BasketItemModel(_apple);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(basketItemApple);
            basket.AddOrUpdateBasket(basketItemApple);
            Assert.AreEqual(2, basket.Total);
            basket.SubtractFromTotal(1);
            Assert.AreEqual(1, basket.Total);
        }

        [TestMethod]
        public void TestBasketSubtractFromTotalLessThanZero()
        {
            BasketItemModel basketItemApple = new BasketItemModel(_apple);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(basketItemApple);
            basket.AddOrUpdateBasket(basketItemApple);
            Assert.AreEqual(2, basket.Total);
            basket.SubtractFromTotal(3);
            Assert.AreEqual(0, basket.Total);
        }
    }
}
