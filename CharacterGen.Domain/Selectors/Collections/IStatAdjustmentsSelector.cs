using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface IStatAdjustmentsSelector
    {
        Dictionary<string, int> SelectFor(Race race);
    }
}