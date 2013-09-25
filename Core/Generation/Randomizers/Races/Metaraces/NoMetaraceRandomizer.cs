using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class NoMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Randomize(Alignment alignment, String className)
        {
            return String.Empty;
        }
    }
}