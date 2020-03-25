using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {


        //-----------------------------------------------------------------------------
        // Character
        //-----------------------------------------------------------------------------
        public class Character
        {
            public string sName;
            public List<string> vShips = new List<string>();
            public int iBill;
            public int iCoins;

            public Character()
            {
                Random random = new Random();
                iCoins = random.Next(0, 99);
            }
        }

        #region AUTO GENERATED CLASS FROM JSON
        //-----------------------------------------------------------------------------
        // Auto Generated
        //-----------------------------------------------------------------------------
        public class Result
        {
            public string            name           { get; set; }            //⚠ ERROR: Use union to make the proper name synonyms via shared memory	⚠		
            public string            height         { get; set; }
            public string            mass           { get; set; }
            public string            hair_color     { get; set; }
            public string            skin_color     { get; set; }
            public string            eye_color      { get; set; }
            public string            birth_year     { get; set; }
            public string            gender         { get; set; }
            public string            homeworld      { get; set; }
            public List<string>      films          { get; set; }
            public List<string>      species        { get; set; }
            public List<object>      vehicles       { get; set; }
            public List<string>      starships      { get; set; }
            public DateTime          created        { get; set; }
            public DateTime          edited         { get; set; }
            public string            url            { get; set; }
        }
        public class RootObject
        {
            public int              count           { get; set; }
            public string           next            { get; set; }
            public object           previous        { get; set; }
            public List<Result>     results         { get; set; }
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
                    character.vShips = parsedjson.results[i].starships;
                    vCharacters.Add(character);
                }
            }
            return vCharacters;
        }






        //-----------------------------------------------------------------------------
        // getShipDetails
        //-----------------------------------------------------------------------------
        public static void getShipDetails()
        {
            #region CURRENTLY USES CACHED DATA DUE TO DEBUG SPEED, RE-ENABLE ON RELEASE TO USE API
            //⚠ ERROR: Slow debugging, use cached file instead	⚠		
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

            ////// Get every char from API
            //RestClient client = new RestClient("https://swapi.co/api/");
            //RestRequest request = new RestRequest("starships/", DataFormat.Json);      // "Dataformat" = enum -> json, xml, none
            //string sContent = client.Execute(request).Content;
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
            /*⚠ ERROR: Un - comment ^   Comment Out -> */
            string sContent = File.ReadAllText("tempCacheShip.json");
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
            #endregion
        }




        //-----------------------------------------------------------------------------
        // Main
        //-----------------------------------------------------------------------------
        static void Main(string[] args)
        {
           #region CURRENTLY USES CACHED DATA, RE-ENABLE ON RELEASE TO USE API
            //⚠ ERROR: Slow debugging, use cached file instead	⚠		
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

            ////// Get every char from API
            //RestClient client = new RestClient("https://swapi.co/api/");
            //RestRequest request = new RestRequest("people/", DataFormat.Json);      // "Dataformat" = enum -> json, xml, none
            //string sContent = client.Execute(request).Content;
            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

           /*⚠ ERROR: Un - comment ^   Comment Out -> */ string sContent = File.ReadAllText("tempCache.json");

            //. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
            #endregion






            RootObject parsed_Json = JsonConvert.DeserializeObject<RootObject>(sContent);   // Pre-made tokenizer
            List<Character> vCharacters = convertToCharachterObject(parsed_Json);



        }
    }
}
