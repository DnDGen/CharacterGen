using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Providers
{
    public class LanguagesProvider : ILanguageProvider
    {
        private ILanguagesXmlParser languagesXmlParser;

        public LanguagesProvider(ILanguagesXmlParser languagesXmlParser)
        {
            this.languagesXmlParser = languagesXmlParser;
        }

        public IEnumerable<String> GetAutomaticLanguagesFor(Race race)
        {
            var languages = languagesXmlParser.Parse("AutomaticLanguages.xml");

            var baseRaceLanguages = languages[race.BaseRace];
            var metaraceLanguages = languages[race.Metarace];

            return baseRaceLanguages.Union(metaraceLanguages);
        }

        public IEnumerable<String> GetBonusLanguagesFor(String baseRace, String className)
        {
            var languages = languagesXmlParser.Parse("BonusLanguages.xml");
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