using System;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryCo.KioskSystem.Tests
{
    [TestClass]
    public class DiscountModelTests
    {
        [TestMethod]
        public void TestDiscountModelCreation()
        {
            PromotionModel promotion = new PromotionModel
            {
                ItemId = 1,
                Description = "TEST",
                PromotionId = 1,
                Quantity = 2,
                SalePrice = 2M
            };

            DiscountModel discount = new DiscountModel(promotion, 1.5M);
            Assert.AreEqual(1.0M, discount.DiscountAmount);
            Assert.AreEqual(2, discount.ThresholdQuantity);
            Assert.AreEqual(0, discount.TotalDiscountAmount);
        }
    }
}
