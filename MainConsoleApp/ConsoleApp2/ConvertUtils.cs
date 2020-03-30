using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ConvertUtils
    {

        public static List<Character> ConvertToCharacters(RootObject parsedjson)
        {
            var allCharacters = new List<Character>();

            foreach (var rootItem in parsedjson.results)
            {
                if (rootItem.starships.Count > 0)
                {
                    var character = new Character
                    {
                        Name = rootItem.name,
                        OwnedShips = rootItem.starships,
                        Exists = true
                    };

                    allCharacters.Add(character);
                }
            }

            return allCharacters;
        }
    }
}
