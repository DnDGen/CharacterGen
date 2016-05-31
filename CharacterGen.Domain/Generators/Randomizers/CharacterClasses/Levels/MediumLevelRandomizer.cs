using RollGen;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.Levels
{
    internal class MediumLevelRandomizer : RangedLevelRandomizer
    {
        public MediumLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 5;
        }
    }
}