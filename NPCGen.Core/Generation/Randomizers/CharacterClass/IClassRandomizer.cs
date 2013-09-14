using NPCGen.Core.Data.Alignments;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public interface IClassRandomizer
    {
        String Randomize(Alignment alignment);
    }
}