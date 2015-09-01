using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Abilities
{
    public class LanguageGenerator : Generator, ILanguageGenerator
    {
        private ILanguageCollectionsSelector languagesSelector;
        private ICollectionsSelector collectionsSelector;

        public LanguageGenerator(ILanguageCollectionsSelector languagesSelector, ICollectionsSelector collectionsSelector)
        {
            this.languagesSelector = languagesSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<String> GenerateWith(Race race, String className, Int32 intelligenceBonus)
        {
            var languages = new List<String>();

            var automaticLanguages = languagesSelector.SelectAutomaticLanguagesFor(race, className);
            languages.AddRange(automaticLanguages);

            var bonusLanguages = languagesSelector.SelectBonusLanguagesFor(race.BaseRace, className);
            var remainingBonusLanguages = bonusLanguages.Except(languages).ToList();
            var numberOfBonusLanguages = intelligenceBonus;

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
    }
}