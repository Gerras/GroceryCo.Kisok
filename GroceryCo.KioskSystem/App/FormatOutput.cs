using System;
using GroceryCo.KioskSystem.AppDefinitions;
using GroceryCo.KioskSystem.Core.Models;

namespace GroceryCo.KioskSystem
{
    public class FormatOutput : IFormatOutput
    {
        public void OutputReceipt(BasketModel basket)
        { 

            Console.WriteLine("**********************Receipt*********************");
            Console.WriteLine("{0,-10} {1,-10} {2,-15} {3, -15}", "Item", "Quantity", "Regular Price", "Discount");
            foreach (BasketItemModel item in basket.Basket)
            {
                if (item.Discount != null)
                {
                    Console.WriteLine("{0,-10} {1,-10} ${2,-14} - ${3,-15}", item.ItemName, item.ItemQuantity, item.ItemPrice, item.Discount.TotalDiscountAmount);
                } else
                {
                    Console.WriteLine("{0,-10} {1,-10} ${2,-15}", item.ItemName, item.ItemQuantity, item.ItemPrice);
                }
                
            }
            Console.WriteLine("{0,-39} ${1,-15}", "Total:", basket.Total);
            Console.WriteLine("**************************************************");
        }
    }
}
