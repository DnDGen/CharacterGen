using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen
{
    class Character
    {
        public enum ALIGNMENT { LAWFUL, NEUTRAL, CHAOTIC, GOOD, EVIL };
        public enum ALIGNMENT_RANDOMIZER { ANY_GOOD, ANY_EVIL, ANY_LAWFUL, ANY_CHAOTIC, ANY, ANY_NEUTRAL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL, ANY_NONLAWFUL, ANY_NONCHAOTIC };
        public enum STATS { STRENGTH, CONSTITUTION, DEXTERITY, INTELLIGENCE, WISDOM, CHARISMA };
        public enum ITEMTYPE { WEAPON, ARMOR, BRACERS, RING_OF_PROTECTION, SHIELD, SCROLL, POTION, MISCELLANEOUS, SPELLS, FAMILIARS, MONEY };

        public Classes.CLASS Class;
        public int Level;
        public int[] Stats;
        public int StrengthPercentile;
        public ALIGNMENT[] Alignment;
        public int[] THACO;
        public string[] Equipment;
        public Races Race;
        public int HP;
        public string Languages;
        public int[] RogueAbilities;

        public static string[] AlignmentRandomizerArray
        {
            get
            {
                return Enum.GetNames(typeof(ALIGNMENT_RANDOMIZER));
            }
        }

        public string StatAbilities
        {
            get
            {
                string output = "";

                if (StrengthAbilities != "")
                    output += "\nSTR: " + StrengthAbilities;
                if (DexterityAbilities != "")
                    output += "\nDEX: " + DexterityAbilities;
                if (ConstitutionAbilities != "")
                    output += "\nCON: " + ConstitutionAbilities;
                if (IntelligenceAbilities != "")
                    output += "\nINT: " + IntelligenceAbilities;
                if (WisdomAbilities != "")
                    output += "\nWIS: " + WisdomAbilities;
                if (CharismaAbilities != "")
                    output += "\nCHA: " + CharismaAbilities;

                return output;
            }
        }

        public string StrengthAbilities
        {
            get
            {
                switch (Stats[(int)STATS.STRENGTH])
                {
                    case 1: return "-4 damage";
                    case 2: return "-2 damage";
                    case 3:
                    case 4:
                    case 5: return "-1 damage";
                    case 16:
                    case 17: return "+1 damage";
                    case 18:
                        if (StrengthPercentile == 0)
                            return "+2 damage";
                        else if (StrengthPercentile <= 50)
                            return "+3 damage";
                        else if (StrengthPercentile <= 75)
                            return "+4 damage";
                        else if (StrengthPercentile <= 90)
                            return "+5 damage";
                        else
                            return "+6 damage";
                    case 19: return "+7 damage";
                    case 20: return "+8 damage";
                    case 21: return "+9 damage";
                    case 22: return "+10 damage";
                    case 23: return "+11 damage";
                    case 24: return "+12 damage";
                    case 25: return "+14 damage";
                    default: return "";
                }
            }
        }

        public string DexterityAbilities
        {
            get
            {
                switch (Stats[(int)STATS.DEXTERITY])
                {
                    case 1: return "-6 Surprise, AC 5 worse";
                    case 2: return "-4 Surprise, AC 5 worse";
                    case 3: return "-3 Surprise, AC 4 worse";
                    case 4: return "-2 Surprise, AC 3 worse";
                    case 5: return "-1 Surprise, AC 2 worse";
                    case 6: return "AC 1 worse";
                    case 15: return "AC 1 better";
                    case 16: return "+1 Surprise, AC 2 better";
                    case 17: return "+2 Surprise, AC 3 better";
                    case 18: return "+2 Surprise, AC 4 better";
                    case 19:
                    case 20: return "+3 Surprise, AC 4 better";
                    case 21:
                    case 22:
                    case 23: return "+4 Surprise, AC 5 better";
                    case 24:
                    case 25: return "+5 Surprise, AC 6 better";
                    default: return "";
                }
            }
        }

        public string ConstitutionAbilities
        {
            get
            {
                switch (Stats[(int)STATS.CONSTITUTION])
                {
                    case 1: return "-2 Poison save";
                    case 2: return "-1 Poison save";
                    case 19: return "+1 Poison save";
                    case 20: return "+1 Poison save, Regen 1/6 turns";
                    case 21: return "+2 Poison save, Regen 1/5 turns";
                    case 22: return "+2 Poison save, Regen 1/4 turns";
                    case 23: return "+3 Poison save, Regen 1/3 turns";
                    case 24: return "+3 Poison save, Regen 1/2 turns";
                    case 25: return "+4 Poison save, Regen 1/1 turns";
                    default: return "";
                }
            }
        }

        public string IntelligenceAbilities
        {
            get
            {
                switch (Stats[(int)STATS.INTELLIGENCE])
                {
                    case 19: return "Immune to 1st-level illusions";
                    case 20: return "Immune to up to 2nd-level illusions";
                    case 21: return "Immune to up to 3rd-level illusions";
                    case 22: return "Immune to up to 4th-level illusions";
                    case 23: return "Immune to up to 5th-level illusions";
                    case 24: return "Immune to up to 6th-level illusions";
                    case 25: return "Immune to up to 7th-level illusions";
                    default: return "";
                }
            }
        }

        public string WisdomAbilities
        {
            get
            {
                switch (Stats[(int)STATS.WISDOM])
                {
                    case 1:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "80% spell failure";
                        return "";
                    case 2:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "60% spell failure";
                        return "";
                    case 3:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "50% spell failure";
                        return "";
                    case 4:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "45% spell failure";
                        return "";
                    case 5:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "40% spell failure";
                        return "";
                    case 6:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "35% spell failure";
                        return "";
                    case 7:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "30% spell failure";
                        return "";
                    case 8:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "25% spell failure";
                        return "";
                    case 9:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "20% spell failure";
                        return "";
                    case 10:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "15% spell failure";
                        return "";
                    case 11:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "10% spell failure";
                        return "";
                    case 12:
                        if (Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER)
                            return "5% spell failure";
                        return "";
                    case 15: return "+1 on saves v. mind spells";
                    case 16: return "+2 on saves v. mind spells";
                    case 17: return "+3 on saves v. mind spells";
                    case 18: return "+4 on saves v. mind spells";
                    case 19: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism";
                    case 20: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism, Forget, Hold Person, Ray of Enfeeblement, Scare";
                    case 21: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism, Forget, Hold Person, Ray of Enfeeblement, Scare, Fear";
                    case 22: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism, Forget, Hold Person, Ray of Enfeeblement, Scare, Fear, Charm Monster, Confusion, Emotion, Fumble, Suggestion";
                    case 23: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism, Forget, Hold Person, Ray of Enfeeblement, Scare, Fear, Charm Monster, Confusion, Emotion, Fumble, Suggestion, Chaos, Feeblemind, Hold Monster, Magic Jar, Quest";
                    case 24: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism, Forget, Hold Person, Ray of Enfeeblement, Scare, Fear, Charm Monster, Confusion, Emotion, Fumble, Suggestion, Chaos, Feeblemind, Hold Monster, Magic Jar, Quest, Geas, Mass Suggestion, Rod of Rulership";
                    case 25: return "+4 on saves v. mind spells; immune to Cause Fear, Charm Person, Command, Friends, Hypnotism, Forget, Hold Person, Ray of Enfeeblement, Scare, Fear, Charm Monster, Confusion, Emotion, Fumble, Suggestion, Chaos, Feeblemind, Hold Monster, Magic Jar, Quest, Geas, Mass Suggestion, Rod of Rulership, Antipathy/Sympathy, Death Spell, Mass Charm";
                    default: return "";
                }
            }
        }

        public string CharismaAbilities
        {
            get
            {
                switch (Stats[(int)STATS.CHARISMA])
                {
                    case 1: return "-7 Reaction adjustment";
                    case 2: return "-6 Reaction adjustment";
                    case 3: return "-5 Reaction adjustment";
                    case 4: return "-4 Reaction adjustment";
                    case 5: return "-3 Reaction adjustment";
                    case 6: return "-2 Reaction adjustment";
                    case 7: return "-1 Reaction adjustment";
                    case 13: return "+1 Reaction adjustment";
                    case 14: return "+2 Reaction adjustment";
                    case 15: return "+3 Reaction adjustment";
                    case 16: return "+5 Reaction adjustment";
                    case 17: return "+6 Reaction adjustment";
                    case 18: return "+7 Reaction adjustment";
                    case 19: return "+8 Reaction adjustment";
                    case 20: return "+9 Reaction adjustment";
                    case 21: return "+10 Reaction adjustment";
                    case 22: return "+11 Reaction adjustment";
                    case 23: return "+12 Reaction adjustment";
                    case 24: return "+13 Reaction adjustment";
                    case 25: return "+14 Reaction adjustment";
                    default: return "";
                }
            }
        }

        public Character(Dice.ROLLMETHOD RollMethod, Races Race, ALIGNMENT[] Alignment, Classes.CLASS Class, int Level, ref System.Windows.Forms.RichTextBox Progress, ref Random random)
        {
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nInitializing NPC object...";
            this.Class = Class;
            this.Level = Level;
            this.Alignment = Alignment;
            this.Race = Race;
            Progress.Text += "Initialized";

            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nRolling stats...";
            Stats = Dice.Roll(RollMethod, ref random);
            foreach (int stat in Stats)
                Progress.Text += " " + stat.ToString();
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nPrioritizing and adjusting stats...";
            Stats = Classes.Prioritize(Class, Stats);
            StrengthPercentile = 0;

            if (IsFighter(Class))
                if (Stats[(int)STATS.STRENGTH] > 17 && Stats[(int)STATS.STRENGTH] < 19)
                    StrengthPercentile = Dice.Percentile(ref random);

            StatAdjustment();
            foreach (int stat in Stats)
                Progress.Text += " " + stat.ToString();
            Progress.Text += " " + StrengthPercentile.ToString();

            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nDetermining HP...";
            HP = Classes.HitPoints(Class, Level, Stats[(int)STATS.CONSTITUTION], Race, ref random);
            Progress.Text += HP.ToString();

            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nDetermining Languages...";
            Languages = SetLanguages(ref random);
            Progress.Text += Languages;

            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nDetermining THAC0...";
            SetTHACO();
            Progress.Text += THACO[0].ToString() + ", " + THACO[1].ToString();

            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nDetermining gear...";
            //Variables for determining magical abilities of NPC's items
            Equipment = new string[Enum.GetNames(typeof(ITEMTYPE)).Length];
            int Bonus; bool TwoHanded = false;

            //Generate weapon
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating weapon...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.WEAPON), ref random);
            Equipment[(int)ITEMTYPE.WEAPON] = Weapons.Generate(Bonus, Class, Level, true, ref TwoHanded, ref random);
            if (Class == Classes.CLASS.MONK)
                Equipment[(int)ITEMTYPE.WEAPON] += String.Format("\nUnarmed Strike: 1d{0}", Classes.MonkDamage(Level));
            Progress.Text += Equipment[(int)ITEMTYPE.WEAPON];

            //Generate armor
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating armor...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.ARMOR), ref random);
            Equipment[(int)ITEMTYPE.ARMOR] = Armor.GenerateArmor(Bonus, Class, Level, ref random);
            Progress.Text += Equipment[(int)ITEMTYPE.ARMOR];

            //Generate rogue abilities, if applicable
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating rogue abilities...";
            bool[] ArmorBool = new bool[3];
            ArmorBool[0] = (Equipment[(int)ITEMTYPE.ARMOR] == "");
            ArmorBool[1] = (Equipment[(int)ITEMTYPE.ARMOR].Contains("chain") || Equipment[(int)ITEMTYPE.ARMOR].Contains("mithral"));
            ArmorBool[2] = (Equipment[(int)ITEMTYPE.ARMOR].Contains("padded") || Equipment[(int)ITEMTYPE.ARMOR].Contains("leather"));
            if (Class == Classes.CLASS.THIEF)
                RogueAbilities = Classes.ThiefAbilities(Level, Race.Race, Stats[(int)STATS.DEXTERITY], ArmorBool, ref random);
            else if (Class == Classes.CLASS.BARD)
                RogueAbilities = Classes.BardAbilities(Level, Race.Race, Stats[(int)STATS.DEXTERITY], ArmorBool, ref random);
            if (RogueAbilities != null)
                foreach (int abil in RogueAbilities)
                    Progress.Text += " " + abil.ToString();

            //Generate shield or dual-wield weapon
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating shield or dual-wield weapon...";
            if (!TwoHanded)
            {
                Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.SHIELD), ref random);
                if (Armor.CanWield(Class, false) && Stats[(int)STATS.DEXTERITY] > 18)
                {
                    if (Dice.Percentile(ref random) > 50)
                        Equipment[(int)ITEMTYPE.SHIELD] = Weapons.Generate(Bonus, Class, Level, false, ref TwoHanded, ref random);
                    else
                        Equipment[(int)ITEMTYPE.SHIELD] = Armor.GenerateShield(Bonus, Class, Level, ref random);
                }
                else if (Stats[(int)STATS.DEXTERITY] > 18)
                    Equipment[(int)ITEMTYPE.SHIELD] = Weapons.Generate(Bonus, Class, Level, false, ref TwoHanded, ref random);
                else if (Armor.CanWield(Class, false))
                    Equipment[(int)ITEMTYPE.SHIELD] = Armor.GenerateShield(Bonus, Class, Level, ref random);
            }
            Progress.Text += Equipment[(int)ITEMTYPE.SHIELD];

            //Generate bracers
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating bracers...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.BRACERS), ref random);
            if (Bonus > 0)
                Equipment[(int)ITEMTYPE.BRACERS] = String.Format("Bracers AC {0}", 10 - 2 * Bonus);
            Progress.Text += Equipment[(int)ITEMTYPE.BRACERS];

            //Generate rings of protection
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating ring of protection...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.RING_OF_PROTECTION), ref random);
            if (Bonus > 0)
                Equipment[(int)ITEMTYPE.RING_OF_PROTECTION] = String.Format("Ring of Protection +{0}", Bonus);
            Progress.Text += Equipment[(int)ITEMTYPE.RING_OF_PROTECTION];

            //Generate potions.  Bonus is quantity
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating potions...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.POTION), ref random);
            Equipment[(int)ITEMTYPE.POTION] = MagicItems.Potion(MagicItems.PowerByLevel(Level, ref random), Bonus, ref random);
            Progress.Text += Equipment[(int)ITEMTYPE.POTION];

            //Generate scrolls.  Bonus is quantity
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating scrolls...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.SCROLL), ref random);
            Equipment[(int)ITEMTYPE.SCROLL] = Scrolls.Generate(Class, Level, Bonus, ref random);
            Progress.Text += Equipment[(int)ITEMTYPE.SCROLL];

            //Generate miscellaneous items.  Bonus is quantity
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating miscellaneous items...";
            Bonus = BonusByChance(Level * PercentageChance(Class, ITEMTYPE.MISCELLANEOUS), ref random);
            if (Bonus > 0)
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] = MagicItems.Generate(MagicItems.PowerByLevel(Level, ref random), Bonus, ref random);
            Equipment[(int)ITEMTYPE.MISCELLANEOUS] += "\n" + Treasure.MundaneItems(ref random);
            if (Dice.Percentile(ref random) > 10)
            {
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += "\nPack:";
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += String.Format("\n\tRations ({0} Days)", Dice.d8(ref random) - 1);
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += String.Format("\n\t{0}' rope", Dice.Percentile(ref random)/2);
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += String.Format("\n\t{0} torches", Dice.d4(ref random) - 1);
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += String.Format("\n\tflint & steel");
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += String.Format("\n\twhetstone");
                Equipment[(int)ITEMTYPE.MISCELLANEOUS] += String.Format("\n\t{0} spikes", Dice.d4(ref random) - 1);
            }
            Progress.Text += Equipment[(int)ITEMTYPE.MISCELLANEOUS];

            //Generate money
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating money, gems, and art objects...";
            Equipment[(int)ITEMTYPE.MONEY] = Treasure.Money(Level, ref random);
            Progress.Text += Equipment[(int)ITEMTYPE.MONEY];

            //Generate spells
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating spells...";
            if (Class == Classes.CLASS.BARD || Class == Classes.CLASS.WIZARD || Class == Classes.CLASS.CLERIC || Class == Classes.CLASS.DRUID || Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.RANGER || Class == Classes.CLASS.SORCERER)
                Equipment[(int)ITEMTYPE.SPELLS] = String.Format("{0} spells: {1}\n", Class.ToString(), Classes.SpellString(Class, Level, Stats[(int)STATS.INTELLIGENCE], Stats[(int)STATS.WISDOM], Stats[(int)STATS.CHARISMA], ref random));
            Progress.Text += Equipment[(int)ITEMTYPE.SPELLS];

            //Generate familiars
            System.Windows.Forms.Application.DoEvents();
            Progress.Text += "\nGenerating familiars...";
            Equipment[(int)ITEMTYPE.FAMILIARS] = Classes.Familiar(Class, Level, ref random);
            Progress.Text += Equipment[(int)ITEMTYPE.FAMILIARS];
        }

        private string SetLanguages(ref Random random)
        {
            int NumberOfLanguages = (Stats[(int)STATS.INTELLIGENCE] - 10) / 2;
            string languages;

            switch (Race.Race)
            {
                case Races.RACE.AASIMAR: languages = "Common, Celestial"; break;
                case Races.RACE.GOBLIN:
                case Races.RACE.HOBGOBLIN:
                case Races.RACE.BUGBEAR: languages = "Common, Goblin"; break;
                case Races.RACE.DUERGAR: languages = "Common, Dwarven, Undercommon"; break;
                case Races.RACE.HILL_DWARF:
                case Races.RACE.MOUNTAIN_DWARF:
                case Races.RACE.DEEP_DWARF:
                case Races.RACE.DERRO_DWARF: languages = "Common, Dwarven"; break;
                case Races.RACE.DROW: languages = "Common, Elven, Undercommon"; break;
                case Races.RACE.FOREST_GNOME: languages = "Common, Gnome, Elven, Sylvan"; break;
                case Races.RACE.GNOLL: languages = "Gnoll"; break;
                case Races.RACE.GRAY_ELF:
                case Races.RACE.HIGH_ELF:
                case Races.RACE.WILD_ELF:
                case Races.RACE.WOOD_ELF:
                case Races.RACE.HALFELF: languages = "Common, Elven"; break;
                case Races.RACE.ORC:
                case Races.RACE.HALFORC: languages = "Common, Orc"; break;
                case Races.RACE.LIZARDFOLK: languages = "Common, Draconic"; break;
                case Races.RACE.TROGLODYTE:
                case Races.RACE.KOBOLD: languages = "Draconic"; break;
                case Races.RACE.DEEP_HALFLING:
                case Races.RACE.TALLFELLOW_HALFLING:
                case Races.RACE.LIGHTFOOT_HALFLING: languages = "Common, Halfling"; break;
                case Races.RACE.MIND_FLAYER: languages = "Common, Undercommon"; break;
                case Races.RACE.OGRE:
                case Races.RACE.OGRE_MAGE:
                case Races.RACE.MINOTAUR: languages = "Common, Giant"; break;
                case Races.RACE.ROCK_GNOME: languages = "Common, Gnome"; break;
                case Races.RACE.SVIRFNEBLIN: languages = "Common, Gnome, Undercommon"; break;
                case Races.RACE.TIEFLING: languages = "Common, Infernal"; break;
                default: languages = "Common"; break;
            }

            switch (Race.MetaRace)
            {
                case Races.METARACE.HALF_CELESTIAL:
                    if (!languages.Contains("Celestial"))
                        languages += ", Celestial";
                    break;
                case Races.METARACE.HALF_DRAGON:
                    if (!languages.Contains("Draconic"))
                        languages += ", Draconic";
                    break;
                case Races.METARACE.HALF_FIEND:
                    if (!languages.Contains("Infernal"))
                        languages += ", Infernal";
                    break;
                default: break;
            }

            if (Class == Classes.CLASS.DRUID)
                languages += ", Druidic";
            
            while (NumberOfLanguages > 0)
            {
                switch (Dice.d20(ref random))
                {
                    case 1:
                        if (!languages.Contains("Abyssal"))
                        {
                            if (Class == Classes.CLASS.CLERIC || Race.CanSpeak("Abyssal"))
                            {
                                NumberOfLanguages--;
                                languages += ", Abyssal";
                            }
                        }
                        break;
                    case 2:
                        if (!languages.Contains("Aquan"))
                        {
                            if (Race.CanSpeak("Aquan"))
                            {
                                NumberOfLanguages--;
                                languages += ", Aquan";
                            }
                        }
                        break;
                    case 3:
                        if (!languages.Contains("Auran"))
                        {
                            if (Race.CanSpeak("Auran"))
                            {
                                NumberOfLanguages--;
                                languages += ", Auran";
                            }
                        }
                        break;
                    case 4:
                        if (!languages.Contains("Celestial"))
                        {
                            if (Class == Classes.CLASS.CLERIC || Race.CanSpeak("Celestial"))
                            {
                                NumberOfLanguages--;
                                languages += ", Celestial";
                            }
                        }
                        break;
                    case 5:
                        if (!languages.Contains("Common"))
                        {
                            NumberOfLanguages--;
                            languages += ", Common";
                        }
                        break;
                    case 6:
                        if (!languages.Contains("Draconic"))
                        {
                            if (Class == Classes.CLASS.WIZARD || Race.CanSpeak("Draconic"))
                            {
                                NumberOfLanguages--;
                                languages += ", Draconic";
                            }
                        }
                        break;
                    case 7: break; //Druidic
                    case 8:
                        if (!languages.Contains("Dwarven"))
                        {
                            if (Race.CanSpeak("Dwarven"))
                            {
                                NumberOfLanguages--;
                                languages += ", Dwarven";
                            }
                        }
                        break;
                    case 9:
                        if (!languages.Contains("Elven"))
                        {
                            if (Race.CanSpeak("Elven"))
                            {
                                NumberOfLanguages--;
                                languages += ", Elven";
                            }
                        }
                        break;
                    case 10:
                        if (!languages.Contains("Giant"))
                        {
                            if (Race.CanSpeak("Giant"))
                            {
                                NumberOfLanguages--;
                                languages += ", Giant";
                            }
                        }
                        break;
                    case 11:
                        if (!languages.Contains("Gnome"))
                        {
                            if (Race.CanSpeak("Gnome"))
                            {
                                NumberOfLanguages--;
                                languages += ", Gnome";
                            }
                        }
                        break;
                    case 12:
                        if (!languages.Contains("Goblin"))
                        {
                            if (Race.CanSpeak("Goblin"))
                            {
                                NumberOfLanguages--;
                                languages += ", Goblin";
                            }
                        }
                        break;
                    case 13:
                        if (!languages.Contains("Gnoll"))
                        {
                            if (Race.CanSpeak("Gnoll"))
                            {
                                NumberOfLanguages--;
                                languages += ", Gnoll";
                            }
                        }
                        break;
                    case 14:
                        if (!languages.Contains("Halfling"))
                        {
                            if (Race.CanSpeak("Halfling"))
                            {
                                NumberOfLanguages--;
                                languages += ", Halfling";
                            }
                        }
                        break;
                    case 15:
                        if (!languages.Contains("Ignan"))
                        {
                            if (Race.CanSpeak("Ignan"))
                            {
                                NumberOfLanguages--;
                                languages += ", Ignan";
                            }
                        }
                        break;
                    case 16:
                        if (!languages.Contains("Infernal"))
                        {
                            if (Class == Classes.CLASS.CLERIC || Race.CanSpeak("Infernal"))
                            {
                                NumberOfLanguages--;
                                languages += ", Infernal";
                            }
                        }
                        break;
                    case 17:
                        if (!languages.Contains("Orc"))
                        {
                            if (Race.CanSpeak("Orc"))
                            {
                                NumberOfLanguages--;
                                languages += ", Orc";
                            }
                        }
                        break;
                    case 18:
                        if (!languages.Contains("Sylvan"))
                        {
                            if (Class == Classes.CLASS.DRUID || Race.CanSpeak("Sylvan"))
                            {
                                NumberOfLanguages--;
                                languages += ", Sylvan";
                            }
                        }
                        break;
                    case 19:
                        if (!languages.Contains("Terran"))
                        {
                            if (Race.CanSpeak("Terran"))
                            {
                                NumberOfLanguages--;
                                languages += ", Terran";
                            }
                        }
                        break;
                    case 20:
                        if (!languages.Contains("Undercommon"))
                        {
                            if (Race.CanSpeak("Undercommon"))
                            {
                                NumberOfLanguages--;
                                languages += ", Undercommon";
                            }
                        }
                        break;
                    default: return "[ERROR: d20 out of loop.  Character.197]";
                }
            }

            return languages;
        }

        private void SetTHACO()
        {
            THACO = new int[2] { 20, 20 };
            switch (Class)
            {
                case Classes.CLASS.BARBARIAN:
                case Classes.CLASS.FIGHTER:
                case Classes.CLASS.MONK:
                case Classes.CLASS.PALADIN:
                case Classes.CLASS.RANGER:
                    THACO[0] -= (Level - 1);
                    THACO[1] -= (Level - 1);
                    break;
                case Classes.CLASS.CLERIC:
                case Classes.CLASS.DRUID:
                    THACO[0] -= ((Level - 1) / 3) * 2;
                    THACO[1] -= ((Level - 1) / 3) * 2;
                    break;
                case Classes.CLASS.SORCERER:
                case Classes.CLASS.WIZARD:
                    THACO[0] -= ((Level - 1) / 3);
                    THACO[1] -= ((Level - 1) / 3);
                    break;
                case Classes.CLASS.THIEF:
                case Classes.CLASS.BARD:
                    THACO[0] -= ((Level - 1) / 2);
                    THACO[1] -= ((Level - 1) / 2);
                    break;
                default: break;
            }

            switch (Stats[(int)STATS.STRENGTH])
            {
                case 1:
                    THACO[0] -= -5;
                    THACO[1] -= -5; break;
                case 2:
                case 3:
                    THACO[0] -= -3;
                    THACO[1] -= -3; break;
                case 4:
                case 5:
                    THACO[0] -= -2;
                    THACO[1] -= -2; break;
                case 6:
                case 7:
                    THACO[0] -= -1;
                    THACO[1] -= -1; break;
                case 17:
                case 18:
                    if (StrengthPercentile < 51)
                    {
                        THACO[0] -= 1;
                        THACO[1] -= 1;
                    }
                    else if (StrengthPercentile < 100)
                    {
                        THACO[0] -= 2;
                        THACO[1] -= 2;
                    }
                    else
                    {
                        THACO[0] -= 3;
                        THACO[1] -= 3;
                    }
                    break;
                case 19:
                case 20:
                    THACO[0] -= 3;
                    THACO[1] -= 3;
                    break;
                case 21:
                case 22:
                    THACO[0] -= 4;
                    THACO[1] -= 4;
                    break;
                case 23:
                    THACO[0] -= 5;
                    THACO[1] -= 5;
                    break;
                case 24:
                    THACO[0] -= 6;
                    THACO[1] -= 6;
                    break;
                case 25:
                    THACO[0] -= 7;
                    THACO[1] -= 7;
                    break;
                default: break;
            }

            switch (Stats[(int)STATS.DEXTERITY])
            {
                case 1: THACO[1] -= -6; break;
                case 2: THACO[1] -= -4; break;
                case 3: THACO[1] -= -3; break;
                case 4: THACO[1] -= -2; break;
                case 5: THACO[1] -= -1; break;
                case 16: THACO[1] -= 1; break;
                case 17:
                case 18: THACO[1] -= 2; break;
                case 19:
                case 20: THACO[1] -= 3; break;
                case 21:
                case 22:
                case 23: THACO[1] -= 4; break;
                case 24:
                case 25: THACO[1] -= 5; break;
                default: break;
            }
        }

        private static int BonusByChance(int Chance, ref Random random)
        {
            int Bonus = 0; int Roll;
            while (Chance > 100)
            {
                Bonus++;
                Chance -= 100;
            }
            while (Chance > 0)
            {
                Roll = Dice.Percentile(ref random);
                if (Roll <= Chance)
                    Bonus++;
                Chance -= Roll;
            }

            return Bonus;
        }

        public static ALIGNMENT[] RandomAlignment(ALIGNMENT_RANDOMIZER Randomizer, ref Random random)
        {
            int Roll; ALIGNMENT[] TempAlignment = new ALIGNMENT[2] { ALIGNMENT.LAWFUL, ALIGNMENT.GOOD };
            
            switch (Randomizer)
            {
                case ALIGNMENT_RANDOMIZER.ANY_CHAOTIC:
                    TempAlignment[0] = ALIGNMENT.CHAOTIC;
                    Roll = Dice.Percentile(ref random);
                    if (Roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (Roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_EVIL:
                    TempAlignment[1] = ALIGNMENT.EVIL;
                    switch (Dice.Roll(1, 3, 0, ref random))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_GOOD:
                    TempAlignment[1] = ALIGNMENT.GOOD;
                    switch (Dice.Roll(1, 3, 0, ref random))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_LAWFUL:
                    TempAlignment[0] = ALIGNMENT.LAWFUL;
                    Roll = Dice.Percentile(ref random);
                    if (Roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (Roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NEUTRAL:
                    switch (Dice.Roll(1, 3, 0, ref random))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    if (TempAlignment[0] == ALIGNMENT.NEUTRAL)
                    {
                        Roll = Dice.Percentile(ref random);
                        if (Roll < 21)
                            TempAlignment[1] = ALIGNMENT.GOOD;
                        else if (Roll < 51)
                            TempAlignment[1] = ALIGNMENT.NEUTRAL;
                        else
                            TempAlignment[1] = ALIGNMENT.EVIL;
                    }
                    else
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONCHAOTIC:
                    TempAlignment[0] = ALIGNMENT.LAWFUL;
                    if (Dice.Percentile(ref random) < 51)
                        TempAlignment[0] = ALIGNMENT.NEUTRAL;
                    Roll = Dice.Percentile(ref random);
                    if (Roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (Roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONEVIL:
                    switch (Dice.Roll(1, 3, 0, ref random))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    TempAlignment[1] = ALIGNMENT.GOOD;
                    if (Dice.Roll(1, 5, 0, ref random) > 2)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONGOOD:
                    switch (Dice.Roll(1, 3, 0, ref random))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    TempAlignment[1] = ALIGNMENT.EVIL;
                    if (Dice.Roll(1, 8, 0, ref random) < 4)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONLAWFUL:
                    TempAlignment[0] = ALIGNMENT.CHAOTIC;
                    if (Dice.Percentile(ref random) < 51)
                        TempAlignment[0] = ALIGNMENT.NEUTRAL;
                    Roll = Dice.Percentile(ref random);
                    if (Roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (Roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONNEUTRAL:
                    TempAlignment[0] = ALIGNMENT.LAWFUL;
                    if (Dice.Percentile(ref random) < 51)
                        TempAlignment[0] = ALIGNMENT.CHAOTIC;
                    TempAlignment[1] = ALIGNMENT.EVIL;
                    if (Dice.Roll(1, 7, 0, ref random) < 3)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY:
                    switch (Dice.Roll(1, 3, 0, ref random))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    Roll = Dice.Percentile(ref random);
                    if (Roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (Roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                default: return TempAlignment;
            }
        }

        private static bool IsFighter(Classes.CLASS Class)
        {
            switch (Class)
            {
                case Classes.CLASS.BARBARIAN:
                case Classes.CLASS.FIGHTER:
                case Classes.CLASS.MONK:
                case Classes.CLASS.PALADIN:
                case Classes.CLASS.RANGER: return true;
                default: return false;
            }
        }

        public static ALIGNMENT[] StringToAlignment(string Input)
        {
            ALIGNMENT[] output = new ALIGNMENT[2];

            if (Input.StartsWith("Lawful"))
                output[0] = ALIGNMENT.LAWFUL;
            else if (Input.StartsWith("Neutral") || Input.StartsWith("True"))
                output[0] = ALIGNMENT.NEUTRAL;
            else
                output[0] = ALIGNMENT.CHAOTIC;

            if (Input.EndsWith("Good"))
                output[1] = ALIGNMENT.GOOD;
            else if (Input.EndsWith("Neutral"))
                output[1] = ALIGNMENT.NEUTRAL;
            else
                output[1] = ALIGNMENT.EVIL;

            return output;
        }

        public void StatAdjustment()
        {
            switch (Race.Race)
            {
                case Races.RACE.HILL_DWARF:
                case Races.RACE.MOUNTAIN_DWARF:
                    Stats[(int)STATS.CONSTITUTION]++;
                    Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.HIGH_ELF:
                    Stats[(int)STATS.DEXTERITY]++;
                    Stats[(int)STATS.CONSTITUTION]--;
                    break;
                case Races.RACE.ROCK_GNOME:
                case Races.RACE.FOREST_GNOME:
                    Stats[(int)STATS.CONSTITUTION]++;
                    AdjustStrength(-1);
                    break;
                case Races.RACE.DEEP_HALFLING:
                case Races.RACE.LIGHTFOOT_HALFLING:
                case Races.RACE.TALLFELLOW_HALFLING:
                    Stats[(int)STATS.DEXTERITY]++;
                    AdjustStrength(-1);
                    break;
                case Races.RACE.HALFORC:
                    AdjustStrength(2);
                    Stats[(int)STATS.CHARISMA]--; Stats[(int)STATS.INTELLIGENCE]--;
                    break;
                case Races.RACE.AASIMAR:
                    Stats[(int)STATS.CHARISMA]++; Stats[(int)STATS.WISDOM]++;
                    break;
                case Races.RACE.BUGBEAR:
                    AdjustStrength(2); Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.CONSTITUTION]++;
                    Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.DERRO_DWARF:
                    Stats[(int)STATS.DEXTERITY] += 2; Stats[(int)STATS.CONSTITUTION]++;
                    Stats[(int)STATS.CHARISMA] -= 2; AdjustStrength(-1);
                    break;
                case Races.RACE.DOPPELGANGER:
                    AdjustStrength(1); Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.CONSTITUTION]++; Stats[(int)STATS.INTELLIGENCE]++; Stats[(int)STATS.CHARISMA]++; Stats[(int)STATS.WISDOM] += 2;
                    break;
                case Races.RACE.DROW:
                    Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.CONSTITUTION]++;
                    if (Race.Male)
                        Stats[(int)STATS.CHARISMA]--;
                    else
                        Stats[(int)STATS.CHARISMA]++;
                    break;
                case Races.RACE.DUERGAR:
                case Races.RACE.DEEP_DWARF:
                    Stats[(int)STATS.CONSTITUTION]++;
                    Stats[(int)STATS.CHARISMA] -= 2;
                    break;
                case Races.RACE.GNOLL:
                    AdjustStrength(2); Stats[(int)STATS.CONSTITUTION]++;
                    Stats[(int)STATS.INTELLIGENCE]--; Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.GOBLIN:
                    Stats[(int)STATS.DEXTERITY]++;
                    AdjustStrength(-1); Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.GRAY_ELF:
                    Stats[(int)STATS.INTELLIGENCE]++; Stats[(int)STATS.DEXTERITY]++;
                    Stats[(int)STATS.CONSTITUTION]--; AdjustStrength(-1); 
                    break;
                case Races.RACE.HOBGOBLIN:
                    Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.CONSTITUTION]++;
                    break;
                case Races.RACE.KOBOLD:
                    Stats[(int)STATS.DEXTERITY]++;
                    AdjustStrength(-2);
                    break;
                case Races.RACE.LIZARDFOLK:
                    AdjustStrength(1); Stats[(int)STATS.CONSTITUTION]++;
                    Stats[(int)STATS.INTELLIGENCE]--;
                    break;
                case Races.RACE.MIND_FLAYER:
                    AdjustStrength(1); Stats[(int)STATS.DEXTERITY] += 2; Stats[(int)STATS.CONSTITUTION]++; Stats[(int)STATS.INTELLIGENCE] += 4; Stats[(int)STATS.CHARISMA] += 3; Stats[(int)STATS.WISDOM] += 3;
                    break;
                case Races.RACE.MINOTAUR:
                    AdjustStrength(4); Stats[(int)STATS.CONSTITUTION] += 2; Stats[(int)STATS.INTELLIGENCE] -= 2; Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.OGRE:
                    AdjustStrength(5); Stats[(int)STATS.DEXTERITY]--; Stats[(int)STATS.CONSTITUTION] += 2; Stats[(int)STATS.INTELLIGENCE] -= 2; Stats[(int)STATS.CHARISMA] -= 2;
                    break;
                case Races.RACE.OGRE_MAGE:
                    AdjustStrength(5); Stats[(int)STATS.CONSTITUTION] += 3; Stats[(int)STATS.INTELLIGENCE] += 2; Stats[(int)STATS.WISDOM] += 2; Stats[(int)STATS.CHARISMA] += 3;
                    break;
                case Races.RACE.ORC:
                    AdjustStrength(2);
                    Stats[(int)STATS.INTELLIGENCE]--; Stats[(int)STATS.WISDOM]--; Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.SVIRFNEBLIN:
                    Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.WISDOM]++;
                    Stats[(int)STATS.CHARISMA] -= 2;
                    break;
                case Races.RACE.TIEFLING:
                    Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.INTELLIGENCE]++;
                    Stats[(int)STATS.CHARISMA]--;
                    break;
                case Races.RACE.TROGLODYTE:
                    Stats[(int)STATS.CONSTITUTION] += 2;
                    Stats[(int)STATS.DEXTERITY]--; Stats[(int)STATS.INTELLIGENCE]--;
                    break;
                case Races.RACE.WILD_ELF:
                    Stats[(int)STATS.DEXTERITY]++;
                    Stats[(int)STATS.INTELLIGENCE]--;
                    break;
                case Races.RACE.WOOD_ELF:
                    Stats[(int)STATS.DEXTERITY]++; AdjustStrength(1);
                    Stats[(int)STATS.CONSTITUTION]--; Stats[(int)STATS.INTELLIGENCE]--;
                    break;
                default: break;
            }

            switch (Race.MetaRace)
            {
                case Races.METARACE.HALF_CELESTIAL:
                    AdjustStrength(2); Stats[(int)STATS.DEXTERITY]++; Stats[(int)STATS.CONSTITUTION] += 2; Stats[(int)STATS.INTELLIGENCE]++; Stats[(int)STATS.CHARISMA] += 2; Stats[(int)STATS.WISDOM] += 2;
                    break;
                case Races.METARACE.HALF_DRAGON:
                    AdjustStrength(4); Stats[(int)STATS.CONSTITUTION]++; Stats[(int)STATS.INTELLIGENCE]++; Stats[(int)STATS.CHARISMA]++;
                    break;
                case Races.METARACE.HALF_FIEND:
                    AdjustStrength(2); Stats[(int)STATS.DEXTERITY] += 2; Stats[(int)STATS.CONSTITUTION]++; Stats[(int)STATS.INTELLIGENCE] += 2; Stats[(int)STATS.CHARISMA]++;
                    break;
                default: break;
            }
        }

        public void AdjustStrength(int Adjustment)
        {
            while (Adjustment != 0)
            {
                if (Adjustment < 0)
                {
                    if (Stats[(int)STATS.STRENGTH] != 18 || StrengthPercentile == 0)
                    {
                        Stats[(int)STATS.STRENGTH]--;
                        if (Stats[(int)STATS.STRENGTH] == 18)
                            StrengthPercentile = 100;
                        else
                            StrengthPercentile = 0;
                        Adjustment++;
                    }
                    else
                    {
                        if (StrengthPercentile == 100)
                            StrengthPercentile = 91;
                        else if (StrengthPercentile > 90)
                            StrengthPercentile = 76;
                        else if (StrengthPercentile > 75)
                            StrengthPercentile = 51;
                        else if (StrengthPercentile > 50)
                            StrengthPercentile = 1;
                        else
                            StrengthPercentile = 0;
                        Adjustment++;
                    }
                }
                else
                {
                    if (Stats[(int)STATS.STRENGTH] != 18 || StrengthPercentile == 100)
                    {
                        Stats[(int)STATS.STRENGTH]++;
                        StrengthPercentile = 0;
                        Adjustment--;
                    }
                    else
                    {
                        if (StrengthPercentile == 0)
                            StrengthPercentile = 1;
                        else if (StrengthPercentile < 51)
                            StrengthPercentile = 51;
                        else if (StrengthPercentile < 76)
                            StrengthPercentile = 76;
                        else if (StrengthPercentile < 91)
                            StrengthPercentile = 91;
                        else
                            StrengthPercentile = 100;
                        Adjustment--;
                    }
                }
            }
        }

        public override string ToString()
        {
            string output = String.Format("{0} {1} Level {2}", Race.ToString(), Class, Level);

            output += "\n" + Alignment[0].ToString() + " " + Alignment[1].ToString();
            output += String.Format(" HP {0}, THAC0: {1} Melee, {2} Missile\n", HP, THACO[0], THACO[1]);
            string StrengthString = String.Format("{0}", Stats[(int)STATS.STRENGTH]);
            if (StrengthPercentile != 0)
                StrengthString += String.Format("/{0}", StrengthPercentile);
            output += String.Format("STR {0} CON {1} DEX {2} INT {3} WIS {4} CHA {5}",
                StrengthString, Stats[(int)STATS.CONSTITUTION], Stats[(int)STATS.DEXTERITY], Stats[(int)STATS.INTELLIGENCE],
                Stats[(int)STATS.WISDOM], Stats[(int)STATS.CHARISMA]);

            if (StatAbilities != "")
                output += "\n" + StatAbilities;

            output += "\nLANGUAGES: " + Languages;

            if (Race.RacialTraits != "" || Race.MetaRacialTraits != "")
            {
                if (Race.RacialTraits != "" && Race.MetaRacialTraits != "")
                    output += String.Format("\n{0}: {1}, {2}", Race.ToString(), Race.RacialTraits, Race.MetaRacialTraits);
                else
                    output += String.Format("\n{0}: {1}{2}", Race.ToString(), Race.RacialTraits, Race.MetaRacialTraits);
            }

            if (Classes.Abilities(Class, Level, Stats[(int)STATS.WISDOM]) != "")
                output += "\n" + Class.ToString() + ": " + Classes.Abilities(Class, Level, Stats[(int)STATS.WISDOM]);

            if (RogueAbilities != null)
            {             
                if (Class == Classes.CLASS.BARD)
                {
                    output += String.Format("\nClimb Walls: {0}%", RogueAbilities[0]);
                    output += String.Format("\nDetect Noise: {0}%", RogueAbilities[1]);
                    output += String.Format("\nPick Pockets: {0}%", RogueAbilities[2]);
                    output += String.Format("\nRead Languages: {0}%", RogueAbilities[3]);
                }
                else if (Class == Classes.CLASS.THIEF)
                {
                    output += String.Format("\nPick Pockets: {0}%", RogueAbilities[0]);
                    output += String.Format("\nOpen Locks: {0}%", RogueAbilities[1]);
                    output += String.Format("\nFind/Remove Traps: {0}%", RogueAbilities[2]);
                    output += String.Format("\nMove Silently: {0}%", RogueAbilities[3]);
                    output += String.Format("\nHide in Shadows: {0}%", RogueAbilities[4]);
                    output += String.Format("\nDetect Noise: {0}%", RogueAbilities[5]);
                    output += String.Format("\nClimb Walls: {0}%", RogueAbilities[6]);
                    output += String.Format("\nRead Languages: {0}%", RogueAbilities[7]);
                }
            }

            output += "\n\n" + Equipment[(int)ITEMTYPE.WEAPON];

            if (Equipment[(int)ITEMTYPE.ARMOR] != "")
                output += "\n" + Equipment[(int)ITEMTYPE.ARMOR];
            if (Equipment[(int)ITEMTYPE.RING_OF_PROTECTION] != "")
                output += "\n" + Equipment[(int)ITEMTYPE.RING_OF_PROTECTION];
            if (Equipment[(int)ITEMTYPE.BRACERS] != "")
                output += "\n" + Equipment[(int)ITEMTYPE.BRACERS];
            if (Equipment[(int)ITEMTYPE.SHIELD] != "")
                output += "\n" + Equipment[(int)ITEMTYPE.SHIELD];
            if (Equipment[(int)ITEMTYPE.POTION] != "")
                output += "\n\n" + Equipment[(int)ITEMTYPE.POTION];
            if (Equipment[(int)ITEMTYPE.SCROLL] != "")
                output += "\n\n" + Equipment[(int)ITEMTYPE.SCROLL];
            if (Equipment[(int)ITEMTYPE.MISCELLANEOUS] != "")
                output += "\n\n" + Equipment[(int)ITEMTYPE.MISCELLANEOUS];
            if (Equipment[(int)ITEMTYPE.MONEY] != "")
                output += "\n\n" + Equipment[(int)ITEMTYPE.MONEY];
            if (Equipment[(int)ITEMTYPE.SPELLS] != "")
                output += "\n\n" + Equipment[(int)ITEMTYPE.SPELLS];
            if (Equipment[(int)ITEMTYPE.FAMILIARS] != "")
                output += "\n\n" + Equipment[(int)ITEMTYPE.FAMILIARS];

            return output;
        }

        private int PercentageChance(Classes.CLASS Class, ITEMTYPE Type)
        {
            switch (Type)
            {
                case ITEMTYPE.ARMOR:
                    switch (Class)
                    {
                        case Classes.CLASS.CLERIC:
                        case Classes.CLASS.DRUID: return 8;
                        case Classes.CLASS.FIGHTER:
                        case Classes.CLASS.BARBARIAN:
                        case Classes.CLASS.PALADIN:
                        case Classes.CLASS.BARD:
                        case Classes.CLASS.THIEF: return 10;
                        case Classes.CLASS.RANGER: return 15;
                        default: return 0;
                    }
                case ITEMTYPE.SHIELD:
                    switch (Class)
                    {
                        case Classes.CLASS.CLERIC:
                        case Classes.CLASS.FIGHTER:
                        case Classes.CLASS.BARBARIAN:
                        case Classes.CLASS.PALADIN: return 10;
                        case Classes.CLASS.RANGER:
                        case Classes.CLASS.BARD: return 8;
                        default: return 0;
                    }
                case ITEMTYPE.BRACERS:
                    switch (Class)
                    {
                        case Classes.CLASS.SORCERER:
                        case Classes.CLASS.WIZARD:
                        case Classes.CLASS.MONK: return 4;
                        default: return 0;
                    }
                case ITEMTYPE.RING_OF_PROTECTION:
                    switch (Class)
                    {
                        case Classes.CLASS.CLERIC: return 2;
                        case Classes.CLASS.DRUID: return 5;
                        case Classes.CLASS.SORCERER:
                        case Classes.CLASS.MONK:
                        case Classes.CLASS.WIZARD: return 15;
                        case Classes.CLASS.BARD: return 3;
                        case Classes.CLASS.THIEF: return 4;
                        default: return 0;
                    }
                case ITEMTYPE.WEAPON:
                    switch (Class)
                    {
                        case Classes.CLASS.CLERIC: return 12;
                        case Classes.CLASS.DRUID:
                        case Classes.CLASS.FIGHTER:
                        case Classes.CLASS.BARBARIAN:
                        case Classes.CLASS.PALADIN:
                        case Classes.CLASS.BARD:
                        case Classes.CLASS.RANGER: return 10;
                        case Classes.CLASS.MONK: return 5;
                        case Classes.CLASS.SORCERER:
                        case Classes.CLASS.WIZARD: return 15;
                        case Classes.CLASS.THIEF: return 12;
                        default: return 0;
                    }
                case ITEMTYPE.SCROLL:
                    switch (Class)
                    {
                        case Classes.CLASS.CLERIC: return 8;
                        case Classes.CLASS.DRUID: return 7;
                        case Classes.CLASS.FIGHTER:
                        case Classes.CLASS.THIEF:
                        case Classes.CLASS.MONK: return 6;
                        case Classes.CLASS.PALADIN: return 4;
                        case Classes.CLASS.RANGER: return 5;
                        case Classes.CLASS.SORCERER: return 12;
                        case Classes.CLASS.WIZARD:
                        case Classes.CLASS.BARD: return 15;
                        default: return 0;
                    }
                case ITEMTYPE.POTION:
                    switch (Class)
                    {
                        case Classes.CLASS.PALADIN:
                        case Classes.CLASS.CLERIC: return 6;
                        case Classes.CLASS.DRUID: return 11;
                        case Classes.CLASS.FIGHTER: return 8;
                        case Classes.CLASS.SORCERER:
                        case Classes.CLASS.WIZARD:
                        case Classes.CLASS.BARD:
                        case Classes.CLASS.MONK: return 10;
                        case Classes.CLASS.RANGER: return 7;
                        case Classes.CLASS.THIEF: return 9;
                        default: return 0;
                    }
                case ITEMTYPE.MISCELLANEOUS:
                    switch (Class)
                    {
                        case Classes.CLASS.DRUID:
                        case Classes.CLASS.PALADIN: return 3;
                        case Classes.CLASS.RANGER: return 4;
                        case Classes.CLASS.SORCERER: return 8;
                        case Classes.CLASS.WIZARD: return 9;
                        case Classes.CLASS.BARD: return 6;
                        case Classes.CLASS.FIGHTER:
                        case Classes.CLASS.CLERIC:
                        case Classes.CLASS.MONK:
                        case Classes.CLASS.THIEF: return 5;
                        default: return 0;
                    }
                default: return 0;
            }
        }

        public static string RandomAlignment(ref Random random)
        {
            switch (Dice.Roll(1, 9, 0, ref random))
            {
                case 1: return "lawful good";
                case 2: return "neutral good";
                case 3: return "chaotic good";
                case 4: return "lawful neutral";
                case 5: return "true neutral";
                case 6: return "chaotic neutral";
                case 7: return "lawful evil";
                case 8: return "neutral evil";
                default: return "chaotic evil";
            }
        }

        public static string RandomGender(ref Random random, bool ExtraGenders)
        {
            int limit = 2;
            if (ExtraGenders)
                limit = 4;

            switch (Dice.Roll(1, limit, 0, ref random))
            {
                case 1: return "male";
                case 2: return "female";
                case 3: return "both";
                default: return "neither";
            }
        }

        public static string RandomClass(ref Random random)
        {
            switch (Dice.Roll(1, 11, 0, ref random))
            {
                case 1: return "barbarian";
                case 2: return "bard";
                case 3: return "cleric";
                case 4: return "druid";
                case 5: return "fighter";
                case 6: return "monk";
                case 7: return "paladin";
                case 8: return "ranger";
                case 9: return "rogue";
                case 10: return "sorcerer";
                default: return "wizard";
            }
        }

        public static string HumanoidSubtype(ref Random random)
        {
            switch (Dice.Roll(1, 510, 0, ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16: return "aasimar";
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32: return "deep dwarf";
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48: return "hill dwarf";
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64: return "mountain dwarf";
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "gray elf";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96: return "high elf";
                case 97:
                case 98:
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                case 108:
                case 109:
                case 110:
                case 111:
                case 112: return "wild elf";
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128: return "wood elf";
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                case 140:
                case 141:
                case 142:
                case 143:
                case 144: return "forest gnome";
                case 145:
                case 146:
                case 147:
                case 148:
                case 149:
                case 150:
                case 151:
                case 152:
                case 153:
                case 154:
                case 155:
                case 156:
                case 157:
                case 158:
                case 159:
                case 160: return "rock gnome";
                case 161:
                case 162:
                case 163:
                case 164:
                case 165:
                case 166:
                case 167:
                case 168:
                case 169:
                case 170:
                case 171:
                case 172:
                case 173:
                case 174:
                case 175:
                case 176: return "half-elf";
                case 177:
                case 178:
                case 179:
                case 180:
                case 181:
                case 182:
                case 183:
                case 184:
                case 185:
                case 186:
                case 187:
                case 188:
                case 189:
                case 190:
                case 191:
                case 192: return "lightfoot halfling";
                case 193:
                case 194:
                case 195:
                case 196:
                case 197:
                case 198:
                case 199:
                case 200:
                case 201:
                case 202:
                case 203:
                case 204:
                case 205:
                case 206:
                case 207:
                case 208: return "deep halfling";
                case 209:
                case 210:
                case 211:
                case 212:
                case 213:
                case 214:
                case 215:
                case 216:
                case 217:
                case 218:
                case 219:
                case 220:
                case 221:
                case 222:
                case 223:
                case 224: return "tallfellow halfling";
                case 225:
                case 226:
                case 227:
                case 228:
                case 229:
                case 230:
                case 231:
                case 232:
                case 233:
                case 234:
                case 235:
                case 236:
                case 237:
                case 238:
                case 239:
                case 240: return "half-orc";
                case 241:
                case 242:
                case 243:
                case 244:
                case 245:
                case 246:
                case 247:
                case 248:
                case 249:
                case 250:
                case 251:
                case 252:
                case 253:
                case 254:
                case 255:
                case 256: return "lizardfolk";
                case 257:
                case 258:
                case 259:
                case 260:
                case 261:
                case 262:
                case 263:
                case 264:
                case 265:
                case 266:
                case 267:
                case 268:
                case 269:
                case 270:
                case 271:
                case 272: return "goblin";
                case 273:
                case 274:
                case 275:
                case 276:
                case 277:
                case 278:
                case 279:
                case 280:
                case 281:
                case 282:
                case 283:
                case 284:
                case 285:
                case 286:
                case 287:
                case 288: return "hobgoblin";
                case 289:
                case 290:
                case 291:
                case 292:
                case 293:
                case 294:
                case 295:
                case 296:
                case 297:
                case 298:
                case 299:
                case 300:
                case 301:
                case 302:
                case 303:
                case 304: return "kobold";
                case 305:
                case 306:
                case 307:
                case 308:
                case 309:
                case 310:
                case 311:
                case 312:
                case 313:
                case 314:
                case 315:
                case 316:
                case 317:
                case 318:
                case 319:
                case 320: return "orc";
                case 321:
                case 322:
                case 323:
                case 324:
                case 325:
                case 326:
                case 327:
                case 328:
                case 329:
                case 330:
                case 331:
                case 332:
                case 333:
                case 334:
                case 335:
                case 336: return "tiefling";
                case 337:
                case 338:
                case 339:
                case 340: return "augmented";
                case 341:
                case 342:
                case 343:
                case 344:
                case 345:
                case 346:
                case 347:
                case 348:
                case 349:
                case 350:
                case 351:
                case 352:
                case 353:
                case 354:
                case 355:
                case 356: return "reptilian humanoid";
                case 357:
                case 358:
                case 359:
                case 360:
                case 361:
                case 362:
                case 363:
                case 364:
                case 365:
                case 366:
                case 367:
                case 368:
                case 369:
                case 370:
                case 371:
                case 372: return "goblinoid";
                case 373:
                case 374:
                case 375:
                case 376:
                case 377:
                case 378:
                case 379:
                case 380: return "svirfneblin";
                case 381:
                case 382:
                case 383:
                case 384:
                case 385:
                case 386:
                case 387:
                case 388: return "half-celestial";
                case 389:
                case 390:
                case 391:
                case 392: return "half-dragon";
                case 393:
                case 394:
                case 395:
                case 396:
                case 397:
                case 398:
                case 399:
                case 400: return "lycanthropes";
                case 401:
                case 402: return "doppelganger";
                case 403:
                case 404:
                case 405:
                case 406: return "werebear";
                case 407:
                case 408:
                case 409:
                case 410:
                case 411:
                case 412:
                case 413:
                case 414: return "wererat";
                case 415:
                case 416:
                case 417:
                case 418:
                case 419:
                case 420:
                case 421:
                case 422: return "wereboar";
                case 423:
                case 424:
                case 425:
                case 426:
                case 427:
                case 428:
                case 429:
                case 430: return "weretiger";
                case 431:
                case 432:
                case 433:
                case 434:
                case 435:
                case 436:
                case 437:
                case 438: return "werewolf";
                case 439:
                case 440:
                case 441:
                case 442: return "half-fiend";
                case 443:
                case 444:
                case 445:
                case 446:
                case 447:
                case 448:
                case 449:
                case 450: return "drow";
                case 451:
                case 452:
                case 453:
                case 454:
                case 455:
                case 456:
                case 457:
                case 458: return "duergar";
                case 459:
                case 460:
                case 461:
                case 462:
                case 463:
                case 464:
                case 465:
                case 466: return "derro dwarf";
                case 467:
                case 468:
                case 469:
                case 470:
                case 471:
                case 472:
                case 473:
                case 474: return "gnoll";
                case 475:
                case 476:
                case 477:
                case 478:
                case 479:
                case 480:
                case 481:
                case 482: return "troglodyte";
                case 483:
                case 484:
                case 485:
                case 486: return "bugbear";
                case 487:
                case 488:
                case 489:
                case 490: return "ogre";
                case 491:
                case 492: return "minotaur";
                case 493: return "mind flayer";
                case 494: return "ogre mage";
                default: return "human";
            }
        }

        public static string CreatureType(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return "aberrations";
                case 6:
                case 7:
                case 8: return "animals";
                case 9:
                case 10:
                case 11:
                case 12:
                case 13: return "beasts";
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return "constructs";
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: return "dragons";
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "elementals";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "fey";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "giants";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "magical beasts";
                case 46:
                case 47:
                case 48:
                case 49:
                case 50: return "monstrous humanoids";
                case 51:
                case 52:
                case 53: return "oozes";
                case 54:
                case 55:
                case 56:
                case 57:
                case 58: return "chaotic outsiders";
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65: return "evil outsiders";
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "good outsiders";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75: return "lawful outsiders";
                case 76:
                case 77: return "plants";
                case 78:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 85: return "shapechangers";
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 91:
                case 92: return "undead";
                case 93:
                case 94: return "vermin";
                default: return HumanoidSubtype(ref random);
            }
        }
    }
}