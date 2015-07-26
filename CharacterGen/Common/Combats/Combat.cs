using System;

namespace CharacterGen.Common.Combats
{
    public class Combat
    {
        public ArmorClass ArmorClass { get; set; }
        public SavingThrows SavingThrows { get; set; }
        public Int32 HitPoints { get; set; }
        public BaseAttack BaseAttack { get; set; }
        public Int32 AdjustedDexterityBonus { get; set; }
        public Int32 InitiativeBonus { get; set; }

        public Combat()
        {
            ArmorClass = new ArmorClass();
            SavingThrows = new SavingThrows();
            BaseAttack = new BaseAttack();
        }
    }
}