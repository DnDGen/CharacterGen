using D20Dice.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Level
{
    public class AnyNonEpicLevel : ILevelRandomizer
    {
        private IDice dice;

        public AnyNonEpicLevel(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            return dice.d20();
        }
    }
}