using System;

namespace CharacterGen.Common.Abilities.Feats
{
    public class Frequency
    {
        public Int32 Quantity { get; set; }
        public String TimePeriod { get; set; }

        public Frequency()
        {
            TimePeriod = String.Empty;
        }
    }
}