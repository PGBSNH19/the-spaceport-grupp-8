using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Ship
    {


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






    }
}
