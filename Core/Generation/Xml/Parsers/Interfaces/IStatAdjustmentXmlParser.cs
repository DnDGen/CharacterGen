using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Xml.Parsers.Interfaces
{
    public interface IStatAdjustmentXmlParser
    {
        Dictionary<String, Int32> Parse(String tableName);
    }
}