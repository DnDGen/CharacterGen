using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal class StatsGeneratorEventGenDecorator : IStatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IStatsGenerator innerGenerator;

        public StatsGeneratorEventGenDecorator(IStatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Dictionary<string, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning stats generation for {characterClass.Summary} {race.Summary}");
            var stats = innerGenerator.GenerateWith(statsRandomizer, characterClass, race);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of stats");

            return stats;
        }
    }
}
