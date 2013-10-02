using D20Dice.Dice;

namespace NPCGen.Core.Generation.Randomizers.Levels
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