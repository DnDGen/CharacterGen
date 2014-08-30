using System;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class StatPrioritySelection
    {
        public String First { get; set; }
        public String Second { get; set; }

        public StatPrioritySelection()
        {
            First = String.Empty;
            Second = String.Empty;
        }
    }
}