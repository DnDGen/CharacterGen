using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;

namespace CharacterGen.Domain.Generators
{
    internal interface IRaceGenerator
    {
        Race GenerateWith(Alignment alignment, CharacterClass characterClass, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
    }
}