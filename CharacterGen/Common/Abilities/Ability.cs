using System;
using System.Collections.Generic;
using System.Linq;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;

namespace CharacterGen.Common.Abilities
{
    public class Ability
    {
        public Dictionary<String, Skill> Skills { get; set; }
        public IEnumerable<String> Languages { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Dictionary<String, Stat> Stats { get; set; }

        public Ability()
        {
            Skills = new Dictionary<String, Skill>();
            Languages = Enumerable.Empty<String>();
            Feats = Enumerable.Empty<Feat>();
            Stats = new Dictionary<String, Stat>();
        }
    }
}