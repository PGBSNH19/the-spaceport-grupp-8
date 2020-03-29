using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Gui
    {
        const int Parkinglotcapacity = 100;  // Macro Substitude
                                            // 26 is the equvilant of 2 default ships (Char 0, Ship 0). used while debuging % for predictibility

        public static void InitiateDialogue(RectangularPlatform parkingDeck)
        {
            parkingDeck.ShowCapacity();

            Console.WriteLine("What's your name?");
            var customerName = Console.ReadLine();

            var customer = ApiUtils.LoadCharacter(customerName);

            var currentPilot = new StarWarsPerson
            {
                Name = customer.Name
            } ;
            
            if (DbUtils.IsDocked(currentPilot))
            {
                Logger.ShowSystemErrorText("You already have a ship here, do you want to check it out? Y/N");
                var answer = Console.ReadLine();
                if (answer.ToLower().Contains("y"))
                {
                    parkingDeck.CheckoutShip(currentPilot);
                    return;
                }
            }
            else if (customer.Exists)
            {
                var ship = PickVehicle(customer);
                customer.CurrentShipName = ship.name;
                currentPilot.ShipName = ship.name;
                currentPilot.Length = ship.length;

                if (parkingDeck.ShipWillFit(ship.length))
                {
                    if (customer.Wealth > parkingDeck.CalculateDockingFee(ship.length))
                    {
                        parkingDeck.DockShip(currentPilot);
                    }
                    else
                    {
                        Logger.ShowSystemErrorText("Sorry, you can't afford that");
                    }
                }
                else
                {
                    Logger.ShowSystemErrorText("Your ship wont fit");
                }

            }
            else
            {
                Logger.ShowSystemErrorText("You do not have access to this garage");
            }
        }


        public static Ship.Result PickVehicle(Character customer)
        {
            var allShipInfo = new List<Ship.Result>();

            Console.WriteLine("Which vehicles do you want to use?");

            for (int j = 0; j != customer.OwnedShips.Count; j++)
            {
                var shipInfo = Ship.GetShipDetails(customer.OwnedShips[j]);
                allShipInfo.Add(shipInfo);

                Console.WriteLine(j + " - " + allShipInfo[j].name);
            }


            var choice = -1;
            while (!GetValidChoice(customer.OwnedShips.Count, out choice))
            {
                Logger.ShowSystemErrorText("Invalid choice choose again!");
            }
            

            
            return allShipInfo[choice];
        }

        private static bool GetValidChoice(int ownedShipsCount, out int choice)
        {
            try
            {
                var res = Convert.ToInt32(Console.ReadLine());
                if (res < 0 || res >= ownedShipsCount)
                {
                    choice = -1;
                    return false;
                }
                choice = res;
                return true;
            }
            catch
            {
                choice = -1;
                return false;

            }
        }
    }
}
