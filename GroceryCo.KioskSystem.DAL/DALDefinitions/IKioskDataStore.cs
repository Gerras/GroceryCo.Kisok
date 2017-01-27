using System.Collections.Generic;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.DAL.DALDefinitions
{
    public interface IKioskDataStore
    {
        List<ItemModel> GetKioskItems();
        List<PromotionModel> GetKioskPromotions();
    }
}
