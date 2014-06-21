using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators
{
    public class LanguageGenerator : ILanguageGenerator
    {
        private IDice dice;
        private ILanguagesSelector languagesSelector;

        public LanguageGenerator(IDice dice, ILanguagesSelector languagesSelector)
        {
            this.dice = dice;
            this.languagesSelector = languagesSelector;
        }

        public IEnumerable<String> GenerateWith(Race race, String className, Int32 intelligenceBonus)
        {
            var languages = new List<String>();

            var automaticLanguages = languagesSelector.GetAutomaticLanguagesFor(race);
            languages.AddRange(automaticLanguages);

            if (className == CharacterClassConstants.Druid)
                languages.Add(LanguageConstants.Druidic);

            var bonusLanguages = languagesSelector.GetBonusLanguagesFor(race.BaseRace, className);
            var remainingBonusLanguages = bonusLanguages.Except(languages).ToList();
            var numberOfBonusLanguages = intelligenceBonus;

            if (numberOfBonusLanguages >= remainingBonusLanguages.Count)
            {
                languages.AddRange(remainingBonusLanguages);
                return languages;
            }

            while (numberOfBonusLanguages-- > 0 && remainingBonusLanguages.Any())
            {
                var indexRoll = String.Format("1d{0}-1", remainingBonusLanguages.Count);
                var roll = dice.Roll(indexRoll);
                var language = remainingBonusLanguages[roll];

                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }
    }
}