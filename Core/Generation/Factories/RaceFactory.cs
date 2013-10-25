using System;
using D20Dice.Dice;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class RaceFactory
    {
        public static Race CreateUsing(String goodnessString, CharacterClass characterClass, IBaseRaceRandomizer baseRaceRandomizer, 
            IMetaraceRandomizer metaraceRandomizer, IDice dice)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(goodnessString, characterClass);
            race.Metarace = metaraceRandomizer.Randomize(goodnessString, characterClass);

            race.Male = GenerateGender(dice, race.BaseRace, characterClass.ClassName);

            return race;
        }

        private static Boolean GenerateGender(IDice dice, String baseRace, String className)
        {
            if (baseRace == RaceConstants.BaseRaces.Drow)
            {
                if (className == CharacterClassConstants.Wizard)
                    return true;
                else if (className == CharacterClassConstants.Cleric)
                    return false;
            }

            return dice.d2() == 1;
        }
    }
}