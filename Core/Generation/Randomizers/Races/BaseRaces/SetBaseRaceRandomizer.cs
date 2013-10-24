using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        public String BaseRace { get; set; }

        public String Randomize(String goodness, String className)
        {
            return BaseRace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, String className)
        {
            throw new NotImplementedException();
        }
    }
}