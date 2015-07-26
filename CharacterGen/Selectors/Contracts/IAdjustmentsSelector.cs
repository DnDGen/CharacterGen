using System;
using System.Collections.Generic;

namespace CharacterGen.Selectors
{
    public interface IAdjustmentsSelector
    {
        Dictionary<String, Int32> SelectFrom(String tableName);
    }
}