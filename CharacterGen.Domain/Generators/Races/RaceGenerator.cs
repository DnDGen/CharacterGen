﻿using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Races
{
    internal class RaceGenerator : IRaceGenerator
    {
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly Dice dice;
        private readonly Generator generator;

        private readonly IEnumerable<string> allSizes;
        private readonly IEnumerable<string> allClassTypes;

        public RaceGenerator(IPercentileSelector percentileSelector,
            ICollectionSelector collectionsSelector,
            IAdjustmentsSelector adjustmentsSelector,
            Dice dice,
            Generator generator)
        {
            this.percentileSelector = percentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.dice = dice;
            this.generator = generator;

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

        public Race GenerateWith(Alignment alignment, CharacterClass characterClass, RacePrototype racePrototype)
        {
            var race = new Race();

            race.BaseRace = racePrototype.BaseRace;
            race.Metarace = racePrototype.Metarace;
            race.MetaraceSpecies = DetermineMetaraceSpecies(alignment, race.Metarace);
            race.IsMale = DetermineIfMale(race.BaseRace, characterClass.Name);
            race.Size = DetermineSize(race.BaseRace);
            race.HasWings = DetermineIfRaceHasWings(race);
            race.LandSpeed = DetermineLandSpeed(race);
            race.AerialSpeed = DetermineAerialSpeed(race);
            race.SwimSpeed = DetermineSwimSpeed(race);
            race.ChallengeRating = DetermineChallengeRating(race);
            race.MaximumAge = DetermineMaximumAge(race);
            race.Age = DetermineAge(race, characterClass);

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, race.Gender);
            var baseHeight = adjustmentsSelector.SelectFrom(tableName, race.BaseRace);
            var heightModifier = RollModifier(race, TableNameConstants.Set.Collection.HeightRolls);

            race.Height.Value = baseHeight + heightModifier;
            race.Height.Description = GetHeightDescription(race, heightModifier);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, race.Gender);
            var baseWeight = adjustmentsSelector.SelectFrom(tableName, race.BaseRace);
            var weightModifier = RollModifier(race, TableNameConstants.Set.Collection.WeightRolls);

            race.Weight.Value = baseWeight + heightModifier * weightModifier;
            race.Weight.Description = GetWeightDescription(race, weightModifier);

            return race;
        }

        private string GetHeightDescription(Race race, int heightModifier)
        {
            return GetDescription(TableNameConstants.Set.Collection.HeightRolls, race, heightModifier, "Short", "Average", "Tall");
        }

        private string GetWeightDescription(Race race, int weightModifier)
        {
            return GetDescription(TableNameConstants.Set.Collection.WeightRolls, race, weightModifier, "Light", "Average", "Heavy");
        }

        private string GetDescription(string tableName, Race race, int modifier, params string[] descriptions)
        {
            var roll = collectionsSelector.SelectFrom(tableName, race.BaseRace).Single();
            var percentile = GetPercentile(roll, modifier);

            var rawIndex = percentile * descriptions.Length;
            rawIndex = Math.Floor(rawIndex);

            var index = Convert.ToInt32(rawIndex);
            index = Math.Min(index, descriptions.Count() - 1);

            return descriptions[index];
        }

        private double GetPercentile(string roll, double modifier)
        {
            var minimumRoll = dice.Roll(roll).AsPotentialMinimum();
            var maximumRoll = dice.Roll(roll).AsPotentialMaximum();
            var totalRange = maximumRoll - minimumRoll;
            var range = modifier - minimumRoll;

            if (totalRange > 0)
                return range / totalRange;

            return .5;
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

            return percentileSelector.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male);
        }

        private string DetermineSize(string baseRace)
        {
            var size = collectionsSelector.FindCollectionOf(TableNameConstants.Set.Collection.BaseRaceGroups, baseRace, allSizes.ToArray());

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

        private Measurement DetermineLandSpeed(Race race)
        {
            var measurement = new Measurement("feet per round");
            measurement.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds, race.BaseRace);

            return measurement;
        }

        private Measurement DetermineAerialSpeed(Race race)
        {
            var measurement = new Measurement("feet per round");
            var metaraceAerialSpeed = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, race.Metarace);
            var baseRaceAerialSpeed = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, race.BaseRace);

            if (SpeedIsPreset(metaraceAerialSpeed) && metaraceAerialSpeed > baseRaceAerialSpeed)
            {
                measurement.Value = metaraceAerialSpeed;
                measurement.Description = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, race.Metarace).Single();

                return measurement;
            }

            if (SpeedIsPreset(baseRaceAerialSpeed) && baseRaceAerialSpeed > metaraceAerialSpeed)
            {
                measurement.Value = baseRaceAerialSpeed;
                measurement.Description = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, race.BaseRace).Single();

                return measurement;
            }

            if (SpeedIsMultiplier(metaraceAerialSpeed) && race.HasWings)
            {
                measurement.Value = race.LandSpeed.Value * metaraceAerialSpeed;
                measurement.Description = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, race.Metarace).Single();

                return measurement;
            }

            return measurement;
        }

        private bool SpeedIsMultiplier(int speed)
        {
            return speed > 0 && speed % 10 != 0;
        }

        private bool SpeedIsPreset(int speed)
        {
            return speed > 0 && speed % 10 == 0;
        }

        private Measurement DetermineSwimSpeed(Race race)
        {
            var measurement = new Measurement("feet per round");
            measurement.Value = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SwimSpeeds, race.BaseRace);

            return measurement;
        }

        private Measurement GenerateAge(Race race, CharacterClass characterClass)
        {
            var age = new Measurement("Years");
            age.Value = GetAgeInYears(race, characterClass);
            age.Description = GetAgeDescription(race, age.Value);

            return age;
        }

        private Measurement GetDefaultAge(Race race, CharacterClass characterClass)
        {
            var age = new Measurement("Years");
            age.Value = race.MaximumAge.Value;
            age.Description = GetAgeDescription(race, age.Value);

            return age;
        }

        private Measurement DetermineMaximumAge(Race race)
        {
            var measurement = new Measurement("Years");
            measurement.Value = GetMaximumAge(race);

            if (measurement.Value == RaceConstants.Ages.Ageless)
                measurement.Description = "Immortal";
            else if (race.BaseRace == RaceConstants.BaseRaces.Pixie)
                measurement.Description = "Will return to their plane of origin";
            else
                measurement.Description = "Will die of natural causes";

            return measurement;
        }

        private Measurement DetermineAge(Race race, CharacterClass characterClass)
        {
            var age = generator.Generate(
                () => GenerateAge(race, characterClass),
                a => race.MaximumAge.Value >= a.Value || race.MaximumAge.Value == RaceConstants.Ages.Ageless,
                () => GetDefaultAge(race, characterClass),
                a => $"{a.Value} {a.Unit} is greater than maximum age of {race.MaximumAge.Value} {race.MaximumAge.Unit}",
                $"age for {race.Summary} {characterClass.Summary} of {race.MaximumAge.Value} years");

            return age;
        }

        private int GetMaximumAge(Race race)
        {
            var maximumAgeRoll = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, race.BaseRace).Single();

            if (maximumAgeRoll == RaceConstants.Ages.Ageless.ToString())
                return RaceConstants.Ages.Ageless;

            var undead = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);
            if (undead.Contains(race.Metarace))
                return RaceConstants.Ages.Ageless;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, race.BaseRace);
            var ages = adjustmentsSelector.SelectAllFrom(tableName);

            var oldestAgeGroup = GetOldestAgeGroup(ages);
            return ages[oldestAgeGroup] + dice.Roll(maximumAgeRoll).AsSum();
        }

        private string GetOldestAgeGroup(Dictionary<string, int> ages)
        {
            return ages.OrderByDescending(kvp => kvp.Value).First().Key;
        }

        private int GetAgeInYears(Race race, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, race.BaseRace);
            var adultAge = adjustmentsSelector.SelectFrom(tableName, RaceConstants.Ages.Adulthood);
            var additionalAge = GetAdditionalAge(race.BaseRace, characterClass);
            var ageInYears = adultAge + additionalAge;

            return ageInYears;
        }

        private int GetAdditionalAge(string baseRace, CharacterClass characterClass)
        {
            var classType = collectionsSelector.FindCollectionOf(TableNameConstants.Set.Collection.ClassNameGroups, characterClass.Name, allClassTypes.ToArray());
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, classType);
            var trainingAgeRoll = collectionsSelector.SelectFrom(tableName, baseRace).Single();
            var additionalAge = 0;

            for (var i = 0; i < characterClass.Level; i++)
            {
                additionalAge += dice.Roll(trainingAgeRoll).AsSum();
            }

            return additionalAge;
        }

        private string GetAgeDescription(Race race, int ageInYears)
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

        private int RollModifier(Race race, string tableName)
        {
            var roll = collectionsSelector.SelectFrom(tableName, race.BaseRace).Single();
            return dice.Roll(roll).AsSum();
        }

        public RacePrototype GeneratePrototype(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var prototype = new RacePrototype();
            prototype.BaseRace = baseRaceRandomizer.Randomize(alignmentPrototype, classPrototype);
            prototype.Metarace = metaraceRandomizer.Randomize(alignmentPrototype, classPrototype);

            return prototype;
        }
    }
}