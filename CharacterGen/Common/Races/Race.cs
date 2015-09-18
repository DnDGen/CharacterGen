using System;

namespace CharacterGen.Common.Races
{
    public class Race
    {
        public String BaseRace { get; set; }
        public String Metarace { get; set; }
        public String MetaraceSpecies { get; set; }
        public Boolean Male { get; set; }
        public Boolean HasWings { get; set; }
        public String Size { get; set; }
        public Int32 LandSpeed { get; set; }
        public Int32 AerialSpeed { get; set; }
        public Int32 HeightInInches { get; set; }
        public Int32 WeightInPounds { get; set; }
        public Age Age { get; set; }

        public Race()
        {
            BaseRace = String.Empty;
            Metarace = String.Empty;
            Size = String.Empty;
            MetaraceSpecies = String.Empty;
            Age = new Age();
        }
    }
}