using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface IAdjustmentsSelector
    {
        Dictionary<string, int> SelectFrom(string tableName);
    }
}