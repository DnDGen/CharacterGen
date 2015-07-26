using System;

namespace CharacterGen.Selectors
{
    public interface IBooleanPercentileSelector
    {
        Boolean SelectFrom(String tableName);
    }
}