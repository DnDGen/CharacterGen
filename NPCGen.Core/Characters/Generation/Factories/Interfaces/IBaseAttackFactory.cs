using NPCGen.Core.Characters.Data.Classes;

namespace NPCGen.Core.Characters.Generation.Factories.Interfaces
{
    public interface IBaseAttackFactory
    {
        BaseAttack Generate(CharacterClass characterClass);
    }
}