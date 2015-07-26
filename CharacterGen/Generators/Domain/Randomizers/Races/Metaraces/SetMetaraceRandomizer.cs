using System;
using System.Collections.Generic;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : ISetMetaraceRandomizer
    {
        public String SetMetarace { get; set; }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            return SetMetarace;
        }

        public IEnumerable<String> GetAllPossible(String goodness, CharacterClass characterClass)
        {
            return new[] { SetMetarace };
        }
    }
}