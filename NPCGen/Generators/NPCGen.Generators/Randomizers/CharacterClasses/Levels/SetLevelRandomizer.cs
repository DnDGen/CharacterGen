using System;
using System.Collections.Generic;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators.Randomizers.CharacterClasses.Levels
{
    public class SetLevelRandomizer : ISetLevelRandomizer
    {
        public Int32 SetLevel { get; set; }

        public Int32 Randomize()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Int32> GetAllPossibleResults()
        {
            throw new NotImplementedException();
        }
    }
}