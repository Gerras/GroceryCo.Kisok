using System.Configuration;
using GroceryCo.KioskSystem.DAL.DALDefinitions;

namespace GroceryCo.KioskSystem.DAL
{
    public class DALConfigurationManager : IConfigurationManager
    {
        public string GetFilePathToPriceCatalog()
        {
            return ConfigurationManager.AppSettings["priceCatalog"];
        }

        public string GetFilePathToPromotionCatalog()
        {
            return ConfigurationManager.AppSettings["promotionCatalog"];
        }
    }
}
