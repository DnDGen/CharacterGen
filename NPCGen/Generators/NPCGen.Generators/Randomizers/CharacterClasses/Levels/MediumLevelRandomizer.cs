using D20Dice;

namespace NPCGen.Generators.Randomizers.CharacterClasses.Levels
{
    public class MediumLevelRandomizer : RangedLevel
    {
        public MediumLevelRandomizer(IDice dice)
            : base(dice)
        {
            rollBonus = 5;
        }
    }
}