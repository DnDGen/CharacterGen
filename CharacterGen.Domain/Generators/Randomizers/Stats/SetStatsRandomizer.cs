using CharacterGen.Abilities.Stats;
using CharacterGen.Randomizers.Stats;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Randomizers.Stats
{
    internal class SetStatsRandomizer : ISetStatsRandomizer
    {
        public int SetStrength { get; set; }
        public int SetDexterity { get; set; }
        public int SetConstitution { get; set; }
        public int SetIntelligence { get; set; }
        public int SetWisdom { get; set; }
        public int SetCharisma { get; set; }
        public bool AllowAdjustments { get; set; }

        public SetStatsRandomizer()
        {
            SetStrength = 10;
            SetDexterity = 10;
            SetConstitution = 10;
            SetIntelligence = 10;
            SetWisdom = 10;
            SetCharisma = 10;
            AllowAdjustments = true;
        }

        public Dictionary<string, Stat> Randomize()
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

        private Dictionary<string, Stat> InitializeStats()
        {
            var stats = new Dictionary<string, Stat>();

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