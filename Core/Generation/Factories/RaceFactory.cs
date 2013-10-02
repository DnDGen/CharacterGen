using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public class RaceFactory : IRaceFactory
    {
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }

        public RaceFactory(IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            BaseRaceRandomizer = baseRaceRandomizer;
            MetaraceRandomizer = metaraceRandomizer;
        }

        public Race Generate(String goodnessString, String className)
        {
            var race = new Race();

            race.BaseRace = BaseRaceRandomizer.Randomize(goodnessString, className);
            race.Metarace = MetaraceRandomizer.Randomize(goodnessString, className);

            return race;
        }
    }
}