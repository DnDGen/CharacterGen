using CharacterGen.Common.Abilities.Feats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Common.Magics
{
    public class Animal
    {
        public String Type { get; set; }
        public Int32 HitPoints { get; set; }
        public Int32 ArmorClass { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Int32 Tricks { get; set; }

        public Animal()
        {
            Type = String.Empty;
            Feats = Enumerable.Empty<Feat>();
        }
    }
}