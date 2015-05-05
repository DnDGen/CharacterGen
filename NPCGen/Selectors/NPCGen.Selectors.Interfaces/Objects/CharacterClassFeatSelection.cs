using System;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class CharacterClassFeatSelection
    {
        public String FeatId { get; set; }
        public String FocusType { get; set; }
        public Int32 MinimumLevel { get; set; }
        public Int32 Strength { get; set; }

        public CharacterClassFeatSelection()
        {
            FeatId = String.Empty;
            FocusType = String.Empty;
        }

        public Boolean RequirementsSatisfied(CharacterClass characterClass)
        {
            return characterClass.Level > -MinimumLevel;
        }
    }
}