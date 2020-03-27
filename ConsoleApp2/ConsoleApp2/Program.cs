using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {

        const int PARKINGLOTCAPACITY = 26;  // Macro Substitude
                                            // 26 is the equvilant of 2 default ships (Char 0, Ship 0). used while debuging % for predictibility


        //-----------------------------------------------------------------------------
        // Character
        //-----------------------------------------------------------------------------
        public class Character
        {
            public string sName;
            public List<string> vShips = new List<string>();
            public int iBill;           // unecasary
            public int iDrivingShipNumber;
            public double dWealth;
            public bool bValid = false;

            Random random = new Random();

            public Character()
            {
                dWealth = random.Next(0, 99);
            }


            public void assignShip(int i)
            {
                //iDrivingShipNumber = random.Next(0, i);                           //  |        ____                   ______
                //⚠ ERROR: Always use ship 0 while debugging, re-enable later	⚠	//	| GIT: " They should be able to select their starship on arrival in the application."
                iDrivingShipNumber = 0;
            }


        }

        #region AUTO GENERATED CLASS FROM JSON
        //-----------------------------------------------------------------------------
        // Auto Generated
        //-----------------------------------------------------------------------------
        public class Result
        {
            public string name { get; set; }            //⚠ ERROR: Use union to make the proper name synonyms via shared memory	⚠		
            public string height { get; set; }
            public string mass { get; set; }
            public string hair_color { get; set; }
            public string skin_color { get; set; }
            public string eye_color { get; set; }
            public string birth_year { get; set; }
            public string gender { get; set; }
            public string homeworld { get; set; }
            public List<string> films { get; set; }
            public List<string> species { get; set; }
            public List<object> vehicles { get; set; }
            public List<string> starships { get; set; }
            public DateTime created { get; set; }
            public DateTime edited { get; set; }
            public string url { get; set; }
        }
        public class RootObject
        {
            public int count { get; set; }
            public string next { get; set; }
            public object previous { get; set; }
            public List<Result> results { get; set; }
        }
        //------------------------END-AUTO---------------------------------------------
        #endregion




        /*
            -----------------------------------------------------------------------------
             Tokinze
            -----------------------------------------------------------------------------
            static public Character tokenize(ref string sContent) // (string& sContent)
            {
                Character character = new Character();   // Heap allocation requiered in C#
                string sSection = sContent.Substring(   sContent.IndexOf('{'),   sContent.IndexOf("},")+2   );
                Character.sName = sSection.Substring(sSection.IndexOf("name") +7, sSection.IndexOf(",") -10 );


                string sShips = sSection.Substring(sSection.IndexOf("starships") + 7, sSection.IndexOf(",") - 10);

                character.vShips.Add();

                sContent = sContent.Substring(sSection.Length, sContent.Length - sSection.Length);      // Remove read block from result to leave a clean slate for next run
                return character;
            }
        */


        //-----------------------------------------------------------------------------
        // convertToCharachterObject
        //-----------------------------------------------------------------------------
        public static List<Character> convertToCharachterObject(RootObject parsedjson)
        {
            List<Character> vCharacters = new List<Character>();

            //.count()
            for (int i = 0; i < parsedjson.results.Count; i++)
            {
                if (parsedjson.results[i].starships.Count != 0) // No ship? no way/need to get to the parking lot
                {
                    Character character = new Character();                // gets overwriten in vector (even though its heap allocated...), thus needs to be spam allocated in here
                    character.sName = parsedjson.results[i].name;
                    character.vShips = parsedjson.results[i].starships;   //        ____________
                    character.assignShip(character.vShips.Count);        // Give him one of his ships when he spawns
                    character.bValid = true;
                    vCharacters.Add(character);
                }
            }
            return vCharacters;
        }


        //-----------------------------------------------------------------------------
        // Rectangular Platform                                 .............................
        //      - endast fickparkerings platser, ie rectangle   ..   P    .   🚗   .  P   ..
        //                                                           ^
        //                                                          🚗
        //-----------------------------------------------------------------------------
        public class RectangularPlatform
        {
            double m_dLength;
            double m_procentageFiled;

            public RectangularPlatform(double x)
            {
                m_dLength = x;
            }

            //-----------------------------------------------------------------------------
            // calculateProcentage
            //-----------------------------------------------------------------------------
            public double calculateProcentage(double dShipLenght)
            {
                return dShipLenght / m_dLength * 100;
            }

            //-----------------------------------------------------------------------------
            // calculatePrice
            //-----------------------------------------------------------------------------
            public double calculatePrice(double dShipLenght)
            {
                return calculateProcentage(dShipLenght);    // How much % of the deck does the ship take? each % = +1kr
            }
            public static int Tax(long lEntry, long lExit)
            {
                int iTaxTime = Convert.ToInt32(lEntry - lExit);
                return iTaxTime;
            }

            //-----------------------------------------------------------------------------
            // dockShip
            //-----------------------------------------------------------------------------
            public void dockShip(double dShipLenght, Character character)
            {
                m_procentageFiled += calculateProcentage(dShipLenght);
                character.dWealth -= calculatePrice(dShipLenght);       // ? dont charge, just add bill to DB?
                //ADD CHAR TO SQL DATABASE
                systemLog("Docked Ship", ConsoleColor.Green);
                //ADD DATE.NOW TO ENTER IN DB
            }


            //-----------------------------------------------------------------------------
            // releaseShip
            //-----------------------------------------------------------------------------
            public void releaseShip(double dShipLenght)
            {
                m_procentageFiled -= calculateProcentage(dShipLenght);
                //ADD DATE.NOW TO EXIT IN DB
            }


            //-----------------------------------------------------------------------------
            // shipWillFit
            //-----------------------------------------------------------------------------
            public bool shipWillFit(double dShipLenght)
            {
                if (calculateProcentage(dShipLenght) + m_procentageFiled < 98)    // 2 margin of error. a ship cant be 2 anyhow
                    return true;

                return false;
            }


            //-----------------------------------------------------------------------------
            // showCapacity
            //-----------------------------------------------------------------------------
            public void showCapacity()
            {

                Console.WriteLine("Dock Capacity: ");
                char[] caCapacity = new char[100];

                for (int i = 0; i < caCapacity.Length; i++)
                {
                    caCapacity[i] = '-';
                }

                for (int i = 0; i < m_procentageFiled; i++)
                {
                    caCapacity[i] = '#';
                }
                string s = new string(caCapacity);
                systemLog(s, ConsoleColor.Gray);
            }



        }

        //-----------------------------------------------------------------------------
        // System Log 
        //-----------------------------------------------------------------------------
        static public void systemLog(string s, ConsoleColor consoleColorEnum = ConsoleColor.Red) // default red
        {
            Console.ForegroundColor = consoleColorEnum;
            Console.WriteLine(("-[" + s + "]-").ToUpper());
            Console.ForegroundColor = ConsoleColor.White;
        }


        //-----------------------------------------------------------------------------
        // Authorized 
        //  - Obsolete - inefficient
        //-----------------------------------------------------------------------------
        static public bool isAuthorized(Character character) // unesesary copying...
        {
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
            ////// Get every char from API
            //RestClient client = new RestClient("https://swapi.co/api/");
            //RestRequest request = new RestRequest("people/", DataFormat.Json);      // "Dataformat" = enum -> json, xml, none
            //string sContent = client.Execute(request).Content;

            /*⚠ ERROR: Un - comment ^   Comment Out -> */
            string sContent = File.ReadAllText("tempCache.json");
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .


            RootObject parsed_Json = JsonConvert.DeserializeObject<RootObject>(sContent);   // Pre-made tokenizer
            List<Character> vCharacters = convertToCharachterObject(parsed_Json);




            for (int i = 0; i != vCharacters.Count; i++)
            {
                if (vCharacters[i].sName == character.sName)
                    return true;
            }
            return false;


        }

        //-----------------------------------------------------------------------------
        // pickVehicle 
        //-----------------------------------------------------------------------------
        static public Ship.Result pickVehicle(Character character) // unesesary copying...
        {
            Ship.Result shipInfo = new Ship.Result();
            List<Ship.Result> vshipInfo = new List<Ship.Result>();

            Console.WriteLine("Which vehicles do you want to use?");

            for (int j = 0; j != character.vShips.Count; j++)
            {

                shipInfo = Ship.getShipDetails(character.vShips[j]);
                vshipInfo.Add(shipInfo);

                Console.WriteLine(j + " - " + vshipInfo[j].name);
            }

            int i = Convert.ToInt32(Console.ReadLine());
            return vshipInfo[i];
        }



        //-----------------------------------------------------------------------------
        // assignRandomVehicle 
        //-----------------------------------------------------------------------------
        static public Ship.Result assignRandomVehicle(Character character) // unesesary copying...
        {
            Ship.Result shipInfo = new Ship.Result();
            List<Ship.Result> vshipInfo = new List<Ship.Result>();

            Random random = new Random();


            int i = random.Next(0, character.vShips.Count);

            shipInfo = Ship.getShipDetails(character.vShips[i]);



            return shipInfo;
        }


        //-----------------------------------------------------------------------------
        // loadCharacters 
        //-----------------------------------------------------------------------------
        static public Character loadCharacters(string sCustomerName) // unesesary copying...
        {

            #region CURRENTLY USES CACHED DATA, RE-ENABLE ON RELEASE TO USE API
            //⚠ ERROR: Slow debugging, use cached file instead	⚠		
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

            ////// Get every char from API
            //RestClient client = new RestClient("https://swapi.co/api/");
            //RestRequest request = new RestRequest("people/", DataFormat.Json);      // "Dataformat" = enum -> json, xml, none
            //string sContent = client.Execute(request).Content;

            /*⚠ ERROR: Un - comment ^   Comment Out -> */
            string sContent = File.ReadAllText("tempCache.json");

            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
            #endregion

            RootObject parsed_Json = JsonConvert.DeserializeObject<RootObject>(sContent);   // Pre-made tokenizer
            List<Character> vCharacters = convertToCharachterObject(parsed_Json);

            int iAnswer = -1;
            for (int i = 0; i != vCharacters.Count; i++)
            {
                if (vCharacters[i].sName == sCustomerName)
                {
                    iAnswer = i;
                    break;
                }
            }

            if (iAnswer != -1)
                return vCharacters[iAnswer];

            else
            {
                // un ahtorized char
                Character character = new Character();
                character.sName = sCustomerName;
                return character;
            }
        }



        //-----------------------------------------------------------------------------
        // Main
        //-----------------------------------------------------------------------------
        static void Main(string[] args)
        {

            RectangularPlatform parkingDeck = new RectangularPlatform(PARKINGLOTCAPACITY);   // 26 is the equvilant of 2 default ships. use while debuging % for predictibility


            while (true)      //⚠ Gameloop	⚠		
            {
                Console.Clear();
                Thread.Sleep(1500);
                parkingDeck.showCapacity();


                Console.WriteLine("What's your name?");
                string sCustomerName = Console.ReadLine();
                Character cCustomer = loadCharacters(sCustomerName);

                if (cCustomer.bValid)
                {
                    //Ship.Result ship = pickVehicle(cCustomer);
                    Ship.Result ship = assignRandomVehicle(cCustomer);      // MAKE FUNCTION ASYNC!!! (GIT requirement)

                    #region OLD CRAP
                    // Pick a random person that wants to park
                    //Random random = new Random();
                    //int iPersonAproaching = /*random.Next(0, vCharacters.Count);*/ 0;       // ⚠ HardCoded while debuging, re-enable random later


                    //  GITHUB: "...be able to pay before they can leave the parking lot and get an invoice in the end." , No entry fee? Current wallet irrelevant?
                    //if (!  (parkingDeck.calculatePrice( Ship.getShipDetails(vCharacters[iPersonAproaching].vShips[iPersonAproaching]).length) > vCharacters[iPersonAproaching].iCoins)  )
                    #endregion

                    //if (isAuthorized(cCustomer))
                    if (cCustomer.bValid)
                    {                                                                    // replace 0 with iPersonAproaching
                        if (parkingDeck.shipWillFit(ship.length))
                        {
                            if (cCustomer.dWealth > parkingDeck.calculatePrice(ship.length))
                                parkingDeck.dockShip(ship.length, cCustomer);

                            else
                                systemLog("Sorry, you can't afford that");
                        }
                        else
                            systemLog("Your ship wont fit");
                    }
                }

                else
                    systemLog("You do not have acces to this garage");








                #region NOW TEMPORARYLY CACHES (using) SHIP FROM 'pickVehicle' WHILE DIALOUGE IS GOING ON

                //while (true)
                //{
                //    if(isAuthorized(vCharacters, sCustomerName))
                //    {                                                                    // replace 0 with iPersonAproaching
                //        if (parkingDeck.shipWillFit(      Ship.getShipDetails( vCharacters[0].vShips[  vCharacters[0].iDrivingShipNumber   ]    ).length    )    )      
                //        {
                //            if (vCharacters[0].dWealth > parkingDeck.calculatePrice(Ship.getShipDetails(vCharacters[0].vShips[vCharacters[0].iDrivingShipNumber]).length))
                //            {
                //                parkingDeck.dockShip(Ship.getShipDetails(vCharacters[0].vShips[vCharacters[0].iDrivingShipNumber]).length,  vCharacters[0] );
                //            }
                //            else
                //            {
                //               systemLog("Sorry, you can't afford that");
                //            }
                //        }
                //        else
                //        {
                //           systemLog("Parkinglot is full, please come back later");
                //        }
                //    }
                //    else
                //    {
                //       systemLog("You do not have acces to this garage");
                //    }
                //
                //}

                #endregion

            }
        }
    }
}
