using System;

namespace CharacterGen.Common.Abilities.Stats
{
    public class Stat
    {
        public Int32 Value { get; set; }
        public Int32 Bonus
        {
            get
            {
                var even = Value - Value % 2;
                return (even - 10) / 2;
            }
        }
    }
}