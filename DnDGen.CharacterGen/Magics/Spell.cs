using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Magics
{
    public class Spell
    {
        public IEnumerable<string> Sources { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Metamagic { get; set; }

        public string Summary => $"{Name} ({string.Join(", ", Sources.Select(s => $"{s}/{Level}"))})";

        public Spell()
        {
            Name = string.Empty;
            Sources = [];
            Metamagic = [];
        }

        public override bool Equals(object obj)
        {
            if (obj is not Spell)
                return false;

            var otherSpell = obj as Spell;

            return Summary == otherSpell.Summary;
        }

        public override int GetHashCode()
        {
            return Summary.GetHashCode();
        }
    }
}
