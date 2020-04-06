using System;
using System.Linq;

namespace ConsoleApp2
{
    public class DbUtils
    {
        public static void AddNewCustomerDocking(StarWarsPerson person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            using var context = new StarWarsDbContext();
            person.EntryTime = DateTime.Now;
            context.Person.Add(person);
            context.SaveChanges();
        }

        public static bool IsDocked(StarWarsPerson person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            using var context = new StarWarsDbContext();
            return context.Person.Any(p => p.Name == person.Name && p.ExitTime == null);
        }

        public static StarWarsPerson CheckOutCustomer(StarWarsPerson person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            using var context = new StarWarsDbContext();
            var item = context.Person.FirstOrDefault(p => p.Name == person.Name && p.ExitTime == null);
            if (item != null)
            {
                item.ExitTime = DateTime.Now;
                context.SaveChanges();
            }

            return item;
        }

        public static double GetCurrentOccupiedSpace()
        {
            using var context = new StarWarsDbContext();
            
            return context.Person.Any(p => p.ExitTime == null)
                ? context.Person.Where(p => p.ExitTime == null).Sum(p => p.Length) 
                : 0.0;
        }
    }
}
