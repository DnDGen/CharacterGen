using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Skills
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

        public IEnumerable<Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning skills generation for {characterClass.Summary} {race.Summary}");
            var skills = innerGenerator.GenerateWith(characterClass, race, abilities);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of skills");

            return skills;
        }
    }
}
