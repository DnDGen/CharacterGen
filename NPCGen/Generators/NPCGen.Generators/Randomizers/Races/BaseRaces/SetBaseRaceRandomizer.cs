using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : ISetBaseRaceRandomizer
    {
        public String SetBaseRaceId { get; set; }

        private INameSelector nameSelector;

        public SetBaseRaceRandomizer(INameSelector nameSelector)
        {
            this.nameSelector = nameSelector;
        }

        public NameModel Randomize(String goodness, CharacterClass characterClass)
        {
            var baseRace = new NameModel();
            baseRace.Id = SetBaseRaceId;
            baseRace.Name = nameSelector.Select(SetBaseRaceId);

            return baseRace;
        }

        public IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass)
        {
            return new[] { SetBaseRaceId };
        }
    }
}