using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coder
{
    public class StarWarsPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryTime {get; set;}
        public DateTime ExitTime { get; set; }
        public string ShipName { get; set; }
        public double Length { get; set; }
    }
}
