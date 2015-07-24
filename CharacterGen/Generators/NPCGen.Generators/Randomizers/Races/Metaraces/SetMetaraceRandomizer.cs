using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
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