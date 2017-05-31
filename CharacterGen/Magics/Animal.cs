using CharacterGen.Abilities;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Magics
{
    public class Animal
    {
        public Race Race { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public Combat Combat { get; set; }
        public int Tricks { get; set; }

        public Animal()
        {
            Race = new Race();
            Skills = Enumerable.Empty<Skill>();
            Languages = Enumerable.Empty<string>();
            Feats = Enumerable.Empty<Feat>();
            Abilities = new Dictionary<string, Ability>();
            Combat = new Combat();
        }
    }
}