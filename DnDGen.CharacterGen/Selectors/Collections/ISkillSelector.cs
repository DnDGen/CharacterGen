using DnDGen.CharacterGen.Selectors.Selections;

namespace DnDGen.CharacterGen.Selectors.Collections
{
    internal interface ISkillSelector
    {
        SkillSelection SelectFor(string skill);
    }
}