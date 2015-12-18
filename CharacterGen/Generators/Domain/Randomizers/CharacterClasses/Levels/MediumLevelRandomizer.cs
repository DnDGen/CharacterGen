using RollGen;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class MediumLevelRandomizer : RangedLevelRandomizer
    {
        public MediumLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 5;
        }
    }
}