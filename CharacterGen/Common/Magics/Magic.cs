using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Common.Magics
{
    public class Magic
    {
        public String Animal { get; set; }
        public IEnumerable<Spells> SpellsPerDay { get; set; }

        public Magic()
        {
            SpellsPerDay = Enumerable.Empty<Spells>();
            Animal = String.Empty;
        }
    }
}