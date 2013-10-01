using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

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

        public Race Generate(Alignment alignment, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}