using D20Dice;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using System;

namespace NPCGen.Core.Generation.Factories
{
    public class RaceFactory : IRaceFactory
    {
        private IDice dice;

        public RaceFactory(IDice dice)
        {
            this.dice = dice;
        }

        public Race CreateWith(String goodnessString, CharacterClassPrototype prototype, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(goodnessString, prototype);
            race.Metarace = metaraceRandomizer.Randomize(goodnessString, prototype);
            race.Male = DetermineIfMale(race.BaseRace, prototype.ClassName);

            return race;
        }

        private Boolean DetermineIfMale(String baseRace, String className)
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