using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Feats
{
    internal class AdditionalFeatsGeneratorEventGenDecorator : IAdditionalFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAdditionalFeatsGenerator innerGenerator;

        public AdditionalFeatsGeneratorEventGenDecorator(IAdditionalFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating additional feats for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            var featNames = feats.Select(f => f.Name);
            eventQueue.Enqueue("CharacterGen", $"Generated additional feats: [{string.Join(", ", featNames)}]");

            return feats;
        }
    }
}
