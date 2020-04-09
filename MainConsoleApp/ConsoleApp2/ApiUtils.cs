using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleApp2
{
    public class ApiUtils
    {
        public static async Task<string> GetJsonFromApi()
        {
            // Get every char from API
            var client = new RestClient("https://swapi.co/api/");
            var request = new RestRequest("people/", DataFormat.Json);      // "Dataformat" = enum -> json, xml, none
            using var resultAsync = client.ExecuteAsync(request);
            var result = await resultAsync;
            return result.Content;
        }

        public static Character LoadCharacter(string customerName)
        {
            var result = GetJsonFromApi();


            var parsed_Json = JsonConvert.DeserializeObject<RootObject>(result.Result);   // Pre-made tokenizer
            var allCharacters = ConvertUtils.ConvertToCharacters(parsed_Json);

            var candidate = allCharacters.FirstOrDefault(a => a.Name == (customerName)) ?? new Character { Name = customerName };

            return candidate;
        }
    }
}
