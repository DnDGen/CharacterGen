using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    //INFO: We are not using the base class here, as aquatic characters will never appear randomly (which assumes mostly on land)
    internal class AquaticBaseRaceRandomizer : RaceRandomizer
    {
        private readonly IAdjustmentsSelector adjustmentSelector;
        private readonly ICollectionsSelector collectionSelector;

        public AquaticBaseRaceRandomizer(IAdjustmentsSelector adjustmentSelector, ICollectionsSelector collectionSelector)
        {
            this.adjustmentSelector = adjustmentSelector;
            this.collectionSelector = collectionSelector;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var allowedBaseRaces = GetAllPossible(alignment, characterClass);
            if (allowedBaseRaces.Any() == false)
                throw new IncompatibleRandomizersException();

            return collectionSelector.SelectRandomFrom(allowedBaseRaces);
        }

        private bool RaceIsAllowed(string baseRace, Alignment alignment, CharacterClass characterClass)
        {
            return LevelAdjustmentIsAllowed(baseRace, characterClass.Level)
                && BaseRaceCanBeAlignment(baseRace, alignment);
        }

        private bool LevelAdjustmentIsAllowed(string baseRace, int level)
        {
            var levelAdjustment = adjustmentSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, baseRace);

            return levelAdjustment < level;
        }

        private bool BaseRaceCanBeAlignment(string baseRace, Alignment alignment)
        {
            var alignmentRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full);
            return alignmentRaces.Contains(baseRace);
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var aquaticBaseRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Aquatic);
            return aquaticBaseRaces.Where(r => RaceIsAllowed(r, alignment, characterClass));
        }
    }
}
