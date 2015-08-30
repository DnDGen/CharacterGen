using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using RollGen;
using System;
using System.Linq;

namespace CharacterGen.Generators.Domain
{
    public class RaceGenerator : IRaceGenerator
    {
        private IBooleanPercentileSelector booleanPercentileSelector;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IDice dice;

        public RaceGenerator(IBooleanPercentileSelector booleanPercentileSelector, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IDice dice)
        {
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.dice = dice;
        }

        public Race GenerateWith(Alignment alignment, CharacterClass characterClass, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(alignment.Goodness, characterClass);
            race.Metarace = metaraceRandomizer.Randomize(alignment.Goodness, characterClass);
            race.MetaraceSpecies = DetermineMetaraceSpecies(alignment, race.Metarace);
            race.Male = DetermineIfMale(race.BaseRace, characterClass.ClassName);
            race.Size = DetermineSize(race.BaseRace);
            race.HasWings = DetermineIfRaceHasWings(race);
            race.LandSpeed = DetermineLandSpeed(race);
            race.AerialSpeed = DetermineAerialSpeed(race);
            race.Age = DetermineAge(race, characterClass);

            var gender = race.Male ? "Male" : "Female";
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.GENDERRACEHeights, gender, race.BaseRace);
            var heights = adjustmentsSelector.SelectFrom(tableName);
            var additionalHeight = dice.Roll(heights[AdjustmentConstants.Quantity]).d(heights[AdjustmentConstants.Die]);

            race.HeightInInches = heights[AdjustmentConstants.Base] + additionalHeight;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.GENDERRACEWeights, gender, race.BaseRace);
            var weights = adjustmentsSelector.SelectFrom(tableName);
            var additionalWeightMultiplier = dice.Roll(weights[AdjustmentConstants.Quantity]).d(weights[AdjustmentConstants.Die]);

            race.WeightInPounds = weights[AdjustmentConstants.Base] + additionalHeight * additionalWeightMultiplier;

            return race;
        }

        private String DetermineMetaraceSpecies(Alignment alignment, String metarace)
        {
            if (metarace != RaceConstants.Metaraces.HalfDragon)
                return String.Empty;

            return collectionsSelector.SelectRandomFrom(TableNameConstants.Set.Collection.DragonSpecies, alignment.ToString());
        }

        private Boolean DetermineIfMale(String baseRace, String className)
        {
            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Wizard)
                return true;

            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Cleric)
                return false;

            return booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male);
        }

        private String DetermineSize(String baseRace)
        {
            var largeRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large);
            if (largeRaces.Contains(baseRace))
                return RaceConstants.Sizes.Large;

            var smallRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Small);
            if (smallRaces.Contains(baseRace))
                return RaceConstants.Sizes.Small;

            return RaceConstants.Sizes.Medium;
        }

        private Boolean DetermineIfRaceHasWings(Race race)
        {
            if (race.Metarace == RaceConstants.Metaraces.None)
                return false;

            if (race.Metarace == RaceConstants.Metaraces.HalfCelestial || race.Metarace == RaceConstants.Metaraces.HalfFiend)
                return true;

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return race.Size == RaceConstants.Sizes.Large;

            return false;
        }

        private Int32 DetermineLandSpeed(Race race)
        {
            var speeds = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds);
            return speeds[race.BaseRace];
        }

        private Int32 DetermineAerialSpeed(Race race)
        {
            if (race.HasWings == false)
                return 0;

            if (race.Metarace == RaceConstants.Metaraces.HalfFiend)
                return race.LandSpeed;

            return race.LandSpeed * 2;
        }

        private Int32 DetermineAge(Race race, CharacterClass characterClass)
        {
            var ageGroup = GetAgeGroup(characterClass);
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, ageGroup, race.BaseRace);
            var ages = adjustmentsSelector.SelectFrom(tableName);
            var additionalAge = dice.Roll(ages[AdjustmentConstants.Quantity]).d(ages[AdjustmentConstants.Die]);

            return ages[AdjustmentConstants.Adulthood] + additionalAge;
        }

        private String GetAgeGroup(CharacterClass characterClass)
        {
            var intuitiveClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Intuitive);
            if (intuitiveClasses.Contains(characterClass.ClassName))
                return GroupConstants.Intuitive;

            var trainedClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Trained);
            if (trainedClasses.Contains(characterClass.ClassName))
                return GroupConstants.Trained;

            return GroupConstants.SelfTaught;
        }
    }
}