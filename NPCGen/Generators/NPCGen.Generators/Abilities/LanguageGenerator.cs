using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Abilities
{
    public class LanguageGenerator : ILanguageGenerator
    {
        private IDice dice;
        private ILanguageCollectionsSelector languagesSelector;

        public LanguageGenerator(IDice dice, ILanguageCollectionsSelector languagesSelector)
        {
            this.dice = dice;
            this.languagesSelector = languagesSelector;
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
                var index = dice.Roll().d(remainingBonusLanguages.Count) - 1;
                var language = remainingBonusLanguages[index];

                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }
    }
}