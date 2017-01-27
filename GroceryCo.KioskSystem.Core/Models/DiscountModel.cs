using GroceryCo.KioskSystem.DAL.Models;

namespace GroceryCo.KioskSystem.Core.Models
{
    public class DiscountModel
    {
        public DiscountModel(PromotionModel promotion, decimal regularItemPrice)
        {
            ThresholdQuantity = promotion.Quantity;
            decimal normalPrice = ThresholdQuantity * regularItemPrice;
            DiscountAmount = normalPrice - promotion.SalePrice;
        }

        public int ThresholdQuantity { get; }
        public decimal DiscountAmount { get; private set; }
        public decimal TotalDiscountAmount { get; set; }
    }
}
