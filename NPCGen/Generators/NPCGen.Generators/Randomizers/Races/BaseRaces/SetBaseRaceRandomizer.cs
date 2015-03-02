using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : ISetBaseRaceRandomizer
    {
        public String SetBaseRace { get; set; }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            return SetBaseRace;
        }

        public IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass)
        {
            return new[] { SetBaseRace };
        }
    }
}