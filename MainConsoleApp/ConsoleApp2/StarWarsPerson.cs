using System;

namespace ConsoleApp2
{
    public class StarWarsPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string ShipName { get; set; }
        public double Length { get; set; }
    }
}
