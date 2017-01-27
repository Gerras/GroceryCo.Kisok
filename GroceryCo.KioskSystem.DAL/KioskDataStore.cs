using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.IO;
using GroceryCo.KioskSystem.DAL.Models;
using GroceryCo.KioskSystem.DAL.CSVMappers;
using GroceryCo.KioskSystem.DAL.DALDefinitions;

namespace GroceryCo.KioskSystem.DAL
{
    public class KioskDataStore : IKioskDataStore
    {
        private readonly IConfigurationManager _appSettings;

        public KioskDataStore(IConfigurationManager appSettings)
        {
            _appSettings = appSettings;
        }


        /// <summary>
        /// Gets the list of items that are stocked by the Grocery Kiosk.
        /// </summary>
        /// <returns>List of Kiosk Items.</returns>
        public List<ItemModel> GetKioskItems()
        {
            using (TextReader reader = File.OpenText(_appSettings.GetFilePathToPriceCatalog()))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<ItemModelCSVMapper>();
                var items = csv.GetRecords<ItemModel>().ToList();
                return items;
            }
        }

        /// <summary>
        /// Gets the list of promotions that are available to be applied as discounts to kiosk items.
        /// </summary>
        /// <returns>List of the Kiosk Promotions.</returns>
        public List<PromotionModel> GetKioskPromotions()
        {
            using (TextReader reader = File.OpenText(_appSettings.GetFilePathToPromotionCatalog()))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<PromotionModelCSVMapper>();
                var items = csv.GetRecords<PromotionModel>().ToList();
                return items;
            }
        }
    }
}
