using System.Collections.Generic;
using GroceryCo.KioskSystem.Core.Models;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.Core.KioskDefinitions
{
    public interface IPromotionService
    {
        void ApplyPromotionsToBasket(BasketModel basket, List<PromotionModel> promotions);
    }
}
