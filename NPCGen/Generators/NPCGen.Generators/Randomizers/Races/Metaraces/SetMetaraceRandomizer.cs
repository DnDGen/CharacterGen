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
        public String SetMetaraceId { get; set; }

        private INameSelector nameSelector;

        public SetMetaraceRandomizer(INameSelector nameSelector)
        {
            this.nameSelector = nameSelector;
        }

        public NameModel Randomize(String goodness, CharacterClass characterClass)
        {
            var metarace = new NameModel();
            metarace.Id = SetMetaraceId;
            metarace.Name = nameSelector.Select(SetMetaraceId);

            return metarace;
        }

        public IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass)
        {
            return new[] { SetMetaraceId };
        }
    }
}