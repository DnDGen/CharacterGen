using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;

namespace NPCGen.Common
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public Int32 ArmorClass { get; set; }
        public CharacterClass Class { get; set; }
        public Familiar Familiar { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Int32 HitPoints { get; set; }
        public String InterestingTrait { get; set; }
        public IEnumerable<String> Languages { get; set; }
        public Race Race { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public Dictionary<String, Stat> Stats { get; set; }
    }
}