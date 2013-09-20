using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        public String BaseRace { get; set; }

        public String Randomize(Alignment alignment, String characterClass)
        {
            return BaseRace;
        }
    }
}