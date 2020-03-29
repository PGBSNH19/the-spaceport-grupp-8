using System;

namespace ConsoleApp2
{
    class Program
    {
        // Macro Substitute
        // 26 is the equivalent of 2 default ships (Char 0, Ship 0). used while debugging % for predictability
        const int Parkinglotcapacity = 100;  
        

        static void Main(string[] args)
        {

            RectangularPlatform southParkingDeck = new RectangularPlatform(Parkinglotcapacity);


            while (true)      //⚠ Gameloop	⚠		
            {
                Console.Clear();
                Gui.InitiateDialogue(southParkingDeck);   // major parking company, thus we can manage different places / parking decks.

                Logger.ShowSystemErrorText("Press enter to continue", ConsoleColor.DarkGreen);
                Console.ReadLine();
            }
        }
    }
}
