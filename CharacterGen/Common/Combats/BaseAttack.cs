using System;
using System.Collections.Generic;

namespace NPCGen.Common.Combats
{
    public class BaseAttack
    {
        public Int32 Bonus { get; set; }

        public List<Int32> GetAllBonuses()
        {
            var bonuses = new List<Int32>();
            var attackBonus = Bonus;

            do
            {
                bonuses.Add(attackBonus);
                attackBonus -= 5;
            } while (attackBonus > 0);

            return bonuses;
        }
    }
}