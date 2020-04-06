using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp2
{
    class Ship
    {



        //-----------------------------------------------------------------------------
        // getShipDetails
        //-----------------------------------------------------------------------------
        public static Result getShipDetails(string sUrl)
        {
        
            RestClient client = new RestClient(sUrl);
            RestRequest request = new RestRequest("", DataFormat.Json);      // "Dataformat" = enum -> json, xml, none
            string sContent = client.Execute(request).Content;

            Result parsed_Json = JsonConvert.DeserializeObject<Result>(sContent);   // Pre-made tokenizer

            return parsed_Json;
        }

        public class Result
        {
            public string name { get; set; }
            public string model { get; set; }
            public string manufacturer { get; set; }
            public string cost_in_credits { get; set; }
            public double length { get; set; }               // was string
            public string max_atmosphering_speed { get; set; }
            public string crew { get; set; }
            public string passengers { get; set; }
            public string cargo_capacity { get; set; }
            public string consumables { get; set; }
            public string hyperdrive_rating { get; set; }
            public string MGLT { get; set; }
            public string starship_class { get; set; }
            public List<object> pilots { get; set; }
            public List<string> films { get; set; }
            public DateTime created { get; set; }
            public DateTime edited { get; set; }
            public string url { get; set; }
        }


    }
}
