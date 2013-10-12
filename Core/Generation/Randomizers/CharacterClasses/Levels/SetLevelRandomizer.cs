using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels
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