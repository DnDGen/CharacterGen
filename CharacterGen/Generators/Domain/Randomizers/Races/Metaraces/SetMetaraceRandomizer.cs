using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : ISetMetaraceRandomizer
    {
        public String SetMetarace { get; set; }

        public String Randomize(Alignment alignment, CharacterClass characterClass)
        {
            return SetMetarace;
        }

        public IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            return new[] { SetMetarace };
        }
    }
}