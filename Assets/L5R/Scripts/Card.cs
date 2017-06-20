using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.L5R.Scripts
{
    // --------- Use Edit > Paste Special to create class --------- 

    [Serializable]
    public class Rootobject
    {
        public Card[] records { get; set; }
        public int size { get; set; }
        public bool success { get; set; }
        public DateTime last_updated { get; set; }
    }

    [Serializable]
    public class Card
    {
        public string clan_code { get; set; }
        public string code { get; set; }
        public int cost { get; set; }
        public Cycles cycles { get; set; }
        public string illustrator { get; set; }
        public int influence_cost { get; set; }
        public bool is_unique { get; set; }
        public string keywords { get; set; }
        public string military_strength_mod { get; set; }
        public string name { get; set; }
        public Packs packs { get; set; }
        public string political_strength_mod { get; set; }
        public string side { get; set; }
        public string text { get; set; }
        public string type_code { get; set; }
        public int glory { get; set; }
        public int military_strength { get; set; }
        public int political_strength { get; set; }
        public string province_strength_mod { get; set; }
        public string element_code { get; set; }
        public int province_strength { get; set; }
        public int fate { get; set; }
        public int honor { get; set; }
        public int influence_pool { get; set; }
    }

    [Serializable]
    public class Cycles
    {
        public int core { get; set; }
    }

    [Serializable]
    public class Packs
    {
        public int core { get; set; }
    }

}
