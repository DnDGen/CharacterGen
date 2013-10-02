using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces
{
    public interface IClassNameRandomizer
    {
        String Randomize(Alignment alignment);
    }
}