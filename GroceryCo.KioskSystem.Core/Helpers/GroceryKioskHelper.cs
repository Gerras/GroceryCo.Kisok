using System;
using System.Collections.Generic;
using System.Linq;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.Core.Helpers
{
    public static class GroceryKioskHelper
    {
        /// <summary>
        /// Takes the user input basket and adds the item to the GroceryKiosk BasketModel.
        /// </summary>
        /// <param name="userBasket"></param>
        /// <param name="kioskItems"></param>
        /// <returns></returns>
        public static BasketModel GetItemAndAddOrUpdateBasket(string[] userBasket, List<ItemModel> kioskItems)
        {
            BasketModel basket = new BasketModel();
            foreach (var itemName in userBasket)
            {
                ItemModel item =
                    kioskItems.FirstOrDefault(
                        x => string.Equals(x.ItemName, itemName, StringComparison.CurrentCultureIgnoreCase));
                if (item == null) continue;

                BasketItemModel basketItem = new BasketItemModel(item);
                basket.AddOrUpdateBasket(basketItem);
            }

            return basket;
        }
    }
}
