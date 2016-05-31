using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;

namespace CharacterGen
{
    public interface ICharacterGenerator
    {
        Character GenerateWith(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer);
    }
}