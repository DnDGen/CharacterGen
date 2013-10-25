using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : IBaseRaceRandomizer
    {
        public String BaseRace { get; set; }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            return BaseRace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            return new[] { BaseRace };
        }
    }
}