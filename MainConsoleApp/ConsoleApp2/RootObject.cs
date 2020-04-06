using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class RootObject
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Character.Result> results { get; set; }
    }
}
