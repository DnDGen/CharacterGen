using System;
using System.Collections.Generic;
using CharacterGen.Generators.Randomizers.CharacterClasses;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class SetLevelRandomizer : ISetLevelRandomizer
    {
        public Int32 SetLevel { get; set; }

        public Int32 Randomize()
        {
            return SetLevel;
        }

        public IEnumerable<Int32> GetAllPossibleResults()
        {
            return new[] { SetLevel };
        }
    }
}