using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class NoMetaraceRandomizer : RaceRandomizer
    {
        public string Randomize(Alignment alignment, CharacterClassPrototype characterClass)
        {
            return RaceConstants.Metaraces.None;
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClassPrototype characterClass)
        {
            return new[] { RaceConstants.Metaraces.None };
        }
    }
}