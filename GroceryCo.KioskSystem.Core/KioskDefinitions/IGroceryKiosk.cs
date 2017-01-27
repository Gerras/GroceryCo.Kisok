using GroceryCo.KioskSystem.Core.Models;

namespace GroceryCo.KioskSystem.Core.KioskDefinitions
{
    public interface IGroceryKiosk
    {
        BasketModel BeginTransaction(string[] userBasket);
    }
}
