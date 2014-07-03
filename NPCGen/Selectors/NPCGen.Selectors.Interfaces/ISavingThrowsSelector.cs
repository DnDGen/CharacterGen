using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;

namespace NPCGen.Selectors.Interfaces
{
    public interface ISavingThrowsSelector
    {
        SavingThrows SelectFor(CharacterClass characterClass);
    }
}