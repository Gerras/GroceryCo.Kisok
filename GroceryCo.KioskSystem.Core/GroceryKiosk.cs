using System.Collections.Generic;
using GroceryCo.KioskSystem.Core.Helpers;
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
           var basket = GroceryKioskHelper.GetItemAndAddOrUpdateBasket(userBasket, kioskItems);
            _promotionService.ApplyPromotionsToBasket(basket, kisokPromotions);
            return basket;
        }
    }
}
