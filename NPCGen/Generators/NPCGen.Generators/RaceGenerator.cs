using System;
using D20Dice;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Races;

namespace NPCGen.Generators
{
    public class RaceGenerator : IRaceGenerator
    {
        private IDice dice;

        public RaceGenerator(IDice dice)
        {
            this.dice = dice;
        }

        public Race GenerateWith(String goodnessString, CharacterClass characterClass, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(goodnessString, characterClass);
            race.Metarace = metaraceRandomizer.Randomize(goodnessString, characterClass);
            race.Male = DetermineIfMale(race.BaseRace, characterClass.ClassName);

            return race;
        }

        private Boolean DetermineIfMale(String baseRace, String className)
        {
            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Wizard)
                return true;

            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Cleric)
                return false;

            return dice.Roll().d2() == 1;
        }
    }
}