using NPCGen.Core.Characters.Data;
using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public interface IClassRandomizer
    {
        String Randomize(Alignment alignment);
    }
}