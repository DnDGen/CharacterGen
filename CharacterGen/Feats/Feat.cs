using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Feats
{
    public class Feat
    {
        public string Name { get; set; }
        public IEnumerable<string> Foci { get; set; }
        public int Power { get; set; }
        public Frequency Frequency { get; set; }

        public Feat()
        {
            Name = string.Empty;
            Foci = Enumerable.Empty<string>();
            Frequency = new Frequency();
        }
    }
}