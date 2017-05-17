using CharacterGen.Abilities.Skills;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
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

        public IEnumerable<string> GenerateWith(Race race, string className, int intelligenceBonus, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning language generation for {className} {race.Summary}");
            var languages = innerGenerator.GenerateWith(race, className, intelligenceBonus, skills);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of languages: {string.Join(", ", languages)}");

            return languages;
        }
    }
}
