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
            eventQueue.Enqueue("CharacterGen", $"Beginning additional feats generation for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of additional feats");

            return feats;
        }
    }
}
