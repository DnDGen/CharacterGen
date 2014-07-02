using System;
using System.Collections.Generic;

namespace NPCGen.Common.Magics
{
    public class Magic
    {
        public Familiar Familiar { get; set; }
        public Dictionary<Int32, IEnumerable<String>> Spells { get; set; }
    }
}