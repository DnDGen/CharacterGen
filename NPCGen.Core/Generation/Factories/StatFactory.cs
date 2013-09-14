using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Factories
{
    public class StatFactory : IStatFactory
    {
        public IStatRandomizer statRandomizer { get; set; }

        public Dictionary<String, Stat> Generate()
        {
            var stats = new Dictionary<String, Stat>();
            stats.Add(StatConstants.Strength, new Stat());
            stats.Add(StatConstants.Constitution, new Stat());
            stats.Add(StatConstants.Dexterity, new Stat());
            stats.Add(StatConstants.Intelligence, new Stat());
            stats.Add(StatConstants.Wisdom, new Stat());
            stats.Add(StatConstants.Charisma, new Stat());

            foreach (var kvp in stats)
            {
                var stat = kvp.Value;
                stat.Name = kvp.Key;
                stat.Value = statRandomizer.Randomize();
            }

            return stats;
        }
    }
}