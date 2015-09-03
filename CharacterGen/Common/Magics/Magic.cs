using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Common.Magics
{
    public class Magic
    {
        public IEnumerable<Familiar> Animals { get; set; }
        public Dictionary<Int32, IEnumerable<String>> Spells { get; set; }

        public Magic()
        {
            Animals = Enumerable.Empty<Familiar>();
            Spells = new Dictionary<Int32, IEnumerable<String>>();
        }
    }
}