using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRace : IBaseRaceRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;
        private ILevelAdjustmentsProvider levelAdjustmentProvider;

        public BaseBaseRace(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
            this.levelAdjustmentProvider = levelAdjustmentProvider;
        }

        public String Randomize(String goodness, CharacterClassPrototype prototype)
        {
            var results = GetAllPossibleResults(goodness, prototype);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}BaseRaces", goodness, prototype.ClassName);
            var baseRace = String.Empty;

            do baseRace = percentileResultProvider.GetPercentileResult(tableName);
            while (!results.Contains(baseRace));

            return baseRace;
        }

        private Boolean RaceIsAllowed(String baseRace, Int32 level)
        {
            return !String.IsNullOrEmpty(baseRace) && LevelAdjustmentIsAllowed(baseRace, level) && BaseRaceIsAllowed(baseRace);
        }

        private Boolean LevelAdjustmentIsAllowed(String baseRace, Int32 level)
        {
            var levelAdjustments = levelAdjustmentProvider.GetLevelAdjustments();
            return levelAdjustments[baseRace] < level;
        }

        protected abstract Boolean BaseRaceIsAllowed(String baseRace);

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype)
        {
            var tableName = String.Format("{0}{1}BaseRaces", goodness, prototype.ClassName);
            var baseRaces = percentileResultProvider.GetAllResults(tableName);
            return baseRaces.Where(r => RaceIsAllowed(r, prototype.Level));
        }
    }
}