using System.Linq;

namespace CharacterGen.Races
{
    public class Race
    {
        public string BaseRace { get; set; }
        public string Metarace { get; set; }
        public string MetaraceSpecies { get; set; }
        public bool IsMale { get; set; }
        public bool HasWings { get; set; }
        public string Size { get; set; }
        public int ChallengeRating { get; set; }
        public Measurement Age { get; set; }
        public Measurement MaximumAge { get; set; }
        public Measurement Height { get; set; }
        public Measurement Weight { get; set; }
        public Measurement LandSpeed { get; set; }
        public Measurement AerialSpeed { get; set; }

        public string Gender
        {
            get
            {
                return IsMale ? "Male" : "Female";
            }
        }

        public string Summary
        {
            get
            {
                var summary = $"{Gender} ";

                if (Metarace.Any())
                    summary += $"{Metarace} ";

                summary += BaseRace;

                return summary;
            }
        }

        public Race()
        {
            BaseRace = string.Empty;
            Metarace = string.Empty;
            Size = string.Empty;
            MetaraceSpecies = string.Empty;
            Age = new Measurement("Years");
            MaximumAge = new Measurement("Years");
            Height = new Measurement("Inches");
            Weight = new Measurement("Pounds");
            LandSpeed = new Measurement("feet per round");
            AerialSpeed = new Measurement("feet per round");
        }
    }
}