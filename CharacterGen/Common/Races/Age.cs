using System;

namespace CharacterGen.Common.Races
{
    public class Age
    {
        public Int32 Years { get; set; }
        public String Stage { get; set; }

        public Age()
        {
            Stage = String.Empty;
        }
    }
}
