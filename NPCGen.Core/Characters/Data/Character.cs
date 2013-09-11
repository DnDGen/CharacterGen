using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Data.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Data
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public Stats Stats { get; set; }
        public List<String> Languages { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Feat> Feats { get; set; }
    }
}