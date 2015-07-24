using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces
{
    public interface ILanguageCollectionsSelector
    {
        IEnumerable<String> SelectAutomaticLanguagesFor(Race race, String className);
        IEnumerable<String> SelectBonusLanguagesFor(String baseRace, String className);
    }
}