using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class NoMetaraceRandomizer : RaceRandomizer
    {
        public String Randomize(Alignment alignment, CharacterClass characterClass)
        {
            return RaceConstants.Metaraces.None;
        }

        public IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            return new[] { RaceConstants.Metaraces.None };
        }
    }
}