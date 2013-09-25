using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Metarace { get; set; }

        public String Randomize(Alignment alignment, String className)
        {
            return Metarace;
        }
    }
}
