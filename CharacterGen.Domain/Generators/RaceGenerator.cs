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

        public RaceGenerator(IBooleanPercentileSelector booleanPercentileSelector, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            Dice dice)
        {
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.dice = dice;
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

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, race.Gender);
            var baseHeights = adjustmentsSelector.SelectFrom(tableName);
            var heightModifier = GetModifier(race, TableNameConstants.Set.Collection.HeightRolls);
            race.HeightInInches = baseHeights[race.BaseRace] + heightModifier;

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, race.Gender);
            var baseWeights = adjustmentsSelector.SelectFrom(tableName);
            var weightModifier = GetModifier(race, TableNameConstants.Set.Collection.WeightRolls);
            race.WeightInPounds = baseWeights[race.BaseRace] + heightModifier * weightModifier;

            return race;
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
            var largeRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large);
            if (largeRaces.Contains(baseRace))
                return RaceConstants.Sizes.Large;

            var smallRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Small);
            if (smallRaces.Contains(baseRace))
                return RaceConstants.Sizes.Small;

            return RaceConstants.Sizes.Medium;
        }

        private bool DetermineIfRaceHasWings(Race race)
        {
            if (race.Metarace == RaceConstants.Metaraces.None)
                return false;

            if (race.Metarace == RaceConstants.Metaraces.HalfCelestial || race.Metarace == RaceConstants.Metaraces.HalfFiend)
                return true;

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return race.Size == RaceConstants.Sizes.Large;

            return false;
        }

        private int DetermineLandSpeed(Race race)
        {
            var speeds = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds);
            return speeds[race.BaseRace];
        }

        private int DetermineAerialSpeed(Race race)
        {
            if (race.Metarace == RaceConstants.Metaraces.Ghost)
                return 30;

            if (race.HasWings == false)
                return 0;

            if (race.Metarace == RaceConstants.Metaraces.HalfFiend)
                return race.LandSpeed;

            return race.LandSpeed * 2;
        }

        private Age DetermineAge(Race race, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, race.BaseRace);
            var ages = adjustmentsSelector.SelectFrom(tableName);

            var age = new Age();
            age.Maximum = GetMaximumAge(race, ages);
            age.Years = GetAgeInYears(race, characterClass, ages, age.Maximum);
            age.Stage = GetAgeStage(age.Years, ages);

            return age;
        }

        private int GetMaximumAge(Race race, Dictionary<string, int> ages)
        {
            var maximumAgeRoll = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, race.BaseRace).Single();

            return ages[RaceConstants.Ages.Venerable] + dice.Roll(maximumAgeRoll).AsSum();
        }

        private int GetAgeInYears(Race race, CharacterClass characterClass, Dictionary<string, int> ages, int maximumAge)
        {
            var classType = GetClassType(characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, classType);
            var trainingAgeRoll = collectionsSelector.SelectFrom(tableName, race.BaseRace).Single();

            var startingAge = ages[RaceConstants.Ages.Adulthood] + dice.Roll(trainingAgeRoll).AsSum();
            var additionalAge = GetAdditionalAge(race, characterClass, classType, maximumAge, ages, startingAge);
            var totalAge = startingAge + additionalAge;

            return Math.Min(maximumAge, totalAge);
        }

        private int GetAdditionalAge(Race race, CharacterClass characterClass, string classType, int maximumAge, Dictionary<string, int> ages, int startingAge)
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
                case CharacterClassConstants.TrainingTypes.SelfTaught: return totalCap * 2 / 60;
                case CharacterClassConstants.TrainingTypes.Trained: return totalCap / 20;
                default: throw new ArgumentException("Not a valid class training type");
            }
        }

        private string GetAgeStage(int ageInYears, Dictionary<string, int> ages)
        {
            if (ageInYears >= ages[RaceConstants.Ages.Venerable])
                return RaceConstants.Ages.Venerable;

            if (ageInYears >= ages[RaceConstants.Ages.Old])
                return RaceConstants.Ages.Old;

            if (ageInYears >= ages[RaceConstants.Ages.MiddleAge])
                return RaceConstants.Ages.MiddleAge;

            return RaceConstants.Ages.Adulthood;
        }

        private string GetClassType(CharacterClass characterClass)
        {
            var intuitiveClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.TrainingTypes.Intuitive);
            if (intuitiveClasses.Contains(characterClass.Name))
                return CharacterClassConstants.TrainingTypes.Intuitive;

            var trainedClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.TrainingTypes.Trained);
            if (trainedClasses.Contains(characterClass.Name))
                return CharacterClassConstants.TrainingTypes.Trained;

            return CharacterClassConstants.TrainingTypes.SelfTaught;
        }

        private int GetModifier(Race race, string tableName)
        {
            var roll = collectionsSelector.SelectFrom(tableName, race.BaseRace).Single();
            return dice.Roll(roll).AsSum();
        }
    }
}