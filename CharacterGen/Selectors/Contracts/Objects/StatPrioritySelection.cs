using System;

namespace CharacterGen.Selectors.Objects
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