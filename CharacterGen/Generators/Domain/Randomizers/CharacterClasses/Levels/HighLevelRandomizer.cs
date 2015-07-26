using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
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