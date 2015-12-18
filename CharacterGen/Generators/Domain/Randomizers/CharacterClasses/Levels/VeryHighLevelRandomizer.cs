using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class VeryHighLevelRandomizer : RangedLevelRandomizer
    {
        public VeryHighLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 15;
        }
    }
}