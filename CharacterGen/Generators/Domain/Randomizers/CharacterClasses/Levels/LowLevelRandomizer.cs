using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class LowLevelRandomizer : RangedLevelRandomizer
    {
        public LowLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 0;
        }
    }
}