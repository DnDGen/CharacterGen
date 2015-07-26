using System;
using System.Collections.Generic;

namespace CharacterGen.Common.Magics
{
    public class Magic
    {
        public Familiar Familiar { get; set; }
        public Dictionary<Int32, IEnumerable<String>> Spells { get; set; }

        public Magic()
        {
            Familiar = new Familiar();
            Spells = new Dictionary<Int32, IEnumerable<String>>();
        }
    }
}