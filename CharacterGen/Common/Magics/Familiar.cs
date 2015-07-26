using System;

namespace CharacterGen.Common.Magics
{
    public class Familiar
    {
        public String Animal { get; set; }
        public Int32 HitPoints { get; set; }
        public Int32 ArmorClass { get; set; }

        public Familiar()
        {
            Animal = String.Empty;
        }
    }
}