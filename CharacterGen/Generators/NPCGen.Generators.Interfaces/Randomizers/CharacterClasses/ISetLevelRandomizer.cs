using System;

namespace NPCGen.Generators.Interfaces.Randomizers.CharacterClasses
{
    public interface ISetLevelRandomizer : ILevelRandomizer
    {
        Int32 SetLevel { get; set; }
    }
}