using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Common.Abilities.Feats
{
    public class Feat
    {
        public String Name { get; set; }
        public IEnumerable<String> Foci { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }

        public Feat()
        {
            Name = String.Empty;
            Foci = Enumerable.Empty<String>();
            Frequency = new Frequency();
        }
    }
}