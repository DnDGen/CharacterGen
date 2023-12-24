using DnDGen.RollGen;

namespace DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.Levels
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