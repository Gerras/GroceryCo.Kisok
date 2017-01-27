using GroceryCo.KioskSystem.Core.Models;

namespace GroceryCo.KioskSystem.AppDefinitions
{
    public interface IFormatOutput
    {
        void OutputReceipt(BasketModel basket);
    }
}
