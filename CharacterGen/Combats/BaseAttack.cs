using System.Collections.Generic;

namespace CharacterGen.Combats
{
    public class BaseAttack
    {
        public int BaseBonus { get; set; }
        public int StrengthBonus { get; set; }
        public int DexterityBonus { get; set; }
        public int SizeModifier { get; set; }
        public bool CircumstantialBonus { get; set; }

        public int RangedBonus
        {
            get
            {
                return BaseBonus + SizeModifier + DexterityBonus;
            }
        }

        public int MeleeBonus
        {
            get
            {
                return BaseBonus + SizeModifier + StrengthBonus;
            }
        }

        public IEnumerable<int> AllRangedBonuses
        {
            get
            {
                return GetBonuses(RangedBonus);
            }
        }

        public IEnumerable<int> AllMeleeBonuses
        {
            get
            {
                return GetBonuses(MeleeBonus);
            }
        }

        private IEnumerable<int> GetBonuses(int totalBonus)
        {
            var bonuses = new List<int>();
            var attackBonus = MeleeBonus;

            do
            {
                bonuses.Add(attackBonus);
                attackBonus -= 5;
            } while (attackBonus > 0 && bonuses.Count < 4);

            return bonuses;
        }
    }
}