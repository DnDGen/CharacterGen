using System;

namespace NPCGen.Common.CharacterClasses
{
    public class BaseAttack
    {
        public Int32 Bonus { get; set; }

        public override String ToString()
        {
            var attackBonus = Bonus;
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