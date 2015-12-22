using System.Collections.Generic;

namespace CharacterGen.Common.Combats
{
    public class BaseAttack
    {
        public int Bonus { get; set; }
        public bool CircumstantialBonus { get; set; }

        public IEnumerable<int> AllBonuses
        {
            get
            {
                var bonuses = new List<int>();
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