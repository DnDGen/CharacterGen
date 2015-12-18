using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class HighLevelRandomizer : RangedLevelRandomizer
    {
        public HighLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 10;
        }
    }
}