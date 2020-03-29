using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Gui
    {
        const int Parkinglotcapacity = 100;  // Macro Substitude
                                            // 26 is the equvilant of 2 default ships (Char 0, Ship 0). used while debuging % for predictibility

        public static async Task InitiateDialogueAsync(RectangularPlatform parkingDeck)
        {
            parkingDeck.ShowCapacity();

            Console.WriteLine("What's your name?");
            var customerName = Console.ReadLine();


            Task<Character> taskLoadChar = new Task<Character>(function: () => ApiUtils.LoadCharacter(customerName));
            taskLoadChar.Start();

            
            // Connect to DB here while api is loading :desktop:
            //  establishDataBaseConnection();



            Character customer = new Character(); //Empty place holder

            var currentPilot = new StarWarsPerson
            {
                Name = customerName
            } ;
            



            if (DbUtils.IsDocked(currentPilot))
            {
                Logger.systemLog("You already have a ship here, do you want to check it out or swap it for another one? S = swap, E = check out ", ConsoleColor.DarkYellow);
                Logger.systemLog("S = swap, E = check out ", ConsoleColor.DarkYellow);
                string sAnswer = Console.ReadLine();
                //char cAnswer = Console.ReadLine();


                if (sAnswer[0] == 'e') 
                { 
                    parkingDeck.CheckoutShip(currentPilot);
                    return;
                }
           

                if (sAnswer[0] == 's')
                    parkingDeck.CheckoutShip(currentPilot);



                // switch (sAnswer[0])
                // {
                //     case 'e':
                //     case 'E':
                //         parkingDeck.CheckoutShip(currentPilot);
                //         break;
                //
                //     case 's':
                //     case 'S':
                //         parkingDeck.CheckoutShip(currentPilot);
                //         break;
                //         
                //     default:
                //         break;
                // }


            }

            customer = await taskLoadChar;

            if (customer.Exists)
            {
                var ship = PickVehicle(customer);
                customer.CurrentShipName = ship.name;
                currentPilot.ShipName = ship.name;
                currentPilot.Length = ship.length;

                if (parkingDeck.ShipWillFit(ship.length))
                {
                    if (customer.Wealth > parkingDeck.CalculateDockingFee(ship.length))
                        parkingDeck.DockShip(currentPilot);

                    else
                        Logger.systemLog("Sorry, you can't afford that");
                }

                else
                    Logger.systemLog("Your ship wont fit");

            }
            else
                Logger.systemLog("You do not have access to this garage");
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
                Logger.systemLog("Invalid choice choose again!");
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
