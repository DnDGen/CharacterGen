using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Data
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public Dictionary<String, Stat> Stats { get; set; }
        public IEnumerable<String> Languages { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Int32 HitPoints { get; set; }
        public String InterestingTrait { get; set; }
    }
}