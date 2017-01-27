using CsvHelper.Configuration;
using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.DAL.CSVMappers
{
    public sealed class ItemModelCSVMapper : CsvClassMap<ItemModel>
    {
        public ItemModelCSVMapper()
        {
            Map(m => m.ItemId).Name("Id");
            Map(m => m.ItemName).Name("Name");
            Map(m => m.ItemPrice).Name("Price");
        }
    }
}
