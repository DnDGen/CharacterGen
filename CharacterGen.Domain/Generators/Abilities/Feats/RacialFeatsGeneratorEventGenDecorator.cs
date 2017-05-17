using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal class RacialFeatsGeneratorEventGenDecorator : IRacialFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IRacialFeatsGenerator innerGenerator;

        public RacialFeatsGeneratorEventGenDecorator(IRacialFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Feat> GenerateWith(Race race, IEnumerable<Skill> skills, Dictionary<string, Stat> stats)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning racial feats generation for {race.Summary}");
            var feats = innerGenerator.GenerateWith(race, skills, stats);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of racial feats");

            return feats;
        }
    }
}
