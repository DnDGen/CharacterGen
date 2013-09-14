using NPCGen.Core.Data.Classes;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IBaseAttackFactory
    {
        BaseAttack Generate(CharacterClass characterClass);
    }
}