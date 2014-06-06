using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface ICharacterClassFactory
    {
        CharacterClassPrototype CreatePrototypeWith(Alignment alignment, ILevelRandomizer levelRandomizer,
            IClassNameRandomizer classNameRandomizer);
        CharacterClass CreateWith(CharacterClassPrototype prototype);
    }
}