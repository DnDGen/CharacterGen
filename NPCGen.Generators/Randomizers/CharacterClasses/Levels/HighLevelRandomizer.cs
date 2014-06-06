using D20Dice;

namespace NPCGen.Generators.Randomizers.CharacterClasses.Levels
{
    public class HighLevelRandomizer : RangedLevel
    {
        public HighLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 10;
        }
    }
}