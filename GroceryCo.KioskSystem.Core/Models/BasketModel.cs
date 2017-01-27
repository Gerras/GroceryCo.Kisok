using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.KioskSystem.Core.Models
{
    public class BasketModel
    {
        public BasketModel()
        {
            Basket = new List<BasketItemModel>();
            Total = 0;
        }

        public List<BasketItemModel> Basket { get; }
        public decimal Total { get; private set; }

        public void SubtractFromTotal(decimal discountAmount)
        {
            Total -= discountAmount;
            if (Total < 0)
            {
                Total = 0;
            }
        }

        /// <summary>
        /// Checks to see if the basketItem is already in the basket. If it is it will update
        /// the existing item, otherwise it will create a new item in the basket. Either way
        /// it will update the basket total.
        /// </summary>
        /// <param name="basketItem"></param>
        public void AddOrUpdateBasket(BasketItemModel basketItem)
        {
            BasketItemModel existingBasketItem;
            if (ItemAlreadyInBasket(basketItem, out existingBasketItem))
            {
                existingBasketItem.ItemQuantity++;
                Total += existingBasketItem.ItemPrice;
            }
            else
            {
                Basket.Add(basketItem);
                Total += basketItem.ItemPrice;
            }
        }

        private bool ItemAlreadyInBasket(BasketItemModel basketItem, out BasketItemModel existingBasketItem)
        {
            existingBasketItem = Basket.FirstOrDefault(b => string.Equals(b.ItemName, basketItem.ItemName, StringComparison.CurrentCultureIgnoreCase));
            return existingBasketItem != null;
        }
    }
}
