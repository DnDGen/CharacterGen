using System;
using System.Linq;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators
{
    public class RaceGenerator : IRaceGenerator
    {
        private IDice dice;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public RaceGenerator(IDice dice, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.dice = dice;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Race GenerateWith(Alignment alignment, CharacterClass characterClass, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(alignment.Goodness, characterClass);
            race.Metarace = metaraceRandomizer.Randomize(alignment.Goodness, characterClass);
            race.MetaraceSpecies = DetermineMetaraceSpecies(alignment, race.Metarace.Id);
            race.Male = DetermineIfMale(race.BaseRace.Id, characterClass.ClassName);
            race.Size = DetermineSize(race.BaseRace.Id);
            race.HasWings = DetermineIfRaceHasWings(race);
            race.LandSpeed = DetermineLandSpeed(race);
            race.AerialSpeed = DetermineAerialSpeed(race);

            return race;
        }

        private String DetermineMetaraceSpecies(Alignment alignment, String metaraceId)
        {
            if (metaraceId != RaceConstants.Metaraces.HalfDragonId)
                return String.Empty;

            var species = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.DragonSpecies, alignment.ToString());
            var index = dice.Roll().d(species.Count()) - 1;

            return species.ElementAt(index);
        }

        private Boolean DetermineIfMale(String baseRace, String className)
        {
            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Wizard)
                return true;

            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Cleric)
                return false;

            return dice.Roll().d2() == 1;
        }

        private String DetermineSize(String baseRaceId)
        {
            var largeRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Large);
            if (largeRaces.Contains(baseRaceId))
                return RaceConstants.Sizes.Large;

            var smallRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, RaceConstants.Sizes.Small);
            if (smallRaces.Contains(baseRaceId))
                return RaceConstants.Sizes.Small;

            return RaceConstants.Sizes.Medium;
        }

        private Boolean DetermineIfRaceHasWings(Race race)
        {
            if (race.Metarace.Id == RaceConstants.Metaraces.NoneId)
                return false;

            if (race.Metarace.Id == RaceConstants.Metaraces.HalfCelestialId || race.Metarace.Id == RaceConstants.Metaraces.HalfFiendId)
                return true;

            if (race.Metarace.Id == RaceConstants.Metaraces.HalfDragonId)
                return race.Size == RaceConstants.Sizes.Large;

            return false;
        }

        private Int32 DetermineLandSpeed(Race race)
        {
            var speeds = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds);
            return speeds[race.BaseRace.Id];
        }

        private Int32 DetermineAerialSpeed(Race race)
        {
            if (!race.HasWings)
                return 0;

            if (race.Metarace.Id == RaceConstants.Metaraces.HalfFiendId)
                return race.LandSpeed;

            return race.LandSpeed * 2;
        }
    }
}