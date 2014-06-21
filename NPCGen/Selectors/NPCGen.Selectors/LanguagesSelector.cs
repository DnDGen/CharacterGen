using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class LanguagesSelector : ILanguagesSelector
    {
        private ICollectionsMapper collectionsMapper;

        public LanguagesSelector(ICollectionsMapper collectionsMapper)
        {
            this.collectionsMapper = collectionsMapper;
        }

        public IEnumerable<String> GetAutomaticLanguagesFor(Race race)
        {
            var languages = collectionsMapper.Map("AutomaticLanguages");

            var baseRaceLanguages = languages[race.BaseRace];
            var metaraceLanguages = languages[race.Metarace];

            return baseRaceLanguages.Union(metaraceLanguages);
        }

        public IEnumerable<String> GetBonusLanguagesFor(String baseRace, String className)
        {
            var languages = collectionsMapper.Map("BonusLanguages");
            var bonusLanguages = new List<String>(languages[baseRace]);

            if (className == CharacterClassConstants.Cleric)
            {
                bonusLanguages.Add(LanguageConstants.Abyssal);
                bonusLanguages.Add(LanguageConstants.Celestial);
                bonusLanguages.Add(LanguageConstants.Infernal);
            }
            else if (className == CharacterClassConstants.Wizard)
            {
                bonusLanguages.Add(LanguageConstants.Draconic);
            }
            else if (className == CharacterClassConstants.Druid)
            {
                bonusLanguages.Add(LanguageConstants.Sylvan);
            }

            return bonusLanguages.Distinct();
        }
    }
}