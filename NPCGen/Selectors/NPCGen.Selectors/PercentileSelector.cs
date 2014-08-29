using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class PercentileSelector : IPercentileSelector
    {
        private IPercentileMapper percentileMapper;
        private IDice dice;

        public PercentileSelector(IPercentileMapper percentileMapper, IDice dice)
        {
            this.percentileMapper = percentileMapper;
            this.dice = dice;
        }

        public String SelectPercentileFrom(String tableName)
        {
            var table = percentileMapper.Map(tableName);
            var roll = dice.Roll().Percentile();
            return table[roll];
        }

        public IEnumerable<String> SelectAllResults(String tableName)
        {
            var table = percentileMapper.Map(tableName);
            return table.Values.Distinct();
        }
    }
}