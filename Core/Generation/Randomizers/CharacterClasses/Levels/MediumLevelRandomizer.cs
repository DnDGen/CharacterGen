using D20Dice;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels
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