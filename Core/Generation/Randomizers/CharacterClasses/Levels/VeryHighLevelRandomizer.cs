using D20Dice.Dice;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels
{
    public class VeryHighLevelRandomizer : RangedLevel
    {
        public VeryHighLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 15;
        }
    }
}