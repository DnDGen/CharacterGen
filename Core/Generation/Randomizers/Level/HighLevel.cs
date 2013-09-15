using D20Dice.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Generation.Randomizers.Level
{
    public class HighLevel : ILevelRandomizer
    {
        private IDice dice;

        public HighLevel(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            var roll = dice.d6();

            while (roll == 6)
                roll = dice.d6();

            return roll + 10;
        }
    }
}