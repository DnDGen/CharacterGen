using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Abilities
{
    public class Ability
    {
        public Dictionary<string, Skill> Skills { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Dictionary<string, Stat> Stats { get; set; }

        public Ability()
        {
            Skills = new Dictionary<string, Skill>();
            Languages = Enumerable.Empty<string>();
            Feats = Enumerable.Empty<Feat>();
            Stats = new Dictionary<string, Stat>();
        }
    }
}