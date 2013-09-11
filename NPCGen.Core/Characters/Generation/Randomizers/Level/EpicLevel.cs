using D20Dice.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Level
{
    public class EpicLevel : ILevelRandomizer
    {
        private IDice dice;

        public EpicLevel(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            return dice.d20(bonus: 20);
        }
    }
}