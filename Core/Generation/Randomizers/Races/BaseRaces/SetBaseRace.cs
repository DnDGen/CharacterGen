using System;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class SetRace : IBaseRaceRandomizer
    {
        public String BaseRace { get; set; }

        public SetRace(String baseRace)
        {
            BaseRace = baseRace;
        }

        public String Randomize(Alignment alignment, String characterClass)
        {
            return BaseRace;
        }
    }
}