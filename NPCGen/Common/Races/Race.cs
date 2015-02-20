using System;

namespace NPCGen.Common.Races
{
    public class Race
    {
        public RaceName BaseRace { get; set; }
        public RaceName Metarace { get; set; }
        public String MetaraceSpecies { get; set; }
        public Boolean Male { get; set; }
        public Boolean HasWings { get; set; }
        public String Size { get; set; }
        public Int32 LandSpeed { get; set; }
        public Int32 AerialSpeed { get; set; }

        public Race()
        {
            BaseRace = new RaceName();
            Metarace = new RaceName();
            Size = String.Empty;
            MetaraceSpecies = String.Empty;
        }
    }
}