using CharacterGen.Domain.Selectors.Selections;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface ISkillSelector
    {
        SkillSelection SelectFor(string skill);
    }
}