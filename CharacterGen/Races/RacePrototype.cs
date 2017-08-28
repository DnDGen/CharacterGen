using System.Linq;

namespace CharacterGen.Races
{
    public class RacePrototype
    {
        public string BaseRace { get; set; }
        public string Metarace { get; set; }

        public string Summary
        {
            get
            {
                var summary = string.Empty;

                if (Metarace.Any())
                    summary += $"{Metarace} ";

                summary += BaseRace;

                return summary;
            }
        }
    }
}