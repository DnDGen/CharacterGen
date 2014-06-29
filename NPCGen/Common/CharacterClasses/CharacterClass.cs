using System;

namespace NPCGen.Common.CharacterClasses
{
    public class CharacterClass
    {
        public Int32 Level { get; set; }
        public String ClassName { get; set; }

        public CharacterClass()
        {
            ClassName = String.Empty;
        }
    }
}