using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.CharacterClasses
{
    public class CharacterClass
    {
        public int Level { get; set; }
        public string ClassName { get; set; }
        public IEnumerable<string> SpecialistFields { get; set; }
        public IEnumerable<string> ProhibitedFields { get; set; }

        public CharacterClass()
        {
            ClassName = string.Empty;
            SpecialistFields = Enumerable.Empty<string>();
            ProhibitedFields = Enumerable.Empty<string>();
        }
    }
}