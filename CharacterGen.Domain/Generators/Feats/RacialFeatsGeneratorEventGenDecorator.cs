using CharacterGen.Abilities;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
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

        public IEnumerable<Feat> GenerateWith(Race race, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning racial feats generation for {race.Summary}");
            var feats = innerGenerator.GenerateWith(race, skills, abilities);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of racial feats");

            return feats;
        }
    }
}
