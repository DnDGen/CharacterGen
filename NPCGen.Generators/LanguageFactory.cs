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
    public class LanguageFactory : ILanguageFactory
    {
        private IDice dice;
        private ILanguageProvider languagesProvider;

        public LanguageFactory(IDice dice, ILanguageProvider languagesProvider)
        {
            this.dice = dice;
            this.languagesProvider = languagesProvider;
        }

        public IEnumerable<String> CreateWith(Race race, String className, Int32 intelligenceBonus)
        {
            var languages = new List<String>();

            var automaticLanguages = languagesProvider.GetAutomaticLanguagesFor(race);
            languages.AddRange(automaticLanguages);

            if (className == CharacterClassConstants.Druid)
                languages.Add(LanguageConstants.Druidic);

            var bonusLanguages = languagesProvider.GetBonusLanguagesFor(race.BaseRace, className);
            var remainingBonusLanguages = new List<String>(bonusLanguages.Except(languages));
            var numberOfBonusLanguages = intelligenceBonus;

            while(numberOfBonusLanguages-- > 0 && remainingBonusLanguages.Any())
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