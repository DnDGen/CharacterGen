using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Common.Races;
using NPCGen.Selectors.Interfaces;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common;

namespace NPCGen.Selectors
{
    public class LanguagesSelector : ILanguagesSelector
    {
        private ILanguagesMapper languagesXmlMapper;

        public LanguagesSelector(ILanguagesMapper languagesXmlMapper)
        {
            this.languagesXmlMapper = languagesXmlMapper;
        }

        public IEnumerable<String> GetAutomaticLanguagesFor(Race race)
        {
            var languages = languagesXmlMapper.Map("AutomaticLanguages.xml");

            var baseRaceLanguages = languages[race.BaseRace];
            var metaraceLanguages = languages[race.Metarace];

            return baseRaceLanguages.Union(metaraceLanguages);
        }

        public IEnumerable<String> GetBonusLanguagesFor(String baseRace, String className)
        {
            var languages = languagesXmlMapper.Map("BonusLanguages.xml");
            var bonusLanguages = languages[baseRace];

            if (className == CharacterClassConstants.Cleric)
                bonusLanguages = bonusLanguages.Union(new[] { LanguageConstants.Abyssal, LanguageConstants.Celestial, LanguageConstants.Infernal });
            else if (className == CharacterClassConstants.Wizard)
                bonusLanguages = bonusLanguages.Union(new[] { LanguageConstants.Draconic });
            else if (className == CharacterClassConstants.Druid)
                bonusLanguages = bonusLanguages.Union(new[] { LanguageConstants.Sylvan });

            return bonusLanguages;
        }
    }
}