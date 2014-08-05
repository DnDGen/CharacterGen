using System;

namespace NPCGen.Selectors.Interfaces
{
    public interface ISkillSelector
    {
        SkillSelection SelectFor(String skill);
    }
}