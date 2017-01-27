using System.Collections.Generic;
using System.Linq;
using GroceryCo.KioskSystem.Core.KioskDefinitions;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.Core
{
    public class PromotionService : IPromotionService
    {
        /// <summary>
        /// Takes a basket of items and a list of promotions and sees if any apply.
        /// If they apply it will add it to the item and subtract it from the total basket price.
        /// This only allows one Promotion per Item.
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="promotions"></param>
        public void ApplyPromotionsToBasket(BasketModel basket, List<PromotionModel> promotions)
        {
            foreach (var basketItem in basket.Basket)
            {
                var promotion = promotions.FirstOrDefault(p => p.ItemId == basketItem.ItemId);
                if (promotion == null) continue;

                var discount = new DiscountModel(promotion, basketItem.ItemPrice);
                decimal discountAmount = ApplyDiscountToBasketItems(basketItem, discount);
                basket.SubtractFromTotal(discountAmount);
            }
        }

        private static decimal ApplyDiscountToBasketItems(BasketItemModel basketItem, DiscountModel discount)
        {
            int basketQuantity = basketItem.ItemQuantity;

            if (basketQuantity >= discount.ThresholdQuantity)
            {
                basketItem.Discount = discount;
                while (basketQuantity >= discount.ThresholdQuantity)
                {
                    discount.TotalDiscountAmount += discount.DiscountAmount;
                    basketQuantity -= discount.ThresholdQuantity;
                }
            }

            return discount.TotalDiscountAmount;
        }
    }
}
