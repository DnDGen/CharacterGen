using System;
using System.Collections.Generic;

namespace CharacterGen.Common.Magics
{
    public class Magic
    {
        //public Animal Animal { get; set; }
        public String Animal { get; set; }
        //public Dictionary<Int32, IEnumerable<String>> Spells { get; set; }
        public Dictionary<Int32, Int32> SpellsPerDay { get; set; }

        public Magic()
        {
            //Spells = new Dictionary<Int32, IEnumerable<String>>();
            SpellsPerDay = new Dictionary<Int32, Int32>();
            Animal = String.Empty;
        }
    }
}