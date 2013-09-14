using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using System;
using System.Collections.Generic;

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

        public Character()
        {
            InitializeStats();

            Languages = new List<String>();
            Skills = new List<Skill>();
            Feats = new List<Feat>();
        }

        private void InitializeStats()
        {
            Stats = new Dictionary<String, Stat>();
            Stats.Add(StatConstants.Strength, new Stat());
            Stats.Add(StatConstants.Constitution, new Stat());
            Stats.Add(StatConstants.Dexterity, new Stat());
            Stats.Add(StatConstants.Intelligence, new Stat());
            Stats.Add(StatConstants.Wisdom, new Stat());
            Stats.Add(StatConstants.Charisma, new Stat());

            Stats[StatConstants.Strength].Name = StatConstants.Strength;
            Stats[StatConstants.Constitution].Name = StatConstants.Constitution;
            Stats[StatConstants.Dexterity].Name = StatConstants.Dexterity;
            Stats[StatConstants.Intelligence].Name = StatConstants.Intelligence;
            Stats[StatConstants.Wisdom].Name = StatConstants.Wisdom;
            Stats[StatConstants.Charisma].Name = StatConstants.Charisma;
        }
    }
}