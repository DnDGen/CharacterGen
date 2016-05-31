using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface ILanguageCollectionsSelector
    {
        IEnumerable<string> SelectAutomaticLanguagesFor(Race race, string className);
        IEnumerable<string> SelectBonusLanguagesFor(string baseRace, string className);
    }
}