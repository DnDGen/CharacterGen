using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class AnimalBaseRaceRandomizer : IBaseRaceRandomizer
    {
        public IEnumerable<String> GetAllPossibles(String goodness, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
