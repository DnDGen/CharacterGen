using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
{
    internal class ClassFeatsGeneratorEventGenDecorator : IClassFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IClassFeatsGenerator innerGenerator;

        public ClassFeatsGeneratorEventGenDecorator(IClassFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Feat> racialFeats, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning class feats generation for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, abilities, racialFeats, skills);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of class feats");

            return feats;
        }
    }
}
