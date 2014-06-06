using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces
{
    public interface ILanguageProvider
    {
        IEnumerable<String> GetAutomaticLanguagesFor(Race race);
        IEnumerable<String> GetBonusLanguagesFor(String baseRace, String className);
    }
}