using D20Dice;

namespace NPCGen.Generators.Randomizers.CharacterClasses.Levels
{
    public class LowLevelRandomizer : RangedLevel
    {
        public LowLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 0;
        }
    }
}