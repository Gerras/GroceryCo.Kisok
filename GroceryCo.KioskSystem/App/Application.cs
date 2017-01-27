using System;
using GroceryCo.KioskSystem.AppDefinitions;
using GroceryCo.KioskSystem.Core.KioskDefinitions;
using GroceryCo.KioskSystem.Core.Models;
using log4net;

namespace GroceryCo.KioskSystem.App
{
    public class Application : IApplication
    {
        private readonly IGroceryKiosk _groceryKiosk;
        private readonly IUserInput _userInput;
        private readonly IFormatOutput _output;
        private readonly ILog _logger;

        public Application(IGroceryKiosk groceryKiosk, IUserInput userInput, IFormatOutput output, ILog logger)
        {
            _groceryKiosk = groceryKiosk;
            _userInput = userInput;
            _output = output;
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Welcome to the GroceryCo KioskSystem!");
                    Console.WriteLine("Press any key to begin a transcation or press 'q' to Quit!");

                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.KeyChar.Equals('q') || key.KeyChar.Equals('Q')) break;

                    string[] userBasket = _userInput.GetUserBasketItems();
                    BasketModel kioskBasket = _groceryKiosk.BeginTransaction(userBasket);

                    _output.OutputReceipt(kioskBasket);
                }
            }
            catch (Exception e)
            {
                _logger.Error($"An error has occured: {e.GetType()}", e);
                Console.WriteLine("A fatal error has occured. The Application will now close.");
                Console.ReadKey();
            }
            

        }
    }
}
