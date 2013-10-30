using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<String> GetBonusLanguagesFor(Race race)
        {
            throw new NotImplementedException();
        }
    }
}