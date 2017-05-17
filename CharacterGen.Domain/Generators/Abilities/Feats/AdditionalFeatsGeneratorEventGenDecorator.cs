using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities.Feats
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

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning additional feats generation for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of additional feats");

            return feats;
        }
    }
}
