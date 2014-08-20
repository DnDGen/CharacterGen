using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;

namespace NPCGen.Generators.Randomizers.Stats
{
    public class SetStatsRandomizer : ISetStatsRandomizer
    {
        public Int32 SetStrength { get; set; }
        public Int32 SetDexterity { get; set; }
        public Int32 SetConstitution { get; set; }
        public Int32 SetIntelligence { get; set; }
        public Int32 SetWisdom { get; set; }
        public Int32 SetCharisma { get; set; }

        public Dictionary<String, Stat> Randomize()
        {
            var stats = InitializeStats();

            stats[StatConstants.Charisma].Value = SetCharisma;
            stats[StatConstants.Constitution].Value = SetConstitution;
            stats[StatConstants.Dexterity].Value = SetDexterity;
            stats[StatConstants.Intelligence].Value = SetIntelligence;
            stats[StatConstants.Strength].Value = SetStrength;
            stats[StatConstants.Wisdom].Value = SetWisdom;

            return stats;
        }

        private Dictionary<String, Stat> InitializeStats()
        {
            var stats = new Dictionary<String, Stat>();

            stats[StatConstants.Charisma] = new Stat();
            stats[StatConstants.Constitution] = new Stat();
            stats[StatConstants.Dexterity] = new Stat();
            stats[StatConstants.Intelligence] = new Stat();
            stats[StatConstants.Strength] = new Stat();
            stats[StatConstants.Wisdom] = new Stat();

            return stats;
        }
    }
}