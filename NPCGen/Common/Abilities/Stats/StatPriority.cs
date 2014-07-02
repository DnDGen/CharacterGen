using System;

namespace NPCGen.Common.Abilities.Stats
{
    public class StatPriority
    {
        public String FirstPriority { get; set; }
        public String SecondPriority { get; set; }

        public StatPriority()
        {
            FirstPriority = String.Empty;
            SecondPriority = String.Empty;
        }
    }
}