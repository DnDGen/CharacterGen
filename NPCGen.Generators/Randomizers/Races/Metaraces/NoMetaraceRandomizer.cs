using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Randomizers.Races.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NoMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Randomize(String goodnessString, CharacterClassPrototype prototype)
        {
            return String.Empty;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype)
        {
            return new[] { String.Empty };
        }
    }
}