using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Metarace { get; set; }

        public String Randomize(String goodnessString, String className)
        {
            return Metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(string goodness, string className)
        {
            throw new NotImplementedException();
        }
    }
}