namespace CharacterGen.Common.Races
{
    public class Race
    {
        public string BaseRace { get; set; }
        public string Metarace { get; set; }
        public string MetaraceSpecies { get; set; }
        public bool Male { get; set; }
        public bool HasWings { get; set; }
        public string Size { get; set; }
        public int LandSpeed { get; set; }
        public int AerialSpeed { get; set; }
        //public int HeightInInches { get; set; }
        //public int WeightInPounds { get; set; }
        //public Age Age { get; set; }

        public Race()
        {
            BaseRace = string.Empty;
            Metarace = string.Empty;
            Size = string.Empty;
            MetaraceSpecies = string.Empty;
            //Age = new Age();
        }
    }
}