using System;
using System.IO;
using GroceryCo.KioskSystem.AppDefinitions;

namespace GroceryCo.KioskSystem.App
{
    public class UserInput : IUserInput
    {
        public string[] GetUserBasketItems()
        {
            while(true)
            {
                Console.WriteLine("Please enter file location for your basket:");
                string basketFileLocation = Console.ReadLine();
                if(!string.IsNullOrEmpty(basketFileLocation))
                {
                    string[] lines;
                    try
                    {
                        lines = File.ReadAllLines(basketFileLocation);
                    } catch (Exception e)
                    {
                        //log it
                        Console.WriteLine("Could not open file from supplied path.");
                        continue;
                    }
                    
                    return lines;
                }
                Console.WriteLine("Could not open file from supplied path.");
            }
        }
    }
}
