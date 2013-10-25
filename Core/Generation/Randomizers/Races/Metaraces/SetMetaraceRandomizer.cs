using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : IMetaraceRandomizer
    {
        public String Metarace { get; set; }

        public String Randomize(String goodnessString, CharacterClass characterClass)
        {
            return Metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            return new[] { Metarace };
        }
    }
}