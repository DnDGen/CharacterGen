using D20Dice.Dice;

namespace NPCGen.Core.Generation.Randomizers.Level
{
    public class HighLevelRandomizer : RangedLevelRandomizer
    {
        public HighLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 10;
        }
    }
}