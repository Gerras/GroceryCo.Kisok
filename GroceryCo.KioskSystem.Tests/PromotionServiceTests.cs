using System.Collections.Generic;
using GroceryCo.KioskSystem.Core;
using GroceryCo.KioskSystem.Core.KioskDefinitions;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryCo.KioskSystem.Tests
{
    [TestClass]
    public class PromotionServiceTests
    {
        private ItemModel _apple;
        private ItemModel _orange;
        private ItemModel _banana;
        private PromotionModel _appleDiscount;
        private PromotionModel _orangeBuyOneGetOneHalfOff;
        private PromotionModel _bananaGroupDiscountOnThree;
        private PromotionModel _appleGroupDiscount;

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
            _appleDiscount = new PromotionModel
            {
                PromotionId = 1,
                ItemId = 1,
                SalePrice = 1,
                Quantity = 1,
                Description = "50 cents off regular priced apples."
            };
            _orangeBuyOneGetOneHalfOff = new PromotionModel
            {
                PromotionId = 2,
                ItemId = 2,
                SalePrice = 3,
                Quantity = 2,
                Description = "Buy an Orange get the second 50% off."
            };
            _bananaGroupDiscountOnThree = new PromotionModel
            {
                PromotionId = 3,
                ItemId = 3,
                SalePrice = 2,
                Quantity = 3,
                Description = "Buy 3 Banana's for $2.00"
            };
            _appleGroupDiscount = new PromotionModel
            {
                PromotionId = 4,
                ItemId = 1,
                SalePrice = 1.5M,
                Description = "Buy an apple ge the second free.",
                Quantity = 2,
            };
        }

        [TestMethod]
        public void TestApplyingOnePromotionToBasketWithOneItem()
        {
            List<PromotionModel> promotions = new List<PromotionModel> {_appleDiscount};
            BasketItemModel appleBasketItem = new BasketItemModel(_apple);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(appleBasketItem);
            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(1, basket.Total);
            Assert.IsTrue(basket.Basket[0].Discount != null);
            Assert.AreEqual(0.5M, basket.Basket[0].Discount.TotalDiscountAmount);
        }

        [TestMethod]
        public void TestApplyingOnePromotionToBasketWithTwoDifferentItems()
        {
            List<PromotionModel> promotions = new List<PromotionModel> { _appleDiscount };
            BasketItemModel appleBasketItem = new BasketItemModel(_apple);
            BasketItemModel orangeBasketItem = new BasketItemModel(_orange);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(appleBasketItem);
            basket.AddOrUpdateBasket(orangeBasketItem);
            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);
            Assert.AreEqual(2, basket.Basket.Count);
            Assert.AreEqual(3, basket.Total);
            Assert.IsTrue(basket.Basket[0].Discount != null);
            Assert.AreEqual("Apple", basket.Basket[0].ItemName);
            Assert.IsTrue(basket.Basket[1].Discount == null);
            Assert.AreEqual("Orange", basket.Basket[1].ItemName);
            Assert.AreEqual(0.5M, basket.Basket[0].Discount.TotalDiscountAmount);
        }

        [TestMethod]
        public void TestApplyingSamePromotionTwiceToBasketWithSameTwoItems()
        {
            List<PromotionModel> promotions = new List<PromotionModel> { _appleDiscount };
            BasketItemModel appleBasketItem = new BasketItemModel(_apple);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(appleBasketItem);
            basket.AddOrUpdateBasket(appleBasketItem);
            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(2, basket.Total);
            Assert.IsTrue(basket.Basket[0].Discount != null);
            Assert.AreEqual(1M, basket.Basket[0].Discount.TotalDiscountAmount);
            Assert.AreEqual(2, basket.Basket[0].ItemQuantity);
        }

        [TestMethod]
        public void TestBasketItemFailsToMeetPromotionThreshold()
        {
            List<PromotionModel> promotions = new List<PromotionModel> { _orangeBuyOneGetOneHalfOff };
            BasketItemModel orangeBasketItem = new BasketItemModel(_orange);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(orangeBasketItem);
            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(2, basket.Total);
            Assert.IsTrue(basket.Basket[0].Discount == null);
            Assert.AreEqual("Orange", basket.Basket[0].ItemName);
        }

        [TestMethod]
        public void TestBasketItemMeetsPromotionThreshold()
        {
            List<PromotionModel> promotions = new List<PromotionModel> { _orangeBuyOneGetOneHalfOff };
            BasketItemModel orangeBasketItem = new BasketItemModel(_orange);
            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(orangeBasketItem);
            basket.AddOrUpdateBasket(orangeBasketItem);
            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);
            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(3, basket.Total);
            Assert.IsTrue(basket.Basket[0].Discount != null);
            Assert.AreEqual("Orange", basket.Basket[0].ItemName);
            Assert.AreEqual(2, basket.Basket[0].ItemQuantity);
        }

        [TestMethod]
        public void TestApplyingMultiplePromotionsToMultipleBasketItems()
        {
            List<PromotionModel> promotions = new List<PromotionModel> { _appleDiscount, _orangeBuyOneGetOneHalfOff, _bananaGroupDiscountOnThree };

            BasketItemModel orangeBasketItem = new BasketItemModel(_orange);
            BasketItemModel appleBasketItem = new BasketItemModel(_apple);
            BasketItemModel bananaBasketItem = new BasketItemModel(_banana);

            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(appleBasketItem);
            basket.AddOrUpdateBasket(orangeBasketItem);
            basket.AddOrUpdateBasket(bananaBasketItem);
            basket.AddOrUpdateBasket(orangeBasketItem);
            basket.AddOrUpdateBasket(bananaBasketItem);
            basket.AddOrUpdateBasket(bananaBasketItem);
            basket.AddOrUpdateBasket(appleBasketItem);

            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);

            Assert.AreEqual(3, basket.Basket.Count);
            Assert.AreEqual(7, basket.Total);

            Assert.IsTrue(basket.Basket[0].Discount != null);
            Assert.AreEqual("Apple", basket.Basket[0].ItemName);
            Assert.AreEqual(2, basket.Basket[0].ItemQuantity);
            Assert.AreEqual(1,basket.Basket[0].Discount.ThresholdQuantity);
            Assert.AreEqual(.5M, basket.Basket[0].Discount.DiscountAmount);
            Assert.AreEqual(1, basket.Basket[0].Discount.TotalDiscountAmount);

            Assert.IsTrue(basket.Basket[1].Discount != null);
            Assert.AreEqual("Orange", basket.Basket[1].ItemName);
            Assert.AreEqual(2, basket.Basket[1].ItemQuantity);
            Assert.AreEqual(2, basket.Basket[1].Discount.ThresholdQuantity);
            Assert.AreEqual(1, basket.Basket[1].Discount.DiscountAmount);
            Assert.AreEqual(1, basket.Basket[1].Discount.TotalDiscountAmount);

            Assert.IsTrue(basket.Basket[2].Discount != null);
            Assert.AreEqual("Banana", basket.Basket[2].ItemName);
            Assert.AreEqual(3, basket.Basket[2].ItemQuantity);
            Assert.AreEqual(3, basket.Basket[2].Discount.ThresholdQuantity);
            Assert.AreEqual(1, basket.Basket[2].Discount.DiscountAmount);
            Assert.AreEqual(1, basket.Basket[2].Discount.TotalDiscountAmount);
        }

        [TestMethod]
        public void TestIfMultiplePromotionsAddedForSameItemOnlyFirstIsTaken()
        {
            List<PromotionModel> promotions = new List<PromotionModel> { _appleDiscount, _appleGroupDiscount};

            BasketItemModel appleBasketItem = new BasketItemModel(_apple);

            BasketModel basket = new BasketModel();
            basket.AddOrUpdateBasket(appleBasketItem);
            basket.AddOrUpdateBasket(appleBasketItem);

            IPromotionService promotionService = new PromotionService();
            promotionService.ApplyPromotionsToBasket(basket, promotions);

            Assert.AreEqual(1, basket.Basket.Count);
            Assert.AreEqual(2, basket.Total);

            Assert.IsTrue(basket.Basket[0].Discount != null);
            Assert.AreEqual("Apple", basket.Basket[0].ItemName);
            Assert.AreEqual(2, basket.Basket[0].ItemQuantity);
            Assert.AreEqual(1, basket.Basket[0].Discount.ThresholdQuantity);
            Assert.AreEqual(.5M, basket.Basket[0].Discount.DiscountAmount);
            Assert.AreEqual(1, basket.Basket[0].Discount.TotalDiscountAmount);
        }
    }
}
