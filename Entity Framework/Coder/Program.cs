using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coder
{   
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new StarWarsDBContext();
            var Person = new StarWarsPerson
            {
                Id = 2,
                Name = "Luke",
                EntryTime = DateTime.Now,
                ExitTime = DateTime.Now,
                ShipName = "X-wing",
                Length = 22
            };
            context.person.Add(Person);
            context.SaveChanges();

        }
    }
}
