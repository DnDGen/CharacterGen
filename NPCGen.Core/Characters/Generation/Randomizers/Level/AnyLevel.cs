using D20Dice.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Level
{
    public class AnyLevel : ILevelRandomizer
    {
        private IDice dice;

        public AnyLevel(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            var tens = (dice.d4() - 1) * 10;
            return tens + dice.d10();
        }
    }
}