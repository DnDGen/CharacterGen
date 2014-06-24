using System;

namespace NPCGen.Common.Races
{
    public class Race
    {
        public String BaseRace { get; set; }
        public String Metarace { get; set; }
        public Boolean Male { get; set; }

        public Race()
        {
            BaseRace = String.Empty;
            Metarace = String.Empty;
        }
    }
}