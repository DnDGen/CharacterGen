using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators.Interfaces
{
    public interface ICharacterClassFactory
    {
        CharacterClassPrototype CreatePrototypeWith(Alignment alignment, ILevelRandomizer levelRandomizer,
            IClassNameRandomizer classNameRandomizer);
        CharacterClass CreateWith(CharacterClassPrototype prototype);
    }
}