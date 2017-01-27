using CsvHelper.Configuration;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.DAL.CSVMappers
{
    public sealed class PromotionModelCSVMapper : CsvClassMap<PromotionModel>
    {
        public PromotionModelCSVMapper()
        {
            Map(m => m.PromotionId).Name("Id");
            Map(m => m.ItemId).Name("ItemId");
            Map(m => m.SalePrice).Name("SalePrice");
            Map(m => m.Description).Name("Description");
            Map(m => m.Quantity).Name("Quantity");
        }
    }
}
