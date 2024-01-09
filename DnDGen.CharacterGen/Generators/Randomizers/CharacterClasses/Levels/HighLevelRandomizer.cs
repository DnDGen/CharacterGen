using DnDGen.RollGen;

namespace DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.Levels
{
    internal class HighLevelRandomizer : RangedLevelRandomizer
    {
        public HighLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 10;
        }
    }
}