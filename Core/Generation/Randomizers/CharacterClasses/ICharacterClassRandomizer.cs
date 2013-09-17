using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses
{
    public interface ICharacterClassRandomizer
    {
        String Randomize(Alignment alignment);
    }
}