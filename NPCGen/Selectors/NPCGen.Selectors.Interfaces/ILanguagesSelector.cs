using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces
{
    public interface ILanguagesSelector
    {
        IEnumerable<String> SelectAutomaticLanguagesFor(Race race);
        IEnumerable<String> SelectBonusLanguagesFor(String baseRace, String className);
    }
}