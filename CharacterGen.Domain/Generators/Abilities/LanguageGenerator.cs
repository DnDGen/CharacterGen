using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal class LanguageGenerator : ILanguageGenerator
    {
        private ILanguageCollectionsSelector languagesSelector;
        private ICollectionsSelector collectionsSelector;

        public LanguageGenerator(ILanguageCollectionsSelector languagesSelector, ICollectionsSelector collectionsSelector)
        {
            this.languagesSelector = languagesSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<string> GenerateWith(Race race, string className, int intelligenceBonus, IEnumerable<Skill> skills)
        {
            var languages = new List<string>();

            var automaticLanguages = languagesSelector.SelectAutomaticLanguagesFor(race, className);
            languages.AddRange(automaticLanguages);

            var bonusLanguages = languagesSelector.SelectBonusLanguagesFor(race.BaseRace, className);
            var remainingBonusLanguages = bonusLanguages.Except(languages).ToList();
            var numberOfBonusLanguages = intelligenceBonus;

            if (IsInterpreter(skills))
                numberOfBonusLanguages = Math.Max(1, intelligenceBonus + 1);

            if (numberOfBonusLanguages >= remainingBonusLanguages.Count)
            {
                languages.AddRange(remainingBonusLanguages);
                return languages;
            }

            while (numberOfBonusLanguages-- > 0 && remainingBonusLanguages.Any())
            {
                var language = collectionsSelector.SelectRandomFrom(remainingBonusLanguages);
                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }

        private bool IsInterpreter(IEnumerable<Skill> skills)
        {
            return skills.Any(s => s.Name == SkillConstants.Profession && s.Focus == SkillConstants.Foci.Profession.Interpreter);
        }
    }
}