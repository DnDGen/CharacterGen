using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : ISetMetaraceRandomizer
    {
        public String SetMetarace { get; set; }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}