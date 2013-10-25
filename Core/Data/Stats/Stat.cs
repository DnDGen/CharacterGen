using System;

namespace NPCGen.Core.Data.Stats
{
    public class Stat
    {
        public Int32 Value { get; set; }
        public Int32 Bonus { get { return (Value - 10) / 2; } }
    }
}