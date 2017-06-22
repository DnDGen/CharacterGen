using CharacterGen.Abilities;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;
using System.Linq;

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
            eventQueue.Enqueue("CharacterGen", $"Generating racial feats for {race.Summary}");
            var feats = innerGenerator.GenerateWith(race, skills, abilities);

            var featNames = feats.Select(f => f.Name);
            eventQueue.Enqueue("CharacterGen", $"Generated racial feats: [{string.Join(", ", featNames)}]");

            return feats;
        }
    }
}
