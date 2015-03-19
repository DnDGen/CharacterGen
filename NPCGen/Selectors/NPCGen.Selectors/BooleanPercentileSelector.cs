using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class BooleanPercentileSelector : IBooleanPercentileSelector
    {
        private IPercentileSelector innerSelector;

        public BooleanPercentileSelector(IPercentileSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public Boolean SelectFrom(String tableName)
        {
            var value = innerSelector.SelectFrom(tableName);
            return Convert.ToBoolean(value);
        }
    }
}