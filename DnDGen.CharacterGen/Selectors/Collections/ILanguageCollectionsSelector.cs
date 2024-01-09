using DnDGen.CharacterGen.Races;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Selectors.Collections
{
    internal interface ILanguageCollectionsSelector
    {
        IEnumerable<string> SelectAutomaticLanguagesFor(Race race, string className);
        IEnumerable<string> SelectBonusLanguagesFor(string baseRace, string className);
    }
}