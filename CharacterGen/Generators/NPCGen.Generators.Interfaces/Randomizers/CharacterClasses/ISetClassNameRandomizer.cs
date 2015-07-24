using System;

namespace NPCGen.Generators.Interfaces.Randomizers.CharacterClasses
{
    public interface ISetClassNameRandomizer : IClassNameRandomizer
    {
        String SetClassName { get; set; }
    }
}