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
            throw new NotImplementedException();
        }
    }
}