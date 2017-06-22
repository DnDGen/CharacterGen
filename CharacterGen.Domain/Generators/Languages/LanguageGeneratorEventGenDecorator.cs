using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Languages
{
    internal class LanguageGeneratorEventGenDecorator : ILanguageGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ILanguageGenerator innerGenerator;

        public LanguageGeneratorEventGenDecorator(ILanguageGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<string> GenerateWith(Race race, CharacterClass characterClass, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating language for {characterClass.Name} {race.Summary}");
            var languages = innerGenerator.GenerateWith(race, characterClass, abilities, skills);
            eventQueue.Enqueue("CharacterGen", $"Generated languages: {string.Join(", ", languages)}");

            return languages;
        }
    }
}
