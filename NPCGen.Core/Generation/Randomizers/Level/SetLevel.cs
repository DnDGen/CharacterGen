using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Generation.Randomizers.Level
{
    public class SetLevel : ILevelRandomizer
    {
        public Int32 Level { get; set; }

        public SetLevel(Int32 level)
        {
            Level = level;
        }

        public Int32 Randomize()
        {
            return Level;
        }
    }
}