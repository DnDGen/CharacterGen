using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class LowLevelRandomizer : RangedLevelRandomizer
    {
        public LowLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 0;
        }
    }
}