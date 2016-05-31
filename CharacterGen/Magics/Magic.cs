using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Magics
{
    public class Magic
    {
        public string Animal { get; set; }
        public IEnumerable<Spells> SpellsPerDay { get; set; }
        public int ArcaneSpellFailure { get; set; }

        public Magic()
        {
            SpellsPerDay = Enumerable.Empty<Spells>();
            Animal = string.Empty;
        }
    }
}