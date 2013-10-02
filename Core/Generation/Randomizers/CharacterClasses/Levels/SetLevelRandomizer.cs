using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Levels
{
    public class SetLevelRandomizer : ILevelRandomizer
    {
        public Int32 Level { get; set; }

        public Int32 Randomize()
        {
            return Level;
        }
    }
}