using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaghettiTableRace
{
    public class Chopstick
    {
        public string Name { get; protected set; }

        public Chopstick(string name)
        {
            Name = name;
        }
    }
}
