using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;

namespace CharacterGen.Domain.Generators.Races
{
    internal interface IRaceGenerator
    {
        RacePrototype GeneratePrototype(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        Race GenerateWith(Alignment alignment, CharacterClass characterClass, RacePrototype racePrototype);
    }
}