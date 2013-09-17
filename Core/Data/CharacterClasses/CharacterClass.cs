using System;

namespace NPCGen.Core.Data.CharacterClasses
{
    public class CharacterClass
    {
        public Int32 HitPoints { get; set; }
        public Int32 Level { get; set; }
        public BaseAttack BaseAttack { get; set; }
        public String ClassName { get; set; }
    }
}