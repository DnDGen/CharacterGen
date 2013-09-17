using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Data
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public Dictionary<String, Stat> Stats { get; set; }
        public List<String> Languages { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Feat> Feats { get; set; }
    }
}