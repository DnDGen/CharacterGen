using System;
using CharacterGen.Selectors.Objects;

namespace CharacterGen.Selectors
{
    public interface ISkillSelector
    {
        SkillSelection SelectFor(String skill);
    }
}