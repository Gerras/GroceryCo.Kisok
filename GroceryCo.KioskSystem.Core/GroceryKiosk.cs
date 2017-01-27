using System;
using System.Collections.Generic;
using System.Linq;
using GroceryCo.KioskSystem.Core.KioskDefinitions;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.DALDefinitions;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.Core
{
    public class GroceryKiosk : IGroceryKiosk
    {
        private readonly IKioskDataStore _dataStore;
        private readonly IPromotionService _promotionService;

        public GroceryKiosk(IKioskDataStore dataStore, IPromotionService promotionService)
        {
            _dataStore = dataStore;
            _promotionService = promotionService;
        }

        /// <summary>
        /// Begins the User Transaction for checking out goods at the grocery kiosk.
        /// </summary>
        /// <param name="userBasket"> Takes a list of items the user would like to purchase</param>
        /// <returns>A user basket with total price and discounts applied.</returns>
        public BasketModel BeginTransaction(string[] userBasket)
        {

            
            List<ItemModel> kioskItems = _dataStore.GetKioskItems();
            List<PromotionModel> kisokPromotions = _dataStore.GetKioskPromotions();
           var basket = GetItemAndAddOrUpdateBasket(userBasket, kioskItems);
            _promotionService.ApplyPromotionsToBasket(basket, kisokPromotions);
            return basket;
        }

        /// <summary>
        /// Takes the user input basket and adds the item to the GroceryKiosk BasketModel.
        /// </summary>
        /// <param name="userBasket"></param>
        /// <param name="kioskItems"></param>
        /// <returns></returns>
        private static BasketModel GetItemAndAddOrUpdateBasket(string[] userBasket, List<ItemModel> kioskItems)
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
