using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NoMetaraceRandomizer : IMetaraceRandomizer
    {
        public NameModel Randomize(String goodnessString, CharacterClass characterClass)
        {
            return new NameModel { Id = RaceConstants.Metaraces.NoneId, Name = RaceConstants.Metaraces.None };
        }

        public IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass)
        {
            return new[] { RaceConstants.Metaraces.NoneId };
        }
    }
}