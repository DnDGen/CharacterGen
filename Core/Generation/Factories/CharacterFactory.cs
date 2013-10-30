using System;
using System.Collections.Generic;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Verifiers;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class CharacterFactory
    {
        public static Character CreateUsing(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer, IDice dice)
        {
            var levelAdjustmentsProvider = ProviderFactory.CreateLevelAdjustmentProvider();
            var randomizerVerifier = new RandomizerVerifier(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer,
                metaraceRandomizer, levelAdjustmentsProvider);

            VerifyRandomizers(randomizerVerifier);

            var character = new Character();

            character.Alignment = GenerateAlignment(randomizerVerifier, alignmentRandomizer);
            var prototype = GenerateCharacterClassPrototype(randomizerVerifier, classNameRandomizer, levelRandomizer, character.Alignment);

            var levelAdjustments = levelAdjustmentsProvider.GetLevelAdjustments();
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, levelAdjustments, character.Alignment, prototype, dice);

            prototype.Level -= levelAdjustments[character.Race.BaseRace];
            prototype.Level -= levelAdjustments[character.Race.Metarace];

            character.Class = CharacterClassFactory.CreateUsing(prototype);

            character.Stats = StatsFactory.CreateUsing(statsRandomizer, character.Class, character.Race, dice);
            character.HitPoints = HitPointsFactory.CreateUsing(dice, character.Class, character.Stats[StatConstants.Constitution].Bonus,
                character.Race);

            var percentileResultProvider = ProviderFactory.CreatePercentileResultProviderUsing(dice);
            character.InterestingTrait = percentileResultProvider.GetPercentileResult("Traits");

            character.Languages = LanguageFactory.CreateUsing(character.Race, character.Class.ClassName, dice, 
                character.Stats[StatConstants.Intelligence].Bonus);

            //******************

            //Application.DoEvents();
            //progress.Text += "\nDetermining Languages...";
            //Languages = SetLanguages();
            //progress.Text += Languages;

            //Application.DoEvents();
            //progress.Text += "\nDetermining THAC0...";
            //SetTHACO();
            //progress.Text += THACO[0].ToString() + ", " + THACO[1].ToString();

            //Application.DoEvents();
            //progress.Text += "\nDetermining gear...";
            ////Variables for determining magical abilities of NPC's items
            //Equipment = new String[EquipmentType.Types];
            //var twoHanded = false;

            ////Generate weapon
            //Application.DoEvents();
            //progress.Text += "\nGenerating weapon...";
            //var bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Weapon));
            //Equipment[EquipmentType.Weapon] = Weapons.Generate(bonus, charClass, level, true, ref twoHanded);
            //if (charClass == CLASS.MONK)
            //    Equipment[EquipmentType.Weapon] += String.Format("\nUnarmed Strike: 1d{0}", Classes.MonkDamage(level));
            //progress.Text += Equipment[EquipmentType.Weapon];

            ////Generate armor
            //System.Windows.Forms.Application.DoEvents();
            //progress.Text += "\nGenerating armor...";
            //bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Armor));
            //Equipment[EquipmentType.Armor] = Armor.GenerateArmor(bonus, charClass, level);
            //progress.Text += Equipment[EquipmentType.Armor];

            ////Generate rogue abilities, if applicable
            //System.Windows.Forms.Application.DoEvents();
            //progress.Text += "\nGenerating rogue abilities...";
            //bool[] ArmorBool = new bool[3];
            //ArmorBool[0] = (Equipment[EquipmentType.Armor] == String.Empty);
            //ArmorBool[1] = (Equipment[EquipmentType.Armor].Contains("chain") || Equipment[EquipmentType.Armor].Contains("mithral"));
            //ArmorBool[2] = (Equipment[EquipmentType.Armor].Contains("padded") || Equipment[EquipmentType.Armor].Contains("leather"));
            //if (charClass == CLASS.THIEF)
            //    RogueAbilities = Classes.ThiefAbilities(level, race.Race, StatScores[Stats.Dexterity], ArmorBool);
            //else if (charClass == CLASS.BARD)
            //    RogueAbilities = Classes.BardAbilities(level, race.Race, StatScores[Stats.Dexterity], ArmorBool);
            //if (RogueAbilities != null)
            //    foreach (var abil in RogueAbilities)
            //        progress.Text += " " + abil.ToString();

            ////Generate shield or dual-wield weapon
            //Application.DoEvents();
            //progress.Text += "\nGenerating shield or dual-wield weapon...";
            //if (!twoHanded)
            //{
            //    bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Shield));
            //    if (Armor.CanWield(charClass, false) && StatScores[Stats.Dexterity] > 18)
            //    {
            //        if (Dice.Percentile() > 50)
            //            Equipment[EquipmentType.Shield] = Weapons.Generate(bonus, charClass, level, false, ref twoHanded);
            //        else
            //            Equipment[EquipmentType.Shield] = Armor.GenerateShield(bonus, charClass, level);
            //    }
            //    else if (StatScores[Stats.Dexterity] > 18)
            //        Equipment[EquipmentType.Shield] = Weapons.Generate(bonus, charClass, level, false, ref twoHanded);
            //    else if (Armor.CanWield(charClass, false))
            //        Equipment[EquipmentType.Shield] = Armor.GenerateShield(bonus, charClass, level);
            //}
            //progress.Text += Equipment[EquipmentType.Shield];

            ////Generate bracers
            //Application.DoEvents();
            //progress.Text += "\nGenerating bracers...";
            //bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Bracers));
            //if (bonus > 0)
            //    Equipment[EquipmentType.Bracers] = String.Format("Bracers AC {0}", 10 - 2 * bonus);
            //progress.Text += Equipment[EquipmentType.Bracers];

            ////Generate rings of protection
            //Application.DoEvents();
            //progress.Text += "\nGenerating ring of protection...";
            //bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.RingOfProtection));
            //if (bonus > 0)
            //    Equipment[EquipmentType.RingOfProtection] = String.Format("Ring of Protection +{0}", bonus);
            //progress.Text += Equipment[EquipmentType.RingOfProtection];

            ////Generate potions.  Bonus is quantity
            //Application.DoEvents();
            //progress.Text += "\nGenerating potions...";
            //bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Potion));
            //Equipment[EquipmentType.Potion] = MagicItems.Potion(MagicItems.PowerByLevel(level), bonus);
            //progress.Text += Equipment[EquipmentType.Potion];

            ////Generate scrolls.  Bonus is quantity
            //Application.DoEvents();
            //progress.Text += "\nGenerating scrolls...";
            //bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Scroll));
            //Equipment[EquipmentType.Scroll] = Scrolls.Generate(charClass, level, bonus);
            //progress.Text += Equipment[EquipmentType.Scroll];

            ////Generate miscellaneous items.  Bonus is quantity
            //Application.DoEvents();
            //progress.Text += "\nGenerating miscellaneous items...";
            //bonus = BonusByChance(level * PercentageChance(charClass, EquipmentType.Miscellaneous));
            //if (bonus > 0)
            //    Equipment[EquipmentType.Miscellaneous] = MagicItems.Generate(MagicItems.PowerByLevel(level), bonus);
            //Equipment[EquipmentType.Miscellaneous] += "\n" + Treasure.MundaneItems();
            //if (Dice.Percentile() > 10)
            //{
            //    Equipment[EquipmentType.Miscellaneous] += "\nPack:";
            //    Equipment[EquipmentType.Miscellaneous] += String.Format("\n\tRations ({0} Days)", Dice.d8() - 1);
            //    Equipment[EquipmentType.Miscellaneous] += String.Format("\n\t{0}' rope", Dice.Percentile() / 2);
            //    Equipment[EquipmentType.Miscellaneous] += String.Format("\n\t{0} torches", Dice.d4() - 1);
            //    Equipment[EquipmentType.Miscellaneous] += String.Format("\n\tflint & steel");
            //    Equipment[EquipmentType.Miscellaneous] += String.Format("\n\twhetstone");
            //    Equipment[EquipmentType.Miscellaneous] += String.Format("\n\t{0} spikes", Dice.d4() - 1);
            //}
            //progress.Text += Equipment[EquipmentType.Miscellaneous];

            ////Generate money
            //Application.DoEvents();
            //progress.Text += "\nGenerating money, gems, and art objects...";
            //Equipment[EquipmentType.Money] = Treasure.Money(level);
            //progress.Text += Equipment[EquipmentType.Money];

            ////Generate spells
            //Application.DoEvents();
            //progress.Text += "\nGenerating spells...";
            //if (charClass == CLASS.BARD || charClass == CLASS.WIZARD || charClass == CLASS.CLERIC || charClass == CLASS.DRUID || charClass == CLASS.PALADIN || charClass == CLASS.RANGER || charClass == CLASS.SORCERER)
            //    Equipment[EquipmentType.Spells] = String.Format("{0} spells: {1}\n", charClass.ToString(), Classes.SpellString(charClass, level, StatScores[Stats.Intelligence], StatScores[Stats.Wisdom], StatScores[Stats.Charisma]));
            //progress.Text += Equipment[EquipmentType.Spells];

            ////Generate familiars
            //Application.DoEvents();
            //progress.Text += "\nGenerating familiars...";
            //Equipment[EquipmentType.Familiars] = Classes.Familiar(charClass, level);
            //progress.Text += Equipment[EquipmentType.Familiars];

            return character;
        }

        private static void VerifyRandomizers(IRandomizerVerifier randomizerVerifier)
        {
            var verified = randomizerVerifier.VerifyCompatibility();

            if (!verified)
                throw new IncompatibleRandomizersException();
        }

        private static Alignment GenerateAlignment(IRandomizerVerifier randomizerVerifier, IAlignmentRandomizer alignmentRandomizer)
        {
            Alignment alignment;

            do alignment = AlignmentFactory.CreateUsing(alignmentRandomizer);
            while (!randomizerVerifier.VerifyAlignmentCompatibility(alignment));

            return alignment;
        }

        private static CharacterClassPrototype GenerateCharacterClassPrototype(IRandomizerVerifier randomizerVerifier, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, Alignment alignment)
        {
            CharacterClassPrototype prototype;

            do prototype = CharacterClassFactory.CreatePrototypeUsing(alignment, levelRandomizer, classNameRandomizer);
            while (!randomizerVerifier.VerifyCharacterClassCompatibility(alignment.Goodness, prototype));

            return prototype;
        }

        private static Race GenerateRace(IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            Dictionary<String, Int32> levelAdjustments, Alignment alignment, CharacterClassPrototype prototype, IDice dice)
        {
            Race race;

            do race = RaceFactory.CreateUsing(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer, dice);
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= prototype.Level);

            return race;
        }
    }
}