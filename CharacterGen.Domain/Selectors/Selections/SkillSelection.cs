using System;

namespace CharacterGen.Domain.Selectors.Selections
{
    internal class SkillSelection
    {
        public String BaseStatName { get; set; }

        public SkillSelection()
        {
            BaseStatName = String.Empty;
        }
    }
}