using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : ISetBaseRaceRandomizer
    {
        public String SetBaseRace { get; set; }

        public String Randomize(Alignment alignment, CharacterClass characterClass)
        {
            return SetBaseRace;
        }

        public IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            return new[] { SetBaseRace };
        }
    }
}