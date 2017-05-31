using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
{
    internal class FeatsGeneratorEventGenDecorator : IFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IFeatsGenerator innerGenerator;

        public FeatsGeneratorEventGenDecorator(IFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills, BaseAttack baseAttack)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning feats generation for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, abilities, skills, baseAttack);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of feats");

            return feats;
        }
    }
}
