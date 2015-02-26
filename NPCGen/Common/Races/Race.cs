using System;

namespace NPCGen.Common.Races
{
    public class Race
    {
        public NameModel BaseRace { get; set; }
        public NameModel Metarace { get; set; }
        public String MetaraceSpecies { get; set; }
        public Boolean Male { get; set; }
        public Boolean HasWings { get; set; }
        public String Size { get; set; }
        public Int32 LandSpeed { get; set; }
        public Int32 AerialSpeed { get; set; }

        public Race()
        {
            BaseRace = new NameModel();
            Metarace = new NameModel();
            Size = String.Empty;
            MetaraceSpecies = String.Empty;
        }
    }
}