using System;

namespace NPCGen.Core.Data.Classes
{
    public class BaseAttack
    {
        public Int32 BaseAttackBonus { get; set; }

        public override String ToString()
        {
            var attackBonus = BaseAttackBonus;
            var output = String.Format("+{0}", attackBonus);

            attackBonus -= 5;
            while (attackBonus > 0)
            {
                output += String.Format("/+{0}", attackBonus);
                attackBonus -= 5;
            }

            return output;
        }
    }
}