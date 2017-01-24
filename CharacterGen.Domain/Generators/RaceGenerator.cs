using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators
{
    internal class RaceGenerator : IRaceGenerator
    {
        private IBooleanPercentileSelector booleanPercentileSelector;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private Dice dice;

        private readonly IEnumerable<string> allSizes;
        private readonly IEnumerable<string> allClassTypes;

        public RaceGenerator(IBooleanPercentileSelector booleanPercentileSelector, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            Dice dice)
        {
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.dice = dice;

            allSizes = new[]
            {
                RaceConstants.Sizes.Colossal,
                RaceConstants.Sizes.Gargantuan,
                RaceConstants.Sizes.Huge,
                RaceConstants.Sizes.Large,
                RaceConstants.Sizes.Medium,
                RaceConstants.Sizes.Small,
                RaceConstants.Sizes.Tiny,
            };

            allClassTypes = new[]
            {
                CharacterClassConstants.TrainingTypes.Intuitive,
                CharacterClassConstants.TrainingTypes.SelfTaught,
                CharacterClassConstants.TrainingTypes.Trained,
            };
        }

        public Race GenerateWith(Alignment alignment, CharacterClass characterClass, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(alignment, characterClass);
            race.Metarace = metaraceRandomizer.Randomize(alignment, characterClass);
            race.MetaraceSpecies = DetermineMetaraceSpecies(alignment, race.Metarace);
            race.IsMale = DetermineIfMale(race.BaseRace, characterClass.Name);
            race.Size = DetermineSize(race.BaseRace);
            race.HasWings = DetermineIfRaceHasWings(race);
            race.LandSpeed = DetermineLandSpeed(race);
            race.AerialSpeed = DetermineAerialSpeed(race);
            race.Age = DetermineAge(race, characterClass);
            race.ChallengeRating = DetermineChallengeRating(race);

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, race.Gender);
            var baseHeight = adjustmentsSelector.SelectFrom(tableName, race.BaseRace);
            var heightModifier = RollModifier(race, TableNameConstants.Set.Collection.HeightRolls);

            race.HeightInInches = baseHeight + heightModifier;

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, race.Gender);
            var baseWeight = adjustmentsSelector.SelectFrom(tableName, race.BaseRace);
            var weightModifier = RollModifier(race, TableNameConstants.Set.Collection.WeightRolls);

            race.WeightInPounds = baseWeight + heightModifier * weightModifier;

            return race;
        }

        private int DetermineChallengeRating(Race race)
        {
            var baseRaceChallengeRating = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ChallengeRatings, race.BaseRace);
            var metaraceChallengeRating = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ChallengeRatings, race.Metarace);

            return baseRaceChallengeRating + metaraceChallengeRating;
        }

        private string DetermineMetaraceSpecies(Alignment alignment, string metarace)
        {
            if (metarace != RaceConstants.Metaraces.HalfDragon)
                return string.Empty;

            return collectionsSelector.SelectRandomFrom(TableNameConstants.Set.Collection.DragonSpecies, alignment.ToString());
        }

        private bool DetermineIfMale(string baseRace, string className)
        {
            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Wizard)
                return true;

            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Cleric)
                return false;

            return booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.Male);
        }

        private string DetermineSize(string baseRace)
        {
            var size = collectionsSelector.FindGroupOf(TableNameConstants.Set.Collection.BaseRaceGroups, baseRace, allSizes.ToArray());

            return size;
        }

        private bool DetermineIfRaceHasWings(Race race)
        {
            var baseRacesWithWings = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.HasWings);
            var metaracesWithWings = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.HasWings);

            if (baseRacesWithWings.Contains(race.BaseRace) || metaracesWithWings.Contains(race.Metarace))
                return true;

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return SizeIsAtLeast(race.Size, RaceConstants.Sizes.Large);

            return false;
        }

        private bool SizeIsAtLeast(string size, string atLeast)
        {
            //INFO: This works because sizes happen to be bigger as they go reverse-alphabetical.  If that changes, this will no longer work
            return size[0] <= atLeast[0];
        }

        private int DetermineLandSpeed(Race race)
        {
            var speed = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds, race.BaseRace);
            return speed;
        }

        private int DetermineAerialSpeed(Race race)
        {
            var metaraceAerialSpeed = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, race.Metarace);

            if (SpeedIsPreset(metaraceAerialSpeed))
                return metaraceAerialSpeed;

            var baseRaceAerialSpeed = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, race.BaseRace);

            if (SpeedIsPreset(baseRaceAerialSpeed))
                return baseRaceAerialSpeed;

            if (SpeedIsMultiplier(metaraceAerialSpeed) && race.HasWings)
                return race.LandSpeed * metaraceAerialSpeed;

            return 0;
        }

        private bool SpeedIsMultiplier(int speed)
        {
            return speed > 0 && speed % 10 != 0;
        }

        private bool SpeedIsPreset(int speed)
        {
            return speed > 0 && speed % 10 == 0;
        }

        private Age DetermineAge(Race race, CharacterClass characterClass)
        {
            var age = new Age();
            age.Maximum = GetMaximumAge(race);
            age.Years = GetAgeInYears(race, characterClass, age.Maximum);
            age.Stage = GetAgeStage(race, age.Years);

            return age;
        }

        private int GetMaximumAge(Race race)
        {
            var maximumAgeRoll = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, race.BaseRace).Single();
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, race.BaseRace);
            var ages = adjustmentsSelector.SelectAllFrom(tableName);

            var oldestAgeGroup = GetOldestAgeGroup(ages);
            return ages[oldestAgeGroup] + dice.Roll(maximumAgeRoll).AsSum();
        }

        private string GetOldestAgeGroup(Dictionary<string, int> ages)
        {
            return ages.OrderByDescending(kvp => kvp.Value).First().Key;
        }

        private int GetAgeInYears(Race race, CharacterClass characterClass, int maximumAge)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, race.BaseRace);
            var adultAge = adjustmentsSelector.SelectFrom(tableName, RaceConstants.Ages.Adulthood);

            var classType = GetClassType(characterClass);
            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, classType);
            var trainingAgeRoll = collectionsSelector.SelectFrom(tableName, race.BaseRace).Single();

            var startingAge = adultAge + dice.Roll(trainingAgeRoll).AsSum();
            var additionalAge = GetAdditionalAge(characterClass, classType, maximumAge, startingAge);
            var totalAge = startingAge + additionalAge;

            return Math.Min(maximumAge, totalAge);
        }

        private int GetAdditionalAge(CharacterClass characterClass, string classType, int maximumAge, int startingAge)
        {
            var additionalAgeDie = GetAdditionalAgeDie(classType, maximumAge, startingAge);
            if (additionalAgeDie < 1)
                return characterClass.Level;

            return dice.Roll(characterClass.Level).d(additionalAgeDie).AsSum();
        }

        private int GetAdditionalAgeDie(string classType, int maximumAge, int startingAge)
        {
            var totalCap = maximumAge - startingAge;

            switch (classType)
            {
                case CharacterClassConstants.TrainingTypes.Intuitive: return totalCap / 60;
                case CharacterClassConstants.TrainingTypes.SelfTaught: return totalCap / 30;
                case CharacterClassConstants.TrainingTypes.Trained: return totalCap / 20;
                default: throw new ArgumentException($"{classType} is not a valid class training type");
            }
        }

        private string GetAgeStage(Race race, int ageInYears)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, race.BaseRace);
            var ages = adjustmentsSelector.SelectAllFrom(tableName);

            if (ageInYears >= ages[RaceConstants.Ages.Venerable] && ages[RaceConstants.Ages.Venerable] != RaceConstants.Ages.Ageless)
                return RaceConstants.Ages.Venerable;

            if (ageInYears >= ages[RaceConstants.Ages.Old] && ages[RaceConstants.Ages.Old] != RaceConstants.Ages.Ageless)
                return RaceConstants.Ages.Old;

            if (ageInYears >= ages[RaceConstants.Ages.MiddleAge] && ages[RaceConstants.Ages.MiddleAge] != RaceConstants.Ages.Ageless)
                return RaceConstants.Ages.MiddleAge;

            return RaceConstants.Ages.Adulthood;
        }

        private string GetClassType(CharacterClass characterClass)
        {
            var classType = collectionsSelector.FindGroupOf(TableNameConstants.Set.Collection.ClassNameGroups, characterClass.Name, allClassTypes.ToArray());

            return classType;
        }

        private int RollModifier(Race race, string tableName)
        {
            var roll = collectionsSelector.SelectFrom(tableName, race.BaseRace).Single();
            return dice.Roll(roll).AsSum();
        }
    }
}