using System;

namespace CharacterGen.Generators.Randomizers.CharacterClasses
{
    public interface ISetLevelRandomizer : ILevelRandomizer
    {
        Int32 SetLevel { get; set; }
    }
}