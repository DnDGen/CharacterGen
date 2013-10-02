using System;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        public String BaseRace { get; set; }

        public String Randomize(String goodnessString, String characterClass)
        {
            return BaseRace;
        }
    }
}