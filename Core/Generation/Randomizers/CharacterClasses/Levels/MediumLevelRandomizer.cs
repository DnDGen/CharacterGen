using D20Dice.Dice;

namespace NPCGen.Core.Generation.Randomizers.Levels
{
    public class MediumLevelRandomizer : RangedLevelRandomizer
    {
        public MediumLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 5;
        }
    }
}