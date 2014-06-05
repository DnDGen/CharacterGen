using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels
{
    public class SetLevelRandomizer : ILevelRandomizer
    {
        public Int32 Level { get; set; }

        public Int32 Randomize()
        {
            return Level;
        }

        public IEnumerable<Int32> GetAllPossibleResults()
        {
            return new[] { Level };
        }
    }
}