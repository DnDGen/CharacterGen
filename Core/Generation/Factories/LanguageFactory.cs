using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers;

namespace NPCGen.Core.Generation.Factories
{
    public static class LanguageFactory
    {
        public static IEnumerable<String> CreateUsing(Race race, String className, IDice dice, Int32 intelligenceBonus)
        {
            var languages = new List<String>();
            var languagesProvider = ProviderFactory.CreateLanguagesProvider();

            var automaticLanguages = languagesProvider.GetAutomaticLanguagesFor(race);
            languages.AddRange(automaticLanguages);

            if (className == CharacterClassConstants.Druid)
                languages.Add(LanguageConstants.Druidic);

            var bonusLanguages = languagesProvider.GetBonusLanguagesFor(race.BaseRace, className);
            var remainingBonusLanguages = new List<String>(bonusLanguages.Except(languages));
            var numberOfLanguages = intelligenceBonus;

            while(numberOfLanguages-- > 0 && remainingBonusLanguages.Any())
            {
                var roll = 0;

                do roll = dice.d20(bonus: -1);
                while (roll >= remainingBonusLanguages.Count);

                var language = remainingBonusLanguages[roll];
                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }
    }
}