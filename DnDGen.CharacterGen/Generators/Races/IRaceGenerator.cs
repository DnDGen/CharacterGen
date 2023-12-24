using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Races;

namespace DnDGen.CharacterGen.Generators.Races
{
    internal interface IRaceGenerator
    {
        RacePrototype GeneratePrototype(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        Race GenerateWith(Alignment alignment, CharacterClass characterClass, RacePrototype racePrototype);
    }
}