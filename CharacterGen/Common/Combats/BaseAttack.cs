using System;
using System.Collections.Generic;

namespace CharacterGen.Common.Combats
{
    public class BaseAttack
    {
        public Int32 Bonus { get; set; }

        public IEnumerable<Int32> AllBonuses
        {
            get
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
}