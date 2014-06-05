using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;

namespace NPCGen.Core.Generation.Providers.Interfaces
{
    public interface ILanguageProvider
    {
        IEnumerable<String> GetAutomaticLanguagesFor(Race race);
        IEnumerable<String> GetBonusLanguagesFor(String baseRace, String className);
    }
}