using System;
using System.Collections.Generic;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : ISetBaseRaceRandomizer
    {
        public String SetBaseRace { get; set; }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            return SetBaseRace;
        }

        public IEnumerable<String> GetAllPossibles(String goodness, CharacterClass characterClass)
        {
            return new[] { SetBaseRace };
        }
    }
}