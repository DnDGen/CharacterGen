using DnDGen.CharacterGen.Races;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Selectors.Collections
{
    internal interface IAbilityAdjustmentsSelector
    {
        Dictionary<string, int> SelectFor(Race race);
    }
}