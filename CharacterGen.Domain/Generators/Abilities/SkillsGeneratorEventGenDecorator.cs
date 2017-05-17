using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal class SkillsGeneratorEventGenDecorator : ISkillsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ISkillsGenerator innerGenerator;

        public SkillsGeneratorEventGenDecorator(ISkillsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning skills generation for {characterClass.Summary} {race.Summary}");
            var skills = innerGenerator.GenerateWith(characterClass, race, stats);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of skills");

            return skills;
        }
    }
}
