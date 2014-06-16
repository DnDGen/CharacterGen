using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators.Interfaces
{
    public interface ICharacterClassGenerator
    {
        CharacterClassPrototype CreatePrototypeWith(Alignment alignment, ILevelRandomizer levelRandomizer,
            IClassNameRandomizer classNameRandomizer);
        CharacterClass CreateWith(CharacterClassPrototype prototype);
    }
}