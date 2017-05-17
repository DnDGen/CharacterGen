using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities.Feats
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

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Feat> racialFeats, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning class feats generation for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, stats, racialFeats, skills);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of class feats");

            return feats;
        }
    }
}
