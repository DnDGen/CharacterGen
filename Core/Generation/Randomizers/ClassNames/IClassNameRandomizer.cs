using NPCGen.Core.Data.Alignments;
using System;

namespace NPCGen.Core.Generation.Randomizers.ClassNames
{
    public interface IClassNameRandomizer
    {
        String Randomize(Alignment alignment);
    }
}