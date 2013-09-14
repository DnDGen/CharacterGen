using D20Dice.Dice;
using NPCGen.Core.Generation.Randomizers.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Core.Generation.Randomizers.Providers
{
    public class PercentileResultProvider : IPercentileResultProvider
    {
        private IPercentileXmlParser percentileXmlParser;
        private IDice dice;
        private List<PercentileObject> table;
        private String tableName;

        public PercentileResultProvider(IPercentileXmlParser percentileXmlParser, IDice dice)
        {
            this.percentileXmlParser = percentileXmlParser;
            this.dice = dice;
        }

        public String GetPercentileResult(String tableName)
        {
            if (this.tableName != tableName)
                CacheTable(tableName);

            var roll = dice.Percentile();
            var percentileObject = table.FirstOrDefault(o => RollIsInRange(roll, o));

            if (percentileObject == null)
                return String.Empty;

            return percentileObject.Content;
        }

        private void CacheTable(String tableName)
        {
            this.tableName = tableName;
            table = percentileXmlParser.Parse(tableName);
        }

        private Boolean RollIsInRange(Int32 roll, PercentileObject percentileObject)
        {
            return percentileObject.LowerLimit <= roll && percentileObject.UpperLimit >= roll;
        }
    }
}