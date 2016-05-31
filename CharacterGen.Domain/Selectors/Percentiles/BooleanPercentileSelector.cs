using System;

namespace CharacterGen.Domain.Selectors.Percentiles
{
    internal class BooleanPercentileSelector : IBooleanPercentileSelector
    {
        private IPercentileSelector innerSelector;

        public BooleanPercentileSelector(IPercentileSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public bool SelectFrom(string tableName)
        {
            var value = innerSelector.SelectFrom(tableName);
            return Convert.ToBoolean(value);
        }
    }
}