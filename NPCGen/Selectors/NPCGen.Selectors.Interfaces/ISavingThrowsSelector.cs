using NPCGen.Common;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Selectors.Interfaces
{
    public interface ISavingThrowsSelector
    {
        SavingThrows SelectFor(CharacterClass characterClass);
    }
}