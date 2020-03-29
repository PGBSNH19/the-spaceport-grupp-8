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


            
            // Connect to DB here while api is loading :desktop:
            //  establishDataBaseConnection();



            Character customer = ApiUtils.LoadCharacter(customerName);


            //Task<Character> taskLoadChar = new Task<Character>(function: () => ApiUtils.LoadCharacter(customerName));

            //Task<List<Ship.Result>> taskLoadShips = new Task<List<Ship.Result>>(function: () => loadAllVehiclesAsync(customer));

            var taskLoadShips = Task.Factory.StartNew(() => loadAllVehiclesAsync(customer));

            //Task<int> taskLoadShips = new Task<int>(function: ()=>loadAllVehiclesAsync(customer));
            //taskLoadShips.Start();

            //System.Console.WriteLine("FROM UI");   // should be called last but appear first. ✔

            var currentPilot = new StarWarsPerson
            {
                Name = customer.Name
            } ;
            



            if (DbUtils.IsDocked(currentPilot))
            {
                Logger.systemLog("You already have a ship here, do you want to check it out or swap it for another one? ", ConsoleColor.DarkYellow);
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


            if (customer.Exists)
            {
                List<Ship.Result> vShip = await await taskLoadShips; //"await await" <-- intellisense  WTF
               
                Console.WriteLine();
                Logger.systemLog("Which ship?", ConsoleColor.DarkYellow);


                for (int i = 0; i != vShip.Count; i++)
                {
                    Console.WriteLine(i + " - " + vShip[i].name);
                }
                int iAnswer = Convert.ToInt32(Console.ReadLine());

                Ship.Result ship = new Ship.Result();
                ship = vShip[iAnswer];

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


        public async static Task<List<Ship.Result>> loadAllVehiclesAsync(Character customer)
        {
            List<Ship.Result> allShipInfo = new List<Ship.Result>();

            for (int j = 0; j != customer.OwnedShips.Count; j++)
            {
                var shipInfo = Ship.GetShipDetails(customer.OwnedShips[j]);
                allShipInfo.Add(shipInfo);
            }

            //System.Console.WriteLine("FROM SHIP LOAD END");   // should be called first but appear last.
            return allShipInfo;
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
