using CharacterGen.Generators.Randomizers.CharacterClasses;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class SetLevelRandomizer : ISetLevelRandomizer
    {
        public Int32 SetLevel { get; set; }
        public Boolean AllowAdjustments { get; set; }

        public SetLevelRandomizer()
        {
            AllowAdjustments = true;
        }

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