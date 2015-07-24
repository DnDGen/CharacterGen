using System;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class SkillSelection
    {
        public String BaseStatName { get; set; }
        public Boolean ArmorCheckPenalty { get; set; }

        public SkillSelection()
        {
            BaseStatName = String.Empty;
        }
    }
}