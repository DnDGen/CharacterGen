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

        public Race GenerateWith(String goodnessString, CharacterClassPrototype prototype, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(goodnessString, prototype);
            race.Metarace = metaraceRandomizer.Randomize(goodnessString, prototype);
            race.Male = DetermineIfMale(race.BaseRace, prototype.ClassName);

            return race;
        }

        private Boolean DetermineIfMale(String baseRace, String className)
        {
            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Wizard)
                return true;

            if (baseRace == RaceConstants.BaseRaces.Drow && className == CharacterClassConstants.Cleric)
                return false;

            return dice.d2() == 1;
        }
    }
}