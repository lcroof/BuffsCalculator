using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuffsCalculator
{
    public class Events
    {
        public decimal timestamp;
        public string type;
        public decimal sourceID;
        public decimal targetID;
        public Ability ability;
        public decimal hitType;
        public decimal amount;
        public bool multistrike;
    }

    public class FFLogAPIEventModel
    {
        public List<Events> events;
    }

    public class Ability
    {
        public string name;
        public decimal guid;
        public string type;
    }
}
