using System.Collections.Generic;

namespace DnDGen.CharacterGen.Combats
{
    public class BaseAttack
    {
        public int BaseBonus { get; set; }
        public int StrengthBonus { get; set; }
        public int DexterityBonus { get; set; }
        public int SizeModifier { get; set; }
        public int RacialModifier { get; set; }
        public bool CircumstantialBonus { get; set; }

        public int RangedBonus
        {
            get
            {
                return GetTotalBonus(BaseBonus, DexterityBonus);
            }
        }

        public int MeleeBonus
        {
            get
            {
                return GetTotalBonus(BaseBonus, StrengthBonus);
            }
        }

        public IEnumerable<int> AllRangedBonuses
        {
            get
            {
                return GetBonuses(DexterityBonus);
            }
        }

        public IEnumerable<int> AllMeleeBonuses
        {
            get
            {
                return GetBonuses(StrengthBonus);
            }
        }

        private int GetTotalBonus(int baseBonus, int statBonus)
        {
            return baseBonus + statBonus + SizeModifier + RacialModifier;
        }

        private IEnumerable<int> GetBonuses(int statBonus)
        {
            var bonuses = new List<int>();
            var baseBonus = BaseBonus;

            do
            {
                var total = GetTotalBonus(baseBonus, statBonus);
                bonuses.Add(total);
                baseBonus -= 5;
            } while (baseBonus > 0 && bonuses.Count < 4);

            return bonuses;
        }
    }
}