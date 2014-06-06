using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Factories
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
                var rollString = String.Format("1d{0}-1", remainingBonusLanguages.Count);
                var roll = dice.Roll(rollString);
                var language = remainingBonusLanguages[roll];

                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }
    }
}