using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Magics;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Magics
{
    internal interface ISpellsGenerator
    {
        IEnumerable<SpellQuantity> GeneratePerDay(CharacterClass characterClass, Dictionary<string, Stat> stats);
        IEnumerable<Spell> GenerateKnown(CharacterClass characterClass, Dictionary<string, Stat> stats);
        IEnumerable<Spell> GeneratePrepared(CharacterClass characterClass, IEnumerable<Spell> knownSpells, IEnumerable<SpellQuantity> spellsPerDay);
    }
}
