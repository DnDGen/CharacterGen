using NPCGen.Equipment;
using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NPCGen.Characters
{
    public enum ALIGNMENT { LAWFUL, NEUTRAL, CHAOTIC, GOOD, EVIL };
    public enum ALIGNMENT_RANDOMIZER { ANY_GOOD, ANY_EVIL, ANY_LAWFUL, ANY_CHAOTIC, ANY, ANY_NEUTRAL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL, ANY_NONLAWFUL, ANY_NONCHAOTIC };

    public class Character
    {
        public CLASS Class;
        public Int32 Level;
        public Int32[] StatScores;
        public Int32 StrengthPercentile;
        public ALIGNMENT[] Alignment;
        public Int32[] THACO;
        public String[] Equipment;
        public Races Race;
        public Int32 HP;
        public String Languages;
        public Int32[] RogueAbilities;

        public static String[] AlignmentRandomizerArray { get { return Enum.GetNames(typeof(ALIGNMENT_RANDOMIZER)); } }

        public String StatAbilities
        {
            get
            {
                var output = String.Empty;

                if (StrengthAbilities != String.Empty)
                    output += "\nSTR: " + StrengthAbilities;
                if (DexterityAbilities != String.Empty)
                    output += "\nDEX: " + DexterityAbilities;
                if (ConstitutionAbilities != String.Empty)
                    output += "\nCON: " + ConstitutionAbilities;
                if (IntelligenceAbilities != String.Empty)
                    output += "\nINT: " + IntelligenceAbilities;
                if (WisdomAbilities != String.Empty)
                    output += "\nWIS: " + WisdomAbilities;
                if (CharismaAbilities != String.Empty)
                    output += "\nCHA: " + CharismaAbilities;

                return output;
            }
        }

        public String StrengthAbilities
        {
            get
            {
                switch (StatScores[Stats.Strength])
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
                    default: return String.Empty;
                }
            }
        }

        public String DexterityAbilities
        {
            get
            {
                switch (StatScores[Stats.Dexterity])
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
                    default: return String.Empty;
                }
            }
        }

        public String ConstitutionAbilities
        {
            get
            {
                switch (StatScores[Stats.Constitution])
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
                    default: return String.Empty;
                }
            }
        }

        public String IntelligenceAbilities
        {
            get
            {
                switch (StatScores[Stats.Intelligence])
                {
                    case 19: return "Immune to 1st-level illusions";
                    case 20: return "Immune to up to 2nd-level illusions";
                    case 21: return "Immune to up to 3rd-level illusions";
                    case 22: return "Immune to up to 4th-level illusions";
                    case 23: return "Immune to up to 5th-level illusions";
                    case 24: return "Immune to up to 6th-level illusions";
                    case 25: return "Immune to up to 7th-level illusions";
                    default: return String.Empty;
                }
            }
        }

        public String WisdomAbilities
        {
            get
            {
                switch (StatScores[Stats.Wisdom])
                {
                    case 1:
                        if (IsHealer())
                            return "80% spell failure";
                        return String.Empty;
                    case 2:
                        if (IsHealer())
                            return "60% spell failure";
                        return String.Empty;
                    case 3:
                        if (IsHealer())
                            return "50% spell failure";
                        return String.Empty;
                    case 4:
                        if (IsHealer())
                            return "45% spell failure";
                        return String.Empty;
                    case 5:
                        if (IsHealer())
                            return "40% spell failure";
                        return String.Empty;
                    case 6:
                        if (IsHealer())
                            return "35% spell failure";
                        return String.Empty;
                    case 7:
                        if (IsHealer())
                            return "30% spell failure";
                        return String.Empty;
                    case 8:
                        if (IsHealer())
                            return "25% spell failure";
                        return String.Empty;
                    case 9:
                        if (IsHealer())
                            return "20% spell failure";
                        return String.Empty;
                    case 10:
                        if (IsHealer())
                            return "15% spell failure";
                        return String.Empty;
                    case 11:
                        if (IsHealer())
                            return "10% spell failure";
                        return String.Empty;
                    case 12:
                        if (IsHealer())
                            return "5% spell failure";
                        return String.Empty;
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
                    default: return String.Empty;
                }
            }
        }

        private Boolean IsHealer()
        {
            return Class == CLASS.CLERIC || Class == CLASS.DRUID || Class == CLASS.PALADIN || Class == CLASS.RANGER;
        }

        public String CharismaAbilities
        {
            get
            {
                switch (StatScores[Stats.Charisma])
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
                    default: return String.Empty;
                }
            }
        }

        public Character(ROLLMETHOD rollMethod, Races race, ALIGNMENT[] alignment, CLASS charClass, Int32 level, ref RichTextBox progress)
        {
            Application.DoEvents();
            progress.Text += "\nInitializing NPC object...";
            Class = charClass;
            Level = level;
            Alignment = alignment;
            Race = race;
            progress.Text += "Initialized";

            Application.DoEvents();
            progress.Text += "\nRolling stats...";
            StatScores = StatDice.Roll(rollMethod);
            foreach (var stat in StatScores)
                progress.Text += " " + stat.ToString();

            Application.DoEvents();
            progress.Text += "\nPrioritizing and adjusting stats...";
            StatScores = Classes.Prioritize(charClass, StatScores);
            StrengthPercentile = 0;

            if (IsFighter(charClass))
                if (StatScores[Stats.Strength] > 17 && StatScores[Stats.Strength] < 19)
                    StrengthPercentile = Dice.Percentile();

            StatAdjustment();
            foreach (var stat in StatScores)
                progress.Text += " " + stat.ToString();
            progress.Text += " " + StrengthPercentile.ToString();

            Application.DoEvents();
            progress.Text += "\nDetermining HP...";
            HP = Classes.HitPoints(charClass, level, StatScores[Stats.Constitution], race);
            progress.Text += HP.ToString();

            Application.DoEvents();
            progress.Text += "\nDetermining Languages...";
            Languages = SetLanguages();
            progress.Text += Languages;

            Application.DoEvents();
            progress.Text += "\nDetermining THAC0...";
            SetTHACO();
            progress.Text += THACO[0].ToString() + ", " + THACO[1].ToString();

            Application.DoEvents();
            progress.Text += "\nDetermining gear...";
            //Variables for determining magical abilities of NPC's items
            Equipment = new String[EquipmentType.Types];
            var twoHanded = false;

            //Generate weapon
            Application.DoEvents();
            progress.Text += "\nGenerating weapon...";
            var bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Weapon));
            Equipment[EquipmentType.Weapon] = Weapons.Generate(bonus, charClass, level, true, ref twoHanded);
            if (charClass == CLASS.MONK)
                Equipment[EquipmentType.Weapon] += String.Format("\nUnarmed Strike: 1d{0}", Classes.MonkDamage(level));
            progress.Text += Equipment[EquipmentType.Weapon];

            //Generate armor
            System.Windows.Forms.Application.DoEvents();
            progress.Text += "\nGenerating armor...";
            bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Armor));
            Equipment[EquipmentType.Armor] = Armor.GenerateArmor(bonus, charClass, level);
            progress.Text += Equipment[EquipmentType.Armor];

            //Generate rogue abilities, if applicable
            System.Windows.Forms.Application.DoEvents();
            progress.Text += "\nGenerating rogue abilities...";
            bool[] ArmorBool = new bool[3];
            ArmorBool[0] = (Equipment[EquipmentType.Armor] == String.Empty);
            ArmorBool[1] = (Equipment[EquipmentType.Armor].Contains("chain") || Equipment[EquipmentType.Armor].Contains("mithral"));
            ArmorBool[2] = (Equipment[EquipmentType.Armor].Contains("padded") || Equipment[EquipmentType.Armor].Contains("leather"));
            if (charClass == CLASS.THIEF)
                RogueAbilities = Classes.ThiefAbilities(level, race.Race, StatScores[Stats.Dexterity], ArmorBool);
            else if (charClass == CLASS.BARD)
                RogueAbilities = Classes.BardAbilities(level, race.Race, StatScores[Stats.Dexterity], ArmorBool);
            if (RogueAbilities != null)
                foreach (var abil in RogueAbilities)
                    progress.Text += " " + abil.ToString();

            //Generate shield or dual-wield weapon
            Application.DoEvents();
            progress.Text += "\nGenerating shield or dual-wield weapon...";
            if (!twoHanded)
            {
                bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Shield));
                if (Armor.CanWield(charClass, false) && StatScores[Stats.Dexterity] > 18)
                {
                    if (Dice.Percentile() > 50)
                        Equipment[EquipmentType.Shield] = Weapons.Generate(bonus, charClass, level, false, ref twoHanded);
                    else
                        Equipment[EquipmentType.Shield] = Armor.GenerateShield(bonus, charClass, level);
                }
                else if (StatScores[Stats.Dexterity] > 18)
                    Equipment[EquipmentType.Shield] = Weapons.Generate(bonus, charClass, level, false, ref twoHanded);
                else if (Armor.CanWield(charClass, false))
                    Equipment[EquipmentType.Shield] = Armor.GenerateShield(bonus, charClass, level);
            }
            progress.Text += Equipment[EquipmentType.Shield];

            //Generate bracers
            Application.DoEvents();
            progress.Text += "\nGenerating bracers...";
            bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Bracers));
            if (bonus > 0)
                Equipment[EquipmentType.Bracers] = String.Format("Bracers AC {0}", 10 - 2 * bonus);
            progress.Text += Equipment[EquipmentType.Bracers];

            //Generate rings of protection
            Application.DoEvents();
            progress.Text += "\nGenerating ring of protection...";
            bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.RingOfProtection));
            if (bonus > 0)
                Equipment[EquipmentType.RingOfProtection] = String.Format("Ring of Protection +{0}", bonus);
            progress.Text += Equipment[EquipmentType.RingOfProtection];

            //Generate potions.  Bonus is quantity
            Application.DoEvents();
            progress.Text += "\nGenerating potions...";
            bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Potion));
            Equipment[EquipmentType.Potion] = MagicItems.Potion(MagicItems.PowerByLevel(level), bonus);
            progress.Text += Equipment[EquipmentType.Potion];

            //Generate scrolls.  Bonus is quantity
            Application.DoEvents();
            progress.Text += "\nGenerating scrolls...";
            bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Scroll));
            Equipment[EquipmentType.Scroll] = Scrolls.Generate(charClass, level, bonus);
            progress.Text += Equipment[EquipmentType.Scroll];

            //Generate miscellaneous items.  Bonus is quantity
            Application.DoEvents();
            progress.Text += "\nGenerating miscellaneous items...";
            bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Miscellaneous));
            if (bonus > 0)
                Equipment[EquipmentType.Miscellaneous] = MagicItems.Generate(MagicItems.PowerByLevel(level), bonus);
            Equipment[EquipmentType.Miscellaneous] += "\n" + Treasure.MundaneItems();
            if (Dice.Percentile() > 10)
            {
                Equipment[EquipmentType.Miscellaneous] += "\nPack:";
                Equipment[EquipmentType.Miscellaneous] += String.Format("\n\tRations ({0} Days)", Dice.d8() - 1);
                Equipment[EquipmentType.Miscellaneous] += String.Format("\n\t{0}' rope", Dice.Percentile() / 2);
                Equipment[EquipmentType.Miscellaneous] += String.Format("\n\t{0} torches", Dice.d4() - 1);
                Equipment[EquipmentType.Miscellaneous] += String.Format("\n\tflint & steel");
                Equipment[EquipmentType.Miscellaneous] += String.Format("\n\twhetstone");
                Equipment[EquipmentType.Miscellaneous] += String.Format("\n\t{0} spikes", Dice.d4() - 1);
            }
            progress.Text += Equipment[EquipmentType.Miscellaneous];

            //Generate money
            Application.DoEvents();
            progress.Text += "\nGenerating money, gems, and art objects...";
            Equipment[EquipmentType.Money] = Treasure.Money(level);
            progress.Text += Equipment[EquipmentType.Money];

            //Generate spells
            Application.DoEvents();
            progress.Text += "\nGenerating spells...";
            if (charClass == CLASS.BARD || charClass == CLASS.WIZARD || charClass == CLASS.CLERIC || charClass == CLASS.DRUID || charClass == CLASS.PALADIN || charClass == CLASS.RANGER || charClass == CLASS.SORCERER)
                Equipment[EquipmentType.Spells] = String.Format("{0} spells: {1}\n", charClass.ToString(), Classes.SpellString(charClass, level, StatScores[Stats.Intelligence], StatScores[Stats.Wisdom], StatScores[Stats.Charisma]));
            progress.Text += Equipment[EquipmentType.Spells];

            //Generate familiars
            Application.DoEvents();
            progress.Text += "\nGenerating familiars...";
            Equipment[EquipmentType.Familiars] = Classes.Familiar(charClass, level);
            progress.Text += Equipment[EquipmentType.Familiars];
        }

        private String SetLanguages()
        {
            var NumberOfLanguages = (StatScores[Stats.Intelligence] - 10) / 2;
            String languages;

            switch (Race.Race)
            {
                case RACE.AASIMAR: languages = "Common, Celestial"; break;
                case RACE.GOBLIN:
                case RACE.HOBGOBLIN:
                case RACE.BUGBEAR: languages = "Common, Goblin"; break;
                case RACE.DUERGAR: languages = "Common, Dwarven, Undercommon"; break;
                case RACE.HILL_DWARF:
                case RACE.MOUNTAIN_DWARF:
                case RACE.DEEP_DWARF:
                case RACE.DERRO_DWARF: languages = "Common, Dwarven"; break;
                case RACE.DROW: languages = "Common, Elven, Undercommon"; break;
                case RACE.FOREST_GNOME: languages = "Common, Gnome, Elven, Sylvan"; break;
                case RACE.GNOLL: languages = "Gnoll"; break;
                case RACE.GRAY_ELF:
                case RACE.HIGH_ELF:
                case RACE.WILD_ELF:
                case RACE.WOOD_ELF:
                case RACE.HALFELF: languages = "Common, Elven"; break;
                case RACE.ORC:
                case RACE.HALFORC: languages = "Common, Orc"; break;
                case RACE.LIZARDFOLK: languages = "Common, Draconic"; break;
                case RACE.TROGLODYTE:
                case RACE.KOBOLD: languages = "Draconic"; break;
                case RACE.DEEP_HALFLING:
                case RACE.TALLFELLOW_HALFLING:
                case RACE.LIGHTFOOT_HALFLING: languages = "Common, Halfling"; break;
                case RACE.MIND_FLAYER: languages = "Common, Undercommon"; break;
                case RACE.OGRE:
                case RACE.OGRE_MAGE:
                case RACE.MINOTAUR: languages = "Common, Giant"; break;
                case RACE.ROCK_GNOME: languages = "Common, Gnome"; break;
                case RACE.SVIRFNEBLIN: languages = "Common, Gnome, Undercommon"; break;
                case RACE.TIEFLING: languages = "Common, Infernal"; break;
                default: languages = "Common"; break;
            }

            switch (Race.MetaRace)
            {
                case METARACE.HALF_CELESTIAL:
                    if (!languages.Contains("Celestial"))
                        languages += ", Celestial";
                    break;
                case METARACE.HALF_DRAGON:
                    if (!languages.Contains("Draconic"))
                        languages += ", Draconic";
                    break;
                case METARACE.HALF_FIEND:
                    if (!languages.Contains("Infernal"))
                        languages += ", Infernal";
                    break;
                default: break;
            }

            if (Class == CLASS.DRUID)
                languages += ", Druidic";
            
            while (NumberOfLanguages > 0)
            {
                switch (Dice.d20())
                {
                    case 1:
                        if (!languages.Contains("Abyssal"))
                        {
                            if (Class == CLASS.CLERIC || Race.CanSpeak("Abyssal"))
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
                            if (Class == CLASS.CLERIC || Race.CanSpeak("Celestial"))
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
                            if (Class == CLASS.WIZARD || Race.CanSpeak("Draconic"))
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
                            if (Class == CLASS.CLERIC || Race.CanSpeak("Infernal"))
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
                            if (Class == CLASS.DRUID || Race.CanSpeak("Sylvan"))
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
            THACO = new Int32[2] { 20, 20 };
            switch (Class)
            {
                case CLASS.BARBARIAN:
                case CLASS.FIGHTER:
                case CLASS.MONK:
                case CLASS.PALADIN:
                case CLASS.RANGER:
                    THACO[0] -= (Level - 1);
                    THACO[1] -= (Level - 1);
                    break;
                case CLASS.CLERIC:
                case CLASS.DRUID:
                    THACO[0] -= ((Level - 1) / 3) * 2;
                    THACO[1] -= ((Level - 1) / 3) * 2;
                    break;
                case CLASS.SORCERER:
                case CLASS.WIZARD:
                    THACO[0] -= ((Level - 1) / 3);
                    THACO[1] -= ((Level - 1) / 3);
                    break;
                case CLASS.THIEF:
                case CLASS.BARD:
                    THACO[0] -= ((Level - 1) / 2);
                    THACO[1] -= ((Level - 1) / 2);
                    break;
                default: break;
            }

            switch (StatScores[Stats.Strength])
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

            switch (StatScores[Stats.Dexterity])
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

        private static Int32 BonusByChance(Int32 Chance)
        {
            var bonus = 0;

            while (Chance > 100)
            {
                bonus++;
                Chance -= 100;
            }

            while (Chance > 0)
            {
                var roll = Dice.Percentile();
                if (roll <= Chance)
                    bonus++;
                Chance -= roll;
            }

            return bonus;
        }

        public static ALIGNMENT[] RandomAlignment(ALIGNMENT_RANDOMIZER Randomizer)
        {
            var TempAlignment = new ALIGNMENT[2] { ALIGNMENT.LAWFUL, ALIGNMENT.GOOD };
            
            switch (Randomizer)
            {
                case ALIGNMENT_RANDOMIZER.ANY_CHAOTIC:
                    TempAlignment[0] = ALIGNMENT.CHAOTIC;
                    var roll = Dice.Percentile();
                    if (roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_EVIL:
                    TempAlignment[1] = ALIGNMENT.EVIL;
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_GOOD:
                    TempAlignment[1] = ALIGNMENT.GOOD;
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_LAWFUL:
                    TempAlignment[0] = ALIGNMENT.LAWFUL;
                    roll = Dice.Percentile();
                    if (roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NEUTRAL:
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    if (TempAlignment[0] == ALIGNMENT.NEUTRAL)
                    {
                        roll = Dice.Percentile();
                        if (roll < 21)
                            TempAlignment[1] = ALIGNMENT.GOOD;
                        else if (roll < 51)
                            TempAlignment[1] = ALIGNMENT.NEUTRAL;
                        else
                            TempAlignment[1] = ALIGNMENT.EVIL;
                    }
                    else
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONCHAOTIC:
                    TempAlignment[0] = ALIGNMENT.LAWFUL;
                    if (Dice.Percentile() < 51)
                        TempAlignment[0] = ALIGNMENT.NEUTRAL;
                    roll = Dice.Percentile();
                    if (roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONEVIL:
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    TempAlignment[1] = ALIGNMENT.GOOD;
                    if (Dice.Roll(1, 5, 0) > 2)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONGOOD:
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    TempAlignment[1] = ALIGNMENT.EVIL;
                    if (Dice.Roll(1, 8, 0) < 4)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONLAWFUL:
                    TempAlignment[0] = ALIGNMENT.CHAOTIC;
                    if (Dice.Percentile() < 51)
                        TempAlignment[0] = ALIGNMENT.NEUTRAL;
                    roll = Dice.Percentile();
                    if (roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY_NONNEUTRAL:
                    TempAlignment[0] = ALIGNMENT.LAWFUL;
                    if (Dice.Percentile() < 51)
                        TempAlignment[0] = ALIGNMENT.CHAOTIC;
                    TempAlignment[1] = ALIGNMENT.EVIL;
                    if (Dice.Roll(1, 7, 0) < 3)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    return TempAlignment;
                case ALIGNMENT_RANDOMIZER.ANY:
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: TempAlignment[0] = ALIGNMENT.LAWFUL; break;
                        case 2: TempAlignment[0] = ALIGNMENT.NEUTRAL; break;
                        case 3: TempAlignment[0] = ALIGNMENT.CHAOTIC; break;
                        default: break;
                    }
                    roll = Dice.Percentile();
                    if (roll < 21)
                        TempAlignment[1] = ALIGNMENT.GOOD;
                    else if (roll < 51)
                        TempAlignment[1] = ALIGNMENT.NEUTRAL;
                    else
                        TempAlignment[1] = ALIGNMENT.EVIL;
                    return TempAlignment;
                default: return TempAlignment;
            }
        }

        private static Boolean IsFighter(CLASS charClass)
        {
            switch (charClass)
            {
                case CLASS.BARBARIAN:
                case CLASS.FIGHTER:
                case CLASS.MONK:
                case CLASS.PALADIN:
                case CLASS.RANGER: return true;
                default: return false;
            }
        }

        public static ALIGNMENT[] StringToAlignment(String input)
        {
            var output = new ALIGNMENT[2];

            if (input.StartsWith("Lawful"))
                output[0] = ALIGNMENT.LAWFUL;
            else if (input.StartsWith("Neutral") || input.StartsWith("True"))
                output[0] = ALIGNMENT.NEUTRAL;
            else
                output[0] = ALIGNMENT.CHAOTIC;

            if (input.EndsWith("Good"))
                output[1] = ALIGNMENT.GOOD;
            else if (input.EndsWith("Neutral"))
                output[1] = ALIGNMENT.NEUTRAL;
            else
                output[1] = ALIGNMENT.EVIL;

            return output;
        }

        public void StatAdjustment()
        {
            switch (Race.Race)
            {
                case RACE.HILL_DWARF:
                case RACE.MOUNTAIN_DWARF:
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.HIGH_ELF:
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Constitution]--;
                    break;
                case RACE.ROCK_GNOME:
                case RACE.FOREST_GNOME:
                    StatScores[Stats.Constitution]++;
                    AdjustStrength(-1);
                    break;
                case RACE.DEEP_HALFLING:
                case RACE.LIGHTFOOT_HALFLING:
                case RACE.TALLFELLOW_HALFLING:
                    StatScores[Stats.Dexterity]++;
                    AdjustStrength(-1);
                    break;
                case RACE.HALFORC:
                    AdjustStrength(2);
                    StatScores[Stats.Charisma]--; 
                    StatScores[Stats.Intelligence]--;
                    break;
                case RACE.AASIMAR:
                    StatScores[Stats.Charisma]++; 
                    StatScores[Stats.Wisdom]++;
                    break;
                case RACE.BUGBEAR:
                    AdjustStrength(2); 
                    StatScores[Stats.Dexterity]++; 
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.DERRO_DWARF:
                    StatScores[Stats.Dexterity] += 2; 
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Charisma] -= 2; 
                    AdjustStrength(-1);
                    break;
                case RACE.DOPPELGANGER:
                    AdjustStrength(1); 
                    StatScores[Stats.Dexterity]++; 
                    StatScores[Stats.Constitution]++; 
                    StatScores[Stats.Intelligence]++;
                    StatScores[Stats.Charisma]++; 
                    StatScores[Stats.Wisdom] += 2;
                    break;
                case RACE.DROW:
                    StatScores[Stats.Dexterity]++; 
                    StatScores[Stats.Constitution]++;
                    if (Race.Male)
                        StatScores[Stats.Charisma]--;
                    else
                        StatScores[Stats.Charisma]++;
                    break;
                case RACE.DUERGAR:
                case RACE.DEEP_DWARF:
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Charisma] -= 2;
                    break;
                case RACE.GNOLL:
                    AdjustStrength(2);
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Intelligence]--;
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.GOBLIN:
                    StatScores[Stats.Dexterity]++;
                    AdjustStrength(-1);
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.GRAY_ELF:
                    StatScores[Stats.Intelligence]++;
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Constitution]--; 
                    AdjustStrength(-1); 
                    break;
                case RACE.HOBGOBLIN:
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Constitution]++;
                    break;
                case RACE.KOBOLD:
                    StatScores[Stats.Dexterity]++;
                    AdjustStrength(-2);
                    break;
                case RACE.LIZARDFOLK:
                    AdjustStrength(1);
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Intelligence]--;
                    break;
                case RACE.MIND_FLAYER:
                    AdjustStrength(1); 
                    StatScores[Stats.Dexterity] += 2;
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Intelligence] += 4;
                    StatScores[Stats.Charisma] += 3;
                    StatScores[Stats.Wisdom] += 3;
                    break;
                case RACE.MINOTAUR:
                    AdjustStrength(4); 
                    StatScores[Stats.Constitution] += 2;
                    StatScores[Stats.Intelligence] -= 2;
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.OGRE:
                    AdjustStrength(5);
                    StatScores[Stats.Dexterity]--;
                    StatScores[Stats.Constitution] += 2;
                    StatScores[Stats.Intelligence] -= 2;
                    StatScores[Stats.Charisma] -= 2;
                    break;
                case RACE.OGRE_MAGE:
                    AdjustStrength(5);
                    StatScores[Stats.Constitution] += 3;
                    StatScores[Stats.Intelligence] += 2;
                    StatScores[Stats.Wisdom] += 2;
                    StatScores[Stats.Charisma] += 3;
                    break;
                case RACE.ORC:
                    AdjustStrength(2);
                    StatScores[Stats.Intelligence]--;
                    StatScores[Stats.Wisdom]--;
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.SVIRFNEBLIN:
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Wisdom]++;
                    StatScores[Stats.Charisma] -= 2;
                    break;
                case RACE.TIEFLING:
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Intelligence]++;
                    StatScores[Stats.Charisma]--;
                    break;
                case RACE.TROGLODYTE:
                    StatScores[Stats.Constitution] += 2;
                    StatScores[Stats.Dexterity]--;
                    StatScores[Stats.Intelligence]--;
                    break;
                case RACE.WILD_ELF:
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Intelligence]--;
                    break;
                case RACE.WOOD_ELF:
                    StatScores[Stats.Dexterity]++; 
                    AdjustStrength(1);
                    StatScores[Stats.Constitution]--;
                    StatScores[Stats.Intelligence]--;
                    break;
                default: break;
            }

            switch (Race.MetaRace)
            {
                case METARACE.HALF_CELESTIAL:
                    AdjustStrength(2);
                    StatScores[Stats.Dexterity]++;
                    StatScores[Stats.Constitution] += 2;
                    StatScores[Stats.Intelligence]++;
                    StatScores[Stats.Charisma] += 2;
                    StatScores[Stats.Wisdom] += 2;
                    break;
                case METARACE.HALF_DRAGON:
                    AdjustStrength(4);
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Intelligence]++;
                    StatScores[Stats.Charisma]++;
                    break;
                case METARACE.HALF_FIEND:
                    AdjustStrength(2);
                    StatScores[Stats.Dexterity] += 2;
                    StatScores[Stats.Constitution]++;
                    StatScores[Stats.Intelligence] += 2;
                    StatScores[Stats.Charisma]++;
                    break;
                default: break;
            }
        }

        public void AdjustStrength(Int32 adjustment)
        {
            while (adjustment != 0)
            {
                if (adjustment < 0)
                {
                    if (StatScores[Stats.Strength] != 18 || StrengthPercentile == 0)
                    {
                        StatScores[Stats.Strength]--;
                        if (StatScores[Stats.Strength] == 18)
                            StrengthPercentile = 100;
                        else
                            StrengthPercentile = 0;
                        adjustment++;
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
                        adjustment++;
                    }
                }
                else
                {
                    if (StatScores[Stats.Strength] != 18 || StrengthPercentile == 100)
                    {
                        StatScores[Stats.Strength]++;
                        StrengthPercentile = 0;
                        adjustment--;
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
                        adjustment--;
                    }
                }
            }
        }

        public override string ToString()
        {
            var output = String.Format("{0} {1} Level {2}", Race.ToString(), Class, Level);

            output += "\n" + Alignment[0].ToString() + " " + Alignment[1].ToString();
            output += String.Format(" HP {0}, THAC0: {1} Melee, {2} Missile\n", HP, THACO[0], THACO[1]);
            var strengthString = String.Format("{0}", StatScores[Stats.Strength]);
            if (StrengthPercentile != 0)
                strengthString += String.Format("/{0}", StrengthPercentile);
            output += String.Format("STR {0} CON {1} DEX {2} INT {3} WIS {4} CHA {5}",
                strengthString, StatScores[Stats.Constitution], StatScores[Stats.Dexterity], StatScores[Stats.Intelligence],
                StatScores[Stats.Wisdom], StatScores[Stats.Charisma]);

            if (StatAbilities != String.Empty)
                output += "\n" + StatAbilities;

            output += "\nLANGUAGES: " + Languages;

            if (Race.RacialTraits != String.Empty || Race.MetaRacialTraits != String.Empty)
            {
                if (Race.RacialTraits != String.Empty && Race.MetaRacialTraits != String.Empty)
                    output += String.Format("\n{0}: {1}, {2}", Race.ToString(), Race.RacialTraits, Race.MetaRacialTraits);
                else
                    output += String.Format("\n{0}: {1}{2}", Race.ToString(), Race.RacialTraits, Race.MetaRacialTraits);
            }

            if (Classes.Abilities(Class, Level, StatScores[Stats.Wisdom]) != String.Empty)
                output += "\n" + Class.ToString() + ": " + Classes.Abilities(Class, Level, StatScores[Stats.Wisdom]);

            if (RogueAbilities != null)
            {             
                if (Class == CLASS.BARD)
                {
                    output += String.Format("\nClimb Walls: {0}%", RogueAbilities[0]);
                    output += String.Format("\nDetect Noise: {0}%", RogueAbilities[1]);
                    output += String.Format("\nPick Pockets: {0}%", RogueAbilities[2]);
                    output += String.Format("\nRead Languages: {0}%", RogueAbilities[3]);
                }
                else if (Class == CLASS.THIEF)
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

            output += "\n\n" + Equipment[EquipmentType.Weapon];

            if (Equipment[EquipmentType.Armor] != String.Empty)
                output += "\n" + Equipment[EquipmentType.Armor];
            if (Equipment[EquipmentType.RingOfProtection] != String.Empty)
                output += "\n" + Equipment[EquipmentType.RingOfProtection];
            if (Equipment[EquipmentType.Bracers] != String.Empty)
                output += "\n" + Equipment[EquipmentType.Bracers];
            if (Equipment[EquipmentType.Shield] != String.Empty)
                output += "\n" + Equipment[EquipmentType.Shield];
            if (Equipment[EquipmentType.Potion] != String.Empty)
                output += "\n\n" + Equipment[EquipmentType.Potion];
            if (Equipment[EquipmentType.Scroll] != String.Empty)
                output += "\n\n" + Equipment[EquipmentType.Scroll];
            if (Equipment[EquipmentType.Miscellaneous] != String.Empty)
                output += "\n\n" + Equipment[EquipmentType.Miscellaneous];
            if (Equipment[EquipmentType.Money] != String.Empty)
                output += "\n\n" + Equipment[EquipmentType.Money];
            if (Equipment[EquipmentType.Spells] != String.Empty)
                output += "\n\n" + Equipment[EquipmentType.Spells];
            if (Equipment[EquipmentType.Familiars] != String.Empty)
                output += "\n\n" + Equipment[EquipmentType.Familiars];

            return output;
        }

        private Int32 PercentageChance(CLASS Class, Int32 equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.Armor:
                    switch (Class)
                    {
                        case CLASS.CLERIC:
                        case CLASS.DRUID: return 8;
                        case CLASS.FIGHTER:
                        case CLASS.BARBARIAN:
                        case CLASS.PALADIN:
                        case CLASS.BARD:
                        case CLASS.THIEF: return 10;
                        case CLASS.RANGER: return 15;
                        default: return 0;
                    }
                case EquipmentType.Shield:
                    switch (Class)
                    {
                        case CLASS.CLERIC:
                        case CLASS.FIGHTER:
                        case CLASS.BARBARIAN:
                        case CLASS.PALADIN: return 10;
                        case CLASS.RANGER:
                        case CLASS.BARD: return 8;
                        default: return 0;
                    }
                case EquipmentType.Bracers:
                    switch (Class)
                    {
                        case CLASS.SORCERER:
                        case CLASS.WIZARD:
                        case CLASS.MONK: return 4;
                        default: return 0;
                    }
                case EquipmentType.RingOfProtection:
                    switch (Class)
                    {
                        case CLASS.CLERIC: return 2;
                        case CLASS.DRUID: return 5;
                        case CLASS.SORCERER:
                        case CLASS.MONK:
                        case CLASS.WIZARD: return 15;
                        case CLASS.BARD: return 3;
                        case CLASS.THIEF: return 4;
                        default: return 0;
                    }
                case EquipmentType.Weapon:
                    switch (Class)
                    {
                        case CLASS.CLERIC: return 12;
                        case CLASS.DRUID:
                        case CLASS.FIGHTER:
                        case CLASS.BARBARIAN:
                        case CLASS.PALADIN:
                        case CLASS.BARD:
                        case CLASS.RANGER: return 10;
                        case CLASS.MONK: return 5;
                        case CLASS.SORCERER:
                        case CLASS.WIZARD: return 15;
                        case CLASS.THIEF: return 12;
                        default: return 0;
                    }
                case EquipmentType.Scroll:
                    switch (Class)
                    {
                        case CLASS.CLERIC: return 8;
                        case CLASS.DRUID: return 7;
                        case CLASS.FIGHTER:
                        case CLASS.THIEF:
                        case CLASS.MONK: return 6;
                        case CLASS.PALADIN: return 4;
                        case CLASS.RANGER: return 5;
                        case CLASS.SORCERER: return 12;
                        case CLASS.WIZARD:
                        case CLASS.BARD: return 15;
                        default: return 0;
                    }
                case EquipmentType.Potion:
                    switch (Class)
                    {
                        case CLASS.PALADIN:
                        case CLASS.CLERIC: return 6;
                        case CLASS.DRUID: return 11;
                        case CLASS.FIGHTER: return 8;
                        case CLASS.SORCERER:
                        case CLASS.WIZARD:
                        case CLASS.BARD:
                        case CLASS.MONK: return 10;
                        case CLASS.RANGER: return 7;
                        case CLASS.THIEF: return 9;
                        default: return 0;
                    }
                case EquipmentType.Miscellaneous:
                    switch (Class)
                    {
                        case CLASS.DRUID:
                        case CLASS.PALADIN: return 3;
                        case CLASS.RANGER: return 4;
                        case CLASS.SORCERER: return 8;
                        case CLASS.WIZARD: return 9;
                        case CLASS.BARD: return 6;
                        case CLASS.FIGHTER:
                        case CLASS.CLERIC:
                        case CLASS.MONK:
                        case CLASS.THIEF: return 5;
                        default: return 0;
                    }
                default: return 0;
            }
        }

        public static String RandomAlignment()
        {
            switch (Dice.Roll(1, 9))
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

        public static String RandomGender(Boolean ExtraGenders)
        {
            var limit = 2;
            if (ExtraGenders)
                limit = 4;

            switch (Dice.Roll(1, limit))
            {
                case 1: return "male";
                case 2: return "female";
                case 3: return "both";
                default: return "neither";
            }
        }

        public static String RandomClass()
        {
            switch (Dice.Roll(1, 11))
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

        public static String HumanoidSubtype()
        {
            switch (Dice.Roll(1, 510))
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

        public static String CreatureType()
        {
            switch (Dice.Percentile())
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
                default: return HumanoidSubtype();
            }
        }
    }
}