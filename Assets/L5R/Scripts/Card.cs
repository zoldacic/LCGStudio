using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.L5R.Scripts
{
    // --------- Use Edit > Paste Special to create class --------- 

    //[Serializable]
    //public class Rootobject
    //{
    //    //public Record[] records;
    //    public int size;
    //    public bool success;
    //}

    //[Serializable]
    //public class Record
    //{
    //    public string clan;
    //}

    [Serializable]
    public class Rootobject
    {
        public Record[] records;
        public int size;
        public bool success;
        public DateTime last_updated;
    }

    [Serializable]
    public class Record
    {
        public string clan;
        public string code;
        public int cost;
        public Cycles cycles;
        public string illustrator;
        public int influence_cost;
        public bool is_unique;
        public string keywords;
        public string military_strength_mod;
        public string name;
        public Packs packs;
        public string political_strength_mod;
        public string side;
        public string text;
        public string type;
        public int glory;
        public int military_strength;
        public int political_strength;
        public string province_strength_mod;
        public string element;
        public int province_strength;
        public int fate;
        public int honor;
        public int influence_pool;
    }

    [Serializable]
    public class Cycles
    {
        public int core;
    }

    [Serializable]
    public class Packs
    {
        public int core;
    }
}
