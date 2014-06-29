using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Common.Feats;
using NPCGen.Common.Races;
using NPCGen.Common.Skills;
using NPCGen.Common.Abilities;
using NPCGen.Common.Combats;

namespace NPCGen.Common
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public String InterestingTrait { get; set; }

        public Combat Combat { get; set; }

        public Dictionary<String, Skill> Skills { get; set; }
        public IEnumerable<String> Languages { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Dictionary<String, Stat> Stats { get; set; }

        public Equipment Equipment { get; set; }

        public Dictionary<Int32, IEnumerable<String>> Spells { get; set; }
        public Familiar Familiar { get; set; }

        public Character()
        {
            Alignment = new Alignment();
            Class = new CharacterClass();
            Race = new Race();
            InterestingTrait = String.Empty;

            Familiar = new Familiar();
            Feats = Enumerable.Empty<Feat>();
            Languages = Enumerable.Empty<String>();
            Skills = new Dictionary<String, Skill>();
            Stats = new Dictionary<String, Stat>();
            Spells = new Dictionary<Int32, IEnumerable<String>>();
            ArmorClass = new ArmorClass();
            SavingThrows = new SavingThrows();
            Equipment = new Equipment();
        }
    }
}