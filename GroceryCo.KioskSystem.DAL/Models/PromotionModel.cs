namespace GroceryCo.KioskSystem.DAL.Models
{
    public class PromotionModel
    {
        public int PromotionId { get; set; }
        public int ItemId { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
