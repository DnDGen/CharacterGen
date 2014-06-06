using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Metarace { get; set; }

        public String Randomize(String goodnessString, CharacterClassPrototype prototype)
        {
            return Metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype)
        {
            return new[] { Metarace };
        }
    }
}