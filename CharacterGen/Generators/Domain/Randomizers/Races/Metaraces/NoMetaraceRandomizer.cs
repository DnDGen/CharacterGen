using System;
using System.Collections.Generic;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class NoMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Randomize(String goodnessString, CharacterClass characterClass)
        {
            return RaceConstants.Metaraces.None;
        }

        public IEnumerable<String> GetAllPossible(String goodness, CharacterClass characterClass)
        {
            return new[] { RaceConstants.Metaraces.None };
        }
    }
}