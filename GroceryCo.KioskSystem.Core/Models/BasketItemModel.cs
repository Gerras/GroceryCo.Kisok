using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.Core.Models
{
    public class BasketItemModel
    {

        public BasketItemModel(ItemModel item)
        {
            ItemId = item.ItemId;
            ItemName = item.ItemName;
            ItemPrice = item.ItemPrice;
            ItemQuantity = 1;
        }

        public int ItemId { get; private set; }
        public string ItemName { get; private set; }
        public decimal ItemPrice { get; private set; }
        public int ItemQuantity { get; set; }
        public DiscountModel Discount { get; set; }
    }
}
