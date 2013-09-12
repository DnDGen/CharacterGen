using System;
using NPCGen.Core.Characters.Data;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces
{
    public class SetRace : IBaseRaceRandomizer
    {
        public String BaseRace { get; set; }

        public SetRace(String baseRace)
        {
            BaseRace = baseRace;
        }

        public String Randomize(Alignment alignment, String characterClass)
        {
            return BaseRace;
        }
    }
}