using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Magics
{
    public class Magic
    {
        public string Animal { get; set; }
        public IEnumerable<SpellQuantity> SpellsPerDay { get; set; }
        public IEnumerable<Spell> KnownSpells { get; set; }
        public IEnumerable<Spell> PreparedSpells { get; set; }
        public int ArcaneSpellFailure { get; set; }

        public Magic()
        {
            SpellsPerDay = Enumerable.Empty<SpellQuantity>();
            Animal = string.Empty;
            KnownSpells = Enumerable.Empty<Spell>();
            PreparedSpells = Enumerable.Empty<Spell>();
        }
    }
}