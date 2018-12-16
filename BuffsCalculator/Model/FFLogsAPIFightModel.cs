using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuffsCalculator
{
    public class FFLogsAPIFightModel
    {
        public List<Fights> fights;
        public List<Friendlies> friendlies;
        public List<Enemies> enemies;
        public List<FriendlyPets> friendlyPets;
        public string title;
        public string start;
        public string end;
        public string zone;
    }

    public class Fights
    {
        public decimal id;
        public string start_time;
        public string end_time;
        public decimal boss;
        public string name;
        public decimal zoneID;
        public string zoneName;
    }

    public class Friendlies
    {
        public string name;
        public decimal id;
        public decimal guid;
        public string type;
    }

    public class Enemies
    {
        public string name;
        public decimal id;
        public decimal guid;
        public string type;
    }

    public class FriendlyPets
    {
        public string name;
        public decimal id;
        public decimal guid;
        public string type;
        public string petOwner;
    }
}
