using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class NoMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Randomize(String goodnessString, String className)
        {
            return String.Empty;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, String className)
        {
            throw new NotImplementedException();
        }
    }
}