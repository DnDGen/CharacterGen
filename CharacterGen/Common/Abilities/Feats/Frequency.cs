namespace CharacterGen.Common.Abilities.Feats
{
    public class Frequency
    {
        public int Quantity { get; set; }
        public string TimePeriod { get; set; }

        public Frequency()
        {
            TimePeriod = string.Empty;
        }
    }
}