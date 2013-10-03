﻿using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class RaceFactory
    {
        public static Race CreateUsing(String goodnessString, String className, IBaseRaceRandomizer baseRaceRandomizer, 
            IMetaraceRandomizer metaraceRandomizer)
        {
            var race = new Race();

            race.BaseRace = baseRaceRandomizer.Randomize(goodnessString, className);
            race.Metarace = metaraceRandomizer.Randomize(goodnessString, className);

            return race;
        }
    }
}