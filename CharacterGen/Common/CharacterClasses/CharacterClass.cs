using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Common.CharacterClasses
{
    public class CharacterClass
    {
        public Int32 Level { get; set; }
        public String ClassName { get; set; }
        public IEnumerable<String> SpecialistFields { get; set; }
        public IEnumerable<String> ProhibitedFields { get; set; }

        public CharacterClass()
        {
            ClassName = String.Empty;
            SpecialistFields = Enumerable.Empty<String>();
            ProhibitedFields = Enumerable.Empty<String>();
        }
    }
}