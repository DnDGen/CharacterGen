using D20Dice;

namespace NPCGen.Generators.Randomizers.CharacterClasses.Levels
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