using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Magics
{
    public class Spell
    {
        public Dictionary<string, int> Sources { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Metamagic { get; set; }

        public string Summary => $"{Name} ({string.Join(", ", Sources.Select(s => $"{s.Key}/{s.Value}"))})";

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

        public override string ToString()
        {
            return Summary;
        }
    }
}
