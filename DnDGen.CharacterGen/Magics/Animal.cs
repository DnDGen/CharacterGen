using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Combats;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Magics
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