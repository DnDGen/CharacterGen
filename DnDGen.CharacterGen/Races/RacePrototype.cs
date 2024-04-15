using System.Linq;

namespace DnDGen.CharacterGen.Races
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

        public RacePrototype()
        {
            BaseRace = string.Empty;
            Metarace = string.Empty;
        }

        public override string ToString()
        {
            return Summary;
        }

        public override bool Equals(object toCompare)
        {
            if (!(toCompare is RacePrototype))
                return false;

            var alignment = toCompare as RacePrototype;
            return Summary == alignment.Summary;
        }

        public override int GetHashCode()
        {
            return Summary.GetHashCode();
        }
    }
}