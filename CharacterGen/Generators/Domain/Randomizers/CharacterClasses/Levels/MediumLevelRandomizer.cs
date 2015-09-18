using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
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