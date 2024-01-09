namespace DnDGen.CharacterGen.Combats
{
    public class Combat
    {
        public ArmorClass ArmorClass { get; set; }
        public SavingThrows SavingThrows { get; set; }
        public int HitPoints { get; set; }
        public BaseAttack BaseAttack { get; set; }
        public int AdjustedDexterityBonus { get; set; }
        public int InitiativeBonus { get; set; }

        public Combat()
        {
            ArmorClass = new ArmorClass();
            SavingThrows = new SavingThrows();
            BaseAttack = new BaseAttack();
        }
    }
}