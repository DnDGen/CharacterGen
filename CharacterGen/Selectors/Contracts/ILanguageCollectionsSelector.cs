using System;
using System.Collections.Generic;
using CharacterGen.Common.Races;

namespace CharacterGen.Selectors
{
    public interface ILanguageCollectionsSelector
    {
        IEnumerable<String> SelectAutomaticLanguagesFor(Race race, String className);
        IEnumerable<String> SelectBonusLanguagesFor(String baseRace, String className);
    }
}