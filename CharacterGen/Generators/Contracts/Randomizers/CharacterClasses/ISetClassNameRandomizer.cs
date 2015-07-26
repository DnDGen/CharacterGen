using System;

namespace CharacterGen.Generators.Randomizers.CharacterClasses
{
    public interface ISetClassNameRandomizer : IClassNameRandomizer
    {
        String SetClassName { get; set; }
    }
}