using DnDGen.RollGen;

namespace DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.Levels
{
    internal class VeryHighLevelRandomizer : RangedLevelRandomizer
    {
        public VeryHighLevelRandomizer(Dice dice)
            : base(dice)
        {
            rollBonus = 15;
        }
    }
}