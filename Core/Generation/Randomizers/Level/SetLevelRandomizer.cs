using System;

namespace NPCGen.Core.Generation.Randomizers.Level
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