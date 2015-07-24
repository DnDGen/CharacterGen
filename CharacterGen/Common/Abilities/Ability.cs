using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Common.Abilities
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