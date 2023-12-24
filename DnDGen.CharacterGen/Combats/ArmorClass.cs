using System;

namespace DnDGen.CharacterGen.Combats
{
    public class ArmorClass
    {
        public const int BaseArmorClass = 10;

        public bool CircumstantialBonus { get; set; }
        public int ArmorBonus { get; set; }
        public int ShieldBonus { get; set; }
        public int SizeModifier { get; set; }
        public int DeflectionBonus { get; set; }
        public int NaturalArmorBonus { get; set; }
        public int AdjustedDexterityBonus { get; set; }
        public int DodgeBonus { get; set; }

        public int Full
        {
            get
            {
                var rawFullArmorClass = BaseArmorClass + ArmorBonus + ShieldBonus + SizeModifier + DeflectionBonus + NaturalArmorBonus + AdjustedDexterityBonus + DodgeBonus;
                return Math.Max(rawFullArmorClass, 1);
            }
        }

        public int Touch
        {
            get
            {
                var rawTouchArmorClass = BaseArmorClass + SizeModifier + DeflectionBonus + AdjustedDexterityBonus + DodgeBonus;
                return Math.Max(rawTouchArmorClass, 1);
            }
        }

        public int FlatFooted
        {
            get
            {
                var rawFlatFootedArmorClass = BaseArmorClass + ArmorBonus + ShieldBonus + SizeModifier + DeflectionBonus + NaturalArmorBonus;
                return Math.Max(rawFlatFootedArmorClass, 1);
            }
        }
    }
}