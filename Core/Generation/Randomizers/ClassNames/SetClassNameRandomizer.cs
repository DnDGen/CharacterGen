using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.ClassNames
{
    public class SetClassNameRandomizer : IClassNameRandomizer
    {
        public String ClassName { get; set; }

        public SetClassNameRandomizer(String className)
        {
            ClassName = className;
        }

        public String Randomize(Alignment alignment)
        {
            return ClassName;
        }
    }
}