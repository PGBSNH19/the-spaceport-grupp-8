using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Character
    {
        public string Name { get; set; }
        public List<string> OwnedShips { get; set; }
        public double Wealth { get; set; }
        public bool Exists { get; set; }
        public string CurrentShipName { get; set; }

        readonly Random random = new Random();

        public Character()
        {
            OwnedShips = new List<string>();
            Exists = false;
            Wealth = random.Next(0, 200);
            CurrentShipName = "";
        }


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


    }
}
