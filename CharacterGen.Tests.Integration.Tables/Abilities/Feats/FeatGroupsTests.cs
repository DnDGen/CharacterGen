using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Tables;
using CharacterGen.Magics;
using CharacterGen.Races;
using NUnit.Framework;
using System;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class FeatGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.FeatGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.Additional,
                GroupConstants.FighterBonusFeats,
                GroupConstants.HasClassRequirements,
                GroupConstants.HasSkillRequirements,
                GroupConstants.HasStatRequirements,
                ItemTypeConstants.Weapon + GroupConstants.Proficiency,
                ItemTypeConstants.Armor + GroupConstants.Proficiency,
                FeatConstants.SkillBonus,
                GroupConstants.TakenMultipleTimes,
                GroupConstants.TwoHanded,
                GroupConstants.WizardBonusFeats,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard,
                CharacterClassConstants.Domains.Air,
                CharacterClassConstants.Domains.Animal,
                CharacterClassConstants.Domains.Chaos,
                CharacterClassConstants.Domains.Death,
                CharacterClassConstants.Domains.Destruction,
                CharacterClassConstants.Domains.Earth,
                CharacterClassConstants.Domains.Evil,
                CharacterClassConstants.Domains.Fire,
                CharacterClassConstants.Domains.Good,
                CharacterClassConstants.Domains.Healing,
                CharacterClassConstants.Domains.Knowledge,
                CharacterClassConstants.Domains.Law,
                CharacterClassConstants.Domains.Luck,
                CharacterClassConstants.Domains.Magic,
                CharacterClassConstants.Domains.Plant,
                CharacterClassConstants.Domains.Protection,
                CharacterClassConstants.Domains.Strength,
                CharacterClassConstants.Domains.Sun,
                CharacterClassConstants.Domains.Travel,
                CharacterClassConstants.Domains.Trickery,
                CharacterClassConstants.Domains.War,
                CharacterClassConstants.Domains.Water,
                CharacterClassConstants.Schools.Conjuration,
                CharacterClassConstants.Schools.Evocation,
                CharacterClassConstants.Schools.Enchantment,
                CharacterClassConstants.Schools.Abjuration,
                CharacterClassConstants.Schools.Divination,
                CharacterClassConstants.Schools.Illusion,
                CharacterClassConstants.Schools.Necromancy,
                CharacterClassConstants.Schools.Transmutation,
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.Metaraces.None,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Werewolf,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WoodElf,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Species.Black,
                RaceConstants.Metaraces.Species.Blue,
                RaceConstants.Metaraces.Species.Brass,
                RaceConstants.Metaraces.Species.Bronze,
                RaceConstants.Metaraces.Species.Copper,
                RaceConstants.Metaraces.Species.Gold,
                RaceConstants.Metaraces.Species.Green,
                RaceConstants.Metaraces.Species.Red,
                RaceConstants.Metaraces.Species.Silver,
                RaceConstants.Metaraces.Species.White,
                SavingThrowConstants.Fortitude,
                SavingThrowConstants.Reflex,
                SavingThrowConstants.Will,
                GroupConstants.SavingThrows,
                GroupConstants.Initiative,
                AttributeConstants.Shield + GroupConstants.Proficiency,
                GroupConstants.AddMonsterHitDiceToPower,
                FeatConstants.AttackBonus,
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Warrior
            };

            AssertCollectionNames(names);
        }

        [TestCase("")]
        [TestCase(FeatConstants.AttackBonus,
            FeatConstants.AttackBonus)]
        [TestCase(GroupConstants.AddMonsterHitDiceToPower,
            FeatConstants.SpellResistance)]
        [TestCase(GroupConstants.HasSkillRequirements,
            FeatConstants.MountedArchery,
            FeatConstants.MountedCombat,
            FeatConstants.RideByAttack,
            FeatConstants.SpiritedCharge,
            FeatConstants.Trample,
            FeatConstants.Acrobatic,
            FeatConstants.Agile,
            FeatConstants.Alertness,
            FeatConstants.AnimalAffinity,
            FeatConstants.Athletic,
            FeatConstants.Deceitful,
            FeatConstants.DeftHands,
            FeatConstants.Diligent,
            FeatConstants.Investigator,
            FeatConstants.MagicalAptitude,
            FeatConstants.NatureSense,
            FeatConstants.Negotiator,
            FeatConstants.NimbleFingers,
            FeatConstants.Persuasive,
            FeatConstants.SelfSufficient,
            FeatConstants.Stealthy)]
        [TestCase(GroupConstants.HasStatRequirements,
            FeatConstants.PowerAttack,
            FeatConstants.CombatExpertise,
            FeatConstants.DeflectArrows,
            FeatConstants.Dodge,
            FeatConstants.GreaterTwoWeaponFighting,
            FeatConstants.ImprovedGrapple,
            FeatConstants.ImprovedPreciseShot,
            FeatConstants.ImprovedTwoWeaponFighting,
            FeatConstants.Manyshot,
            FeatConstants.Mobility,
            FeatConstants.NaturalSpell,
            FeatConstants.RapidShot,
            FeatConstants.SnatchArrows,
            FeatConstants.SpellMastery,
            FeatConstants.SpringAttack,
            FeatConstants.StunningFist,
            FeatConstants.ShotOnTheRun,
            FeatConstants.TwoWeaponDefense,
            FeatConstants.TwoWeaponFighting,
            FeatConstants.WhirlwindAttack)]
        [TestCase(GroupConstants.TakenMultipleTimes,
            FeatConstants.AttackBonus,
            FeatConstants.SpellMastery,
            FeatConstants.Toughness,
            FeatConstants.SkillMastery,
            FeatConstants.SkillBonus,
            FeatConstants.DodgeBonus,
            FeatConstants.SaveBonus,
            FeatConstants.ExtraTurning)]
        [TestCase(FeatConstants.SkillBonus,
            FeatConstants.SkillBonus,
            FeatConstants.Acrobatic,
            FeatConstants.Agile,
            FeatConstants.Alertness,
            FeatConstants.AnimalAffinity,
            FeatConstants.Athletic,
            FeatConstants.Deceitful,
            FeatConstants.DeftHands,
            FeatConstants.Diligent,
            FeatConstants.Investigator,
            FeatConstants.MagicalAptitude,
            FeatConstants.Negotiator,
            FeatConstants.NimbleFingers,
            FeatConstants.Persuasive,
            FeatConstants.SelfSufficient,
            FeatConstants.SkillFocus,
            FeatConstants.Stealthy,
            FeatConstants.NatureSense)]
        [TestCase(GroupConstants.WizardBonusFeats,
            FeatConstants.SpellMastery,
            FeatConstants.ScribeScroll,
            FeatConstants.EmpowerSpell,
            FeatConstants.EnlargeSpell,
            FeatConstants.ExtendSpell,
            FeatConstants.HeightenSpell,
            FeatConstants.MaximizeSpell,
            FeatConstants.QuickenSpell,
            FeatConstants.SilentSpell,
            FeatConstants.StillSpell,
            FeatConstants.WidenSpell)]
        [TestCase(ItemTypeConstants.Weapon + GroupConstants.Proficiency,
            FeatConstants.ExoticWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency)]
        [TestCase(ItemTypeConstants.Armor + GroupConstants.Proficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency)]
        [TestCase(GroupConstants.TwoHanded,
            FeatConstants.TwoWeaponDefense,
            FeatConstants.TwoWeaponFighting,
            FeatConstants.GreaterTwoWeaponFighting,
            FeatConstants.ImprovedTwoWeaponFighting)]
        [TestCase(SavingThrowConstants.Fortitude,
            FeatConstants.GreatFortitude)]
        [TestCase(SavingThrowConstants.Reflex,
            FeatConstants.LightningReflexes)]
        [TestCase(SavingThrowConstants.Will,
            FeatConstants.IronWill)]
        [TestCase(GroupConstants.SavingThrows,
            FeatConstants.SaveBonus)]
        [TestCase(GroupConstants.Initiative,
            FeatConstants.ImprovedInitiative)]
        [TestCase(AttributeConstants.Shield + GroupConstants.Proficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.TowerShieldProficiency)]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void FeatsWithClassRequirements()
        {
            var featNames = new[]
            {
                FeatConstants.CombatCasting,
                FeatConstants.CripplingStrike,
                FeatConstants.DefensiveRoll,
                FeatConstants.GreaterWeaponFocus,
                FeatConstants.GreaterWeaponSpecialization,
                FeatConstants.ImprovedEvasion,
                FeatConstants.ImprovedFamiliar,
                FeatConstants.Leadership,
                FeatConstants.Opportunist,
                FeatConstants.SkillMastery,
                FeatConstants.SlipperyMind,
                FeatConstants.SpellMastery,
                FeatConstants.WeaponSpecialization,
                FeatConstants.EmpowerSpell,
                FeatConstants.EnlargeSpell,
                FeatConstants.EschewMaterials,
                FeatConstants.ExtendSpell,
                FeatConstants.HeightenSpell,
                FeatConstants.ImprovedCounterspell,
                FeatConstants.MaximizeSpell,
                FeatConstants.QuickenSpell,
                FeatConstants.ScribeScroll,
                FeatConstants.SilentSpell,
                FeatConstants.SpellFocus,
                FeatConstants.StillSpell,
                FeatConstants.WidenSpell,
                FeatConstants.SpellPenetration
            };

            base.DistinctCollection(GroupConstants.HasClassRequirements, featNames);
        }

        [Test]
        public void FighterBonusFeats()
        {
            var featNames = new[]
            {
                FeatConstants.BlindFight,
                FeatConstants.CombatExpertise,
                FeatConstants.ImprovedDisarm,
                FeatConstants.ImprovedFeint,
                FeatConstants.ImprovedTrip,
                FeatConstants.WhirlwindAttack,
                FeatConstants.CombatReflexes,
                FeatConstants.Dodge,
                FeatConstants.Mobility,
                FeatConstants.SpringAttack,
                FeatConstants.ExoticWeaponProficiency,
                FeatConstants.ImprovedCritical,
                FeatConstants.ImprovedInitiative,
                FeatConstants.ImprovedShieldBash,
                FeatConstants.ImprovedUnarmedStrike,
                FeatConstants.DeflectArrows,
                FeatConstants.ImprovedGrapple,
                FeatConstants.SnatchArrows,
                FeatConstants.StunningFist,
                FeatConstants.MountedCombat,
                FeatConstants.MountedArchery,
                FeatConstants.RideByAttack,
                FeatConstants.SpiritedCharge,
                FeatConstants.Trample,
                FeatConstants.PointBlankShot,
                FeatConstants.FarShot,
                FeatConstants.PreciseShot,
                FeatConstants.RapidShot,
                FeatConstants.Manyshot,
                FeatConstants.ShotOnTheRun,
                FeatConstants.ImprovedPreciseShot,
                FeatConstants.PowerAttack,
                FeatConstants.Cleave,
                FeatConstants.GreatCleave,
                FeatConstants.ImprovedBullRush,
                FeatConstants.ImprovedOverrun,
                FeatConstants.ImprovedSunder,
                FeatConstants.QuickDraw,
                FeatConstants.RapidReload,
                FeatConstants.TwoWeaponFighting,
                FeatConstants.TwoWeaponDefense,
                FeatConstants.ImprovedTwoWeaponFighting,
                FeatConstants.GreaterTwoWeaponFighting,
                FeatConstants.WeaponFinesse,
                FeatConstants.WeaponFocus,
                FeatConstants.WeaponSpecialization,
                FeatConstants.GreaterWeaponFocus,
                FeatConstants.GreaterWeaponSpecialization
            };

            base.DistinctCollection(GroupConstants.FighterBonusFeats, featNames);
        }

        [TestCase(CharacterClassConstants.Cleric,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Fighter,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.TowerShieldProficiency)]
        [TestCase(CharacterClassConstants.Sorcerer,
            FeatConstants.SimpleWeaponProficiency)]
        [TestCase(CharacterClassConstants.Wizard,
            FeatConstants.ScribeScroll,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff)]
        [TestCase(CharacterClassConstants.Domains.Air,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Animal,
            FeatConstants.SpellLikeAbility)]
        [TestCase(CharacterClassConstants.Domains.Chaos,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Death,
            FeatConstants.DeathTouch)]
        [TestCase(CharacterClassConstants.Domains.Destruction,
            FeatConstants.Smite)]
        [TestCase(CharacterClassConstants.Domains.Earth,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Evil,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Fire,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Good,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Healing,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Knowledge,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Law,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Luck,
            FeatConstants.GoodFortune)]
        [TestCase(CharacterClassConstants.Domains.Magic,
            FeatConstants.UseMagicDeviceAsWizard)]
        [TestCase(CharacterClassConstants.Domains.Plant,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Protection,
            FeatConstants.SpellLikeAbility)]
        [TestCase(CharacterClassConstants.Domains.Strength,
            FeatConstants.SupernaturalStrength)]
        [TestCase(CharacterClassConstants.Domains.Sun,
            FeatConstants.GreaterTurning)]
        [TestCase(CharacterClassConstants.Domains.Travel,
            FeatConstants.SpellLikeAbility)]
        [TestCase(CharacterClassConstants.Domains.Trickery)]
        [TestCase(CharacterClassConstants.Domains.War,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.WeaponFocus)]
        [TestCase(CharacterClassConstants.Domains.Water,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Schools.Abjuration,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Conjuration,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Divination,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Enchantment,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Evocation,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Illusion,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Necromancy,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Transmutation,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Adept,
            FeatConstants.SimpleWeaponProficiency)]
        [TestCase(CharacterClassConstants.Aristocrat,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.TowerShieldProficiency)]
        [TestCase(CharacterClassConstants.Commoner,
            FeatConstants.SimpleWeaponProficiency)]
        [TestCase(CharacterClassConstants.Expert,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.LightArmorProficiency)]
        [TestCase(CharacterClassConstants.Warrior,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.TowerShieldProficiency)]
        public void ClassFeatGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar,
            FeatConstants.Darkvision,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SpellLikeAbility,
            FeatConstants.Resistance + FeatConstants.Foci.Acid,
            FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance + FeatConstants.Foci.Electricity)]
        [TestCase(RaceConstants.BaseRaces.Bugbear,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.NaturalArmor,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.Darkvision,
            FeatConstants.Scent)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
            FeatConstants.Stability,
            FeatConstants.SaveBonus + "Poison",
            FeatConstants.SaveBonus + "Spell",
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Appraise,
            FeatConstants.LightSensitivity)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling,
            FeatConstants.SaveBonus + "All",
            FeatConstants.SaveBonus + "Fear",
            FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Appraise,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning)]
        [TestCase(RaceConstants.BaseRaces.Derro,
            FeatConstants.Madness,
            FeatConstants.PoisonUse,
            FeatConstants.SneakAttack,
            FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility + SpellConstants.Daze,
            FeatConstants.SpellLikeAbility + SpellConstants.SoundBurst,
            FeatConstants.VulnerabilityToSunlight,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger,
            FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus + SkillConstants.Disguise,
            FeatConstants.SkillBonus + SkillConstants.Disguise + FeatConstants.ChangeShape,
            FeatConstants.SkillBonus + SkillConstants.Bluff + SpellConstants.DetectThoughts,
            FeatConstants.SkillBonus + SkillConstants.Disguise + SpellConstants.DetectThoughts,
            FeatConstants.Darkvision,
            FeatConstants.NaturalArmor,
            FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
            FeatConstants.ChangeShape,
            FeatConstants.ImmuneToEffect + "Sleep",
            FeatConstants.ImmuneToEffect + "Charm")]
        [TestCase(RaceConstants.BaseRaces.Drow,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.Darkvision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.HandCrossbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SpellResistance,
            FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility + SpellConstants.FaerieFire,
            FeatConstants.LightBlindness,
            FeatConstants.Poison)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning,
            FeatConstants.Stability,
            FeatConstants.ImmuneToEffect + "Paralysis",
            FeatConstants.ImmuneToEffect + "Phantasms",
            FeatConstants.ImmuneToEffect + "Poison",
            FeatConstants.SaveBonus + "Spell",
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Appraise,
            FeatConstants.SpellLikeAbility + SpellConstants.EnlargePerson,
            FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
            FeatConstants.LightSensitivity,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome,
            FeatConstants.LowLightVision,
            FeatConstants.WeaponFamiliarity,
            FeatConstants.SaveBonus,
            FeatConstants.ImprovedSpell,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Lizardfolk,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
            FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
            FeatConstants.SpellLikeAbility + SpellConstants.PassWithoutTrace,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.Hide + "Woods")]
        [TestCase(RaceConstants.BaseRaces.Gnoll,
            FeatConstants.Darkvision,
            FeatConstants.NaturalArmor)]
        [TestCase(RaceConstants.BaseRaces.Goblin,
            FeatConstants.Darkvision,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus + SkillConstants.Ride)]
        [TestCase(RaceConstants.BaseRaces.GrayElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen)]
        [TestCase(RaceConstants.Metaraces.HalfDragon,
            FeatConstants.NaturalArmor,
            FeatConstants.Darkvision,
            FeatConstants.LowLightVision,
            FeatConstants.ImmuneToEffect + "Sleep",
            FeatConstants.ImmuneToEffect + "Paralysis",
            FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon + "Bite")]
        [TestCase(RaceConstants.BaseRaces.HalfElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.ElvenBlood,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Diplomacy,
            FeatConstants.SkillBonus + SkillConstants.GatherInformation)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc,
            FeatConstants.Darkvision,
            FeatConstants.OrcBlood)]
        [TestCase(RaceConstants.BaseRaces.HighElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
            FeatConstants.Stability,
            FeatConstants.SaveBonus + "Poison",
            FeatConstants.SaveBonus + "Spell",
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Appraise)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin,
            FeatConstants.Darkvision,
            FeatConstants.SkillBonus)]
        [TestCase(RaceConstants.BaseRaces.Human)]
        [TestCase(RaceConstants.BaseRaces.Kobold,
            FeatConstants.Darkvision,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.NaturalArmor,
            FeatConstants.LightSensitivity)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling,
            FeatConstants.SaveBonus + "All",
            FeatConstants.SaveBonus + "Fear",
            FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Climb,
            FeatConstants.SkillBonus + SkillConstants.Jump,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.NaturalArmor,
            FeatConstants.HoldBreath,
            FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon + "Bite")]
        [TestCase(RaceConstants.BaseRaces.Minotaur,
            FeatConstants.Darkvision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Greataxe,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.NaturalArmor,
            FeatConstants.NaturalWeapon,
            FeatConstants.PowerfulCharge,
            FeatConstants.NaturalCunning,
            FeatConstants.Scent,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus + SavingThrowConstants.Will)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
            FeatConstants.Stability,
            FeatConstants.SaveBonus + "Poison",
            FeatConstants.SaveBonus + "Spell",
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Appraise)]
        [TestCase(RaceConstants.Metaraces.None)]
        [TestCase(RaceConstants.BaseRaces.Ogre,
            FeatConstants.Darkvision,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.NaturalArmor)]
        [TestCase(RaceConstants.BaseRaces.OgreMage,
            FeatConstants.Darkvision,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.NaturalArmor,
            FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
            FeatConstants.SpellLikeAbility + SpellConstants.CharmPerson,
            FeatConstants.SpellLikeAbility + SpellConstants.ConeOfCold,
            FeatConstants.SpellLikeAbility + SpellConstants.GaseousForm,
            FeatConstants.SpellLikeAbility + SpellConstants.Sleep,
            FeatConstants.Flight,
            FeatConstants.ChangeShape,
            FeatConstants.Regeneration,
            FeatConstants.SpellResistance)]
        [TestCase(RaceConstants.BaseRaces.Orc,
            FeatConstants.Darkvision,
            FeatConstants.LightSensitivity)]
        [TestCase(RaceConstants.BaseRaces.RockGnome,
            FeatConstants.LowLightVision,
            FeatConstants.WeaponFamiliarity,
            FeatConstants.SaveBonus,
            FeatConstants.ImprovedSpell,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus,
            FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
            FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin,
            FeatConstants.LowLightVision,
            FeatConstants.Darkvision,
            FeatConstants.WeaponFamiliarity,
            FeatConstants.SaveBonus,
            FeatConstants.ImprovedSpell,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SpellLikeAbility + SpellConstants.BlindnessDeafness,
            FeatConstants.SpellLikeAbility + SpellConstants.Blur,
            FeatConstants.SpellLikeAbility + SpellConstants.DisguiseSelf,
            FeatConstants.Stonecunning,
            FeatConstants.SpellResistance,
            FeatConstants.SpellLikeAbility + SpellConstants.Nondetection,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.Hide + "Underground")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling,
            FeatConstants.SaveBonus + "All",
            FeatConstants.SaveBonus + "Fear",
            FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.PassiveSecretDoorSearch)]
        [TestCase(RaceConstants.BaseRaces.Tiefling,
            FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.Darkvision,
            FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance + FeatConstants.Foci.Fire)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte,
            FeatConstants.Darkvision,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.Hide + "Underground",
            FeatConstants.NaturalArmor,
            FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon + "Bite",
            FeatConstants.Stench)]
        [TestCase(RaceConstants.Metaraces.Werebear,
            FeatConstants.AlternateForm,
            FeatConstants.Empathy,
            FeatConstants.Lycanthropy,
            FeatConstants.ImprovedGrab,
            FeatConstants.SkillBonus + SkillConstants.Swim,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.NaturalArmor,
            FeatConstants.IronWill,
            FeatConstants.Track,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.LowLightVision,
            FeatConstants.Scent)]
        [TestCase(RaceConstants.Metaraces.Wereboar,
            FeatConstants.AlternateForm,
            FeatConstants.Empathy,
            FeatConstants.Lycanthropy,
            FeatConstants.Ferocity,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.NaturalArmor,
            FeatConstants.LowLightVision,
            FeatConstants.Scent)]
        [TestCase(RaceConstants.Metaraces.Wererat,
            FeatConstants.AlternateForm,
            FeatConstants.Empathy,
            FeatConstants.Lycanthropy,
            FeatConstants.Disease,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SaveBonus,
            FeatConstants.Alertness,
            FeatConstants.IronWill,
            FeatConstants.NaturalArmor,
            FeatConstants.WeaponFinesse,
            FeatConstants.LowLightVision,
            FeatConstants.Scent)]
        [TestCase(RaceConstants.Metaraces.Weretiger,
            FeatConstants.AlternateForm,
            FeatConstants.Empathy,
            FeatConstants.Lycanthropy,
            FeatConstants.ImprovedGrab,
            FeatConstants.Pounce,
            FeatConstants.Rake,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus + SkillConstants.Balance,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.Hide + "Grass",
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.Alertness,
            FeatConstants.IronWill,
            FeatConstants.LowLightVision,
            FeatConstants.Scent,
            FeatConstants.NaturalArmor)]
        [TestCase(RaceConstants.Metaraces.Werewolf,
            FeatConstants.AlternateForm,
            FeatConstants.Empathy,
            FeatConstants.Lycanthropy,
            FeatConstants.IronWill,
            FeatConstants.Track,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.NaturalArmor,
            FeatConstants.Trip,
            FeatConstants.LowLightVision,
            FeatConstants.Scent,
            FeatConstants.SkillBonus + SkillConstants.Survival)]
        [TestCase(RaceConstants.Metaraces.Ghost,
            FeatConstants.CorruptingGaze,
            FeatConstants.CorruptingTouch,
            FeatConstants.DrainingTouch,
            FeatConstants.FrightfulMoan,
            FeatConstants.HorrificAppearance,
            FeatConstants.Malevolence,
            FeatConstants.Manifestation,
            FeatConstants.Telekinesis,
            FeatConstants.Rejuvenation,
            FeatConstants.TurnResistance,
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot)]
        [TestCase(RaceConstants.Metaraces.Lich,
            FeatConstants.ArmorBonus,
            FeatConstants.FearAura,
            FeatConstants.ParalyzingTouch,
            FeatConstants.TurnResistance,
            FeatConstants.DamageReduction,
            FeatConstants.ImmuneToEffect + FeatConstants.Foci.Cold,
            FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
            FeatConstants.ImmuneToEffect + SpellConstants.Polymorph,
            FeatConstants.ImmuneToEffect + "Mind",
            FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.SenseMotive,
            FeatConstants.SkillBonus + SkillConstants.Spot)]
        [TestCase(RaceConstants.BaseRaces.WildElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen)]
        [TestCase(RaceConstants.BaseRaces.WoodElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen)]
        [TestCase(RaceConstants.Metaraces.Species.Black,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Blue,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Brass,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Bronze,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Copper,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Gold,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Green,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Red,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.Silver,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        [TestCase(RaceConstants.Metaraces.Species.White,
            FeatConstants.NaturalWeapon,
            FeatConstants.ImmuneToEffect)]
        public void RaceFeatGroup(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void VampireFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.ArmorBonus,
                FeatConstants.Slam + RaceConstants.Sizes.Large,
                FeatConstants.Slam + RaceConstants.Sizes.Medium,
                FeatConstants.Slam + RaceConstants.Sizes.Small,
                FeatConstants.BloodDrain,
                FeatConstants.ChildrenOfTheNight,
                FeatConstants.Dominate,
                FeatConstants.CreateSpawn,
                FeatConstants.EnergyDrain,
                FeatConstants.AlternateForm,
                FeatConstants.DamageReduction,
                FeatConstants.FastHealing,
                FeatConstants.GaseousForm,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.SpellLikeAbility + SpellConstants.SpiderClimb,
                FeatConstants.TurnResistance,
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.SenseMotive,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.Alertness,
                FeatConstants.CombatReflexes,
                FeatConstants.Dodge,
                FeatConstants.ImprovedInitiative,
                FeatConstants.LightningReflexes
            };

            base.DistinctCollection(RaceConstants.Metaraces.Vampire, featNames);
        }

        [Test]
        public void PaladinFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.AuraOfGood,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectEvil,
                FeatConstants.SmiteEvil + "1",
                FeatConstants.SmiteEvil + "2",
                FeatConstants.SmiteEvil + "3",
                FeatConstants.SmiteEvil + "4",
                FeatConstants.SmiteEvil + "5",
                FeatConstants.DivineGrace,
                FeatConstants.LayOnHands,
                FeatConstants.AuraOfCourage,
                FeatConstants.DivineHealth,
                FeatConstants.Turn,
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "1",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "2",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "3",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "4",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "5"
            };

            base.DistinctCollection(CharacterClassConstants.Paladin, featNames);
        }

        [Test]
        public void DruidFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dart,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Sling,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Shortspear,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Longspear,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Scimitar,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Sickle,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.NatureSense,
                FeatConstants.WildEmpathy,
                FeatConstants.WildShape + "1",
                FeatConstants.WildShape + "2",
                FeatConstants.WildShape + "3",
                FeatConstants.WildShape + "4",
                FeatConstants.WildShape + "5",
                FeatConstants.WildShape + "6",
                FeatConstants.WildShape + "Tiny",
                FeatConstants.WildShape + "Large",
                FeatConstants.WildShape + "Plant",
                FeatConstants.WildShape + "Huge",
                FeatConstants.WildShape + "ElementalHuge",
                FeatConstants.WildShape + "Elemental1",
                FeatConstants.WildShape + "Elemental2",
                FeatConstants.WildShape + "Elemental3",
                FeatConstants.WoodlandStride,
                FeatConstants.TracklessStep,
                FeatConstants.ResistNaturesLure,
                FeatConstants.VenomImmunity,
                FeatConstants.AThousandFaces,
                FeatConstants.TimelessBody
            };

            base.DistinctCollection(CharacterClassConstants.Druid, featNames);
        }

        [Test]
        public void MindFlayerFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.NaturalArmor,
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Concentration,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Intimidate,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.SenseMotive,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.Darkvision,
                FeatConstants.MindBlast,
                FeatConstants.NaturalWeapon,
                FeatConstants.SpellLikeAbility + SpellConstants.CharmMonster,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
                FeatConstants.SpellLikeAbility + SpellConstants.Levitate,
                FeatConstants.SpellLikeAbility + SpellConstants.PlaneShift,
                FeatConstants.SpellLikeAbility + SpellConstants.Suggestion,
                FeatConstants.ImprovedGrab,
                FeatConstants.Extract
            };

            base.DistinctCollection(RaceConstants.BaseRaces.MindFlayer, featNames);
        }

        [Test]
        public void HalfFiendFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon + "Claw",
                FeatConstants.NaturalWeapon + "Bite",
                FeatConstants.SmiteGood,
                FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
                FeatConstants.SpellLikeAbility + SpellConstants.Desecrate,
                FeatConstants.SpellLikeAbility + SpellConstants.UnholyBlight,
                FeatConstants.SpellLikeAbility + SpellConstants.Poison,
                FeatConstants.SpellLikeAbility + SpellConstants.Contagion,
                FeatConstants.SpellLikeAbility + SpellConstants.Blasphemy,
                FeatConstants.SpellLikeAbility + SpellConstants.UnholyAura,
                FeatConstants.SpellLikeAbility + SpellConstants.Unhallow,
                FeatConstants.SpellLikeAbility + SpellConstants.HorridWilting,
                FeatConstants.SpellLikeAbility + SpellConstants.SummonMonsterIX,
                FeatConstants.SpellLikeAbility + SpellConstants.Destruction,
                FeatConstants.Darkvision,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.Resistance + FeatConstants.Foci.Fire,
                FeatConstants.DamageReduction + "11-",
                FeatConstants.DamageReduction + "12+",
                FeatConstants.SpellResistance
            };

            base.DistinctCollection(RaceConstants.Metaraces.HalfFiend, featNames);
        }

        [Test]
        public void RangerFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.FavoredEnemy + "1",
                FeatConstants.FavoredEnemy + "2",
                FeatConstants.FavoredEnemy + "3",
                FeatConstants.FavoredEnemy + "4",
                FeatConstants.FavoredEnemy + "5",
                FeatConstants.Track,
                FeatConstants.WildEmpathy,
                FeatConstants.CombatStyle,
                FeatConstants.Endurance,
                FeatConstants.ImprovedCombatStyle,
                FeatConstants.WoodlandStride,
                FeatConstants.SwiftTracker,
                FeatConstants.Evasion,
                FeatConstants.CombatStyleMastery,
                FeatConstants.Camouflage,
                FeatConstants.HideInPlainSight,
                FeatConstants.RapidShot + CharacterClassConstants.Ranger,
                FeatConstants.TwoWeaponFighting + CharacterClassConstants.Ranger,
                FeatConstants.Manyshot + CharacterClassConstants.Ranger,
                FeatConstants.ImprovedTwoWeaponFighting + CharacterClassConstants.Ranger,
                FeatConstants.ImprovedPreciseShot + CharacterClassConstants.Ranger,
                FeatConstants.GreaterTwoWeaponFighting + CharacterClassConstants.Ranger
            };

            base.DistinctCollection(CharacterClassConstants.Ranger, featNames);
        }

        [Test]
        public void MonkFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Sling,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Handaxe,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Javelin,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Kama,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Nunchaku,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Shuriken,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Siangham,
                FeatConstants.ArmorBonus + StatConstants.Wisdom,
                FeatConstants.ArmorBonus + "1",
                FeatConstants.ArmorBonus + "2",
                FeatConstants.ArmorBonus + "3",
                FeatConstants.ArmorBonus + "4",
                FeatConstants.FlurryOfBlows,
                FeatConstants.GreaterFlurry,
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d4",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d10",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "2d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "2d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "1d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "1d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "1d10",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "2d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "2d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "2d10",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "1d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "2d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "2d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "3d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "3d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "4d8",
                FeatConstants.ImprovedUnarmedStrike,
                FeatConstants.MonkBonusFeat + "1",
                FeatConstants.MonkBonusFeat + "2",
                FeatConstants.MonkBonusFeat + "6",
                FeatConstants.ImprovedGrapple + CharacterClassConstants.Monk,
                FeatConstants.StunningFist + CharacterClassConstants.Monk,
                FeatConstants.CombatReflexes + CharacterClassConstants.Monk,
                FeatConstants.DeflectArrows + CharacterClassConstants.Monk,
                FeatConstants.ImprovedDisarm + CharacterClassConstants.Monk,
                FeatConstants.ImprovedTrip + CharacterClassConstants.Monk,
                FeatConstants.Evasion,
                FeatConstants.FastMovement + "10",
                FeatConstants.FastMovement + "20",
                FeatConstants.FastMovement + "30",
                FeatConstants.FastMovement + "40",
                FeatConstants.FastMovement + "50",
                FeatConstants.FastMovement + "60",
                FeatConstants.StillMind,
                FeatConstants.KiStrike + "Magic",
                FeatConstants.KiStrike + "Lawful",
                FeatConstants.KiStrike + "Adamantine",
                FeatConstants.SlowFall + "20",
                FeatConstants.SlowFall + "30",
                FeatConstants.SlowFall + "40",
                FeatConstants.SlowFall + "50",
                FeatConstants.SlowFall + "60",
                FeatConstants.SlowFall + "70",
                FeatConstants.SlowFall + "80",
                FeatConstants.SlowFall + "90",
                FeatConstants.SlowFall + "Any",
                FeatConstants.PurityOfBody,
                FeatConstants.WholenessOfBody,
                FeatConstants.ImprovedEvasion,
                FeatConstants.DiamondBody,
                FeatConstants.AbundantStep,
                FeatConstants.DiamondSoul,
                FeatConstants.QuiveringPalm,
                FeatConstants.TimelessBody,
                FeatConstants.TongueOfSunAndMoon,
                FeatConstants.EmptyBody,
                FeatConstants.PerfectSelf
            };

            base.DistinctCollection(CharacterClassConstants.Monk, featNames);
        }

        [Test]
        public void HalfCelestialFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SpellLikeAbility + SpellConstants.Daylight,
                FeatConstants.SmiteEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.ProtectionFromEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.Bless,
                FeatConstants.SpellLikeAbility + SpellConstants.Aid,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.CureSeriousWounds,
                FeatConstants.SpellLikeAbility + SpellConstants.NeutralizePoison,
                FeatConstants.SpellLikeAbility + SpellConstants.HolySmite,
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease,
                FeatConstants.SpellLikeAbility + SpellConstants.DispelEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.HolyWord,
                FeatConstants.SpellLikeAbility + SpellConstants.HolyAura,
                FeatConstants.SpellLikeAbility + SpellConstants.Hallow,
                FeatConstants.SpellLikeAbility + SpellConstants.MassCharmMonster,
                FeatConstants.SpellLikeAbility + SpellConstants.SummonMonsterIX,
                FeatConstants.SpellLikeAbility + SpellConstants.Resurrection,
                FeatConstants.NaturalArmor,
                FeatConstants.Darkvision,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.DamageReduction + "11-",
                FeatConstants.DamageReduction + "12+",
                FeatConstants.SpellResistance,
                FeatConstants.SaveBonus
            };

            base.DistinctCollection(RaceConstants.Metaraces.HalfCelestial, featNames);
        }

        [Test]
        public void BarbarianFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.FastMovement,
                FeatConstants.Illiteracy,
                FeatConstants.Rage + "1",
                FeatConstants.Rage + "2",
                FeatConstants.Rage + "3",
                FeatConstants.Rage + "4",
                FeatConstants.Rage + "5",
                FeatConstants.Rage + "6",
                FeatConstants.UncannyDodge,
                FeatConstants.TrapSense + "1",
                FeatConstants.TrapSense + "2",
                FeatConstants.TrapSense + "3",
                FeatConstants.TrapSense + "4",
                FeatConstants.TrapSense + "5",
                FeatConstants.TrapSense + "6",
                FeatConstants.ImprovedUncannyDodge,
                FeatConstants.DamageReduction + "1",
                FeatConstants.DamageReduction + "2",
                FeatConstants.DamageReduction + "3",
                FeatConstants.DamageReduction + "4",
                FeatConstants.DamageReduction + "5",
                FeatConstants.GreaterRage,
                FeatConstants.IndomitableWill,
                FeatConstants.TirelessRage,
                FeatConstants.MightyRage
            };

            base.DistinctCollection(CharacterClassConstants.Barbarian, featNames);
        }

        [Test]
        public void BardFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Whip,
                FeatConstants.LightArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.BardicMusic + "1",
                FeatConstants.BardicMusic + "2",
                FeatConstants.BardicMusic + "3",
                FeatConstants.BardicMusic + "4",
                FeatConstants.BardicMusic + "5",
                FeatConstants.BardicMusic + "6",
                FeatConstants.BardicMusic + "7",
                FeatConstants.BardicMusic + "8",
                FeatConstants.BardicMusic + "9",
                FeatConstants.BardicMusic + "10",
                FeatConstants.BardicMusic + "11",
                FeatConstants.BardicMusic + "12",
                FeatConstants.BardicMusic + "13",
                FeatConstants.BardicMusic + "14",
                FeatConstants.BardicMusic + "15",
                FeatConstants.BardicMusic + "16",
                FeatConstants.BardicMusic + "17",
                FeatConstants.BardicMusic + "18",
                FeatConstants.BardicMusic + "19",
                FeatConstants.BardicMusic + "20",
                FeatConstants.BardicKnowledge,
                FeatConstants.Countersong,
                FeatConstants.Fascinate,
                FeatConstants.InspireCourage + "1",
                FeatConstants.InspireCourage + "2",
                FeatConstants.InspireCourage + "3",
                FeatConstants.InspireCourage + "4",
                FeatConstants.InspireCompetence,
                FeatConstants.Suggestion,
                FeatConstants.InspireGreatness,
                FeatConstants.SongOfFreedom,
                FeatConstants.InspireHeroics,
                FeatConstants.MassSuggestion
            };

            base.DistinctCollection(CharacterClassConstants.Bard, featNames);
        }

        [Test]
        public void RogueFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.SneakAttack + "1",
                FeatConstants.SneakAttack + "2",
                FeatConstants.SneakAttack + "3",
                FeatConstants.SneakAttack + "4",
                FeatConstants.SneakAttack + "5",
                FeatConstants.SneakAttack + "6",
                FeatConstants.SneakAttack + "7",
                FeatConstants.SneakAttack + "8",
                FeatConstants.SneakAttack + "9",
                FeatConstants.SneakAttack + "10",
                FeatConstants.Trapfinding,
                FeatConstants.Evasion,
                FeatConstants.UncannyDodge,
                FeatConstants.TrapSense + "1",
                FeatConstants.TrapSense + "2",
                FeatConstants.TrapSense + "3",
                FeatConstants.TrapSense + "4",
                FeatConstants.TrapSense + "5",
                FeatConstants.TrapSense + "6",
                FeatConstants.ImprovedUncannyDodge,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.HandCrossbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
                FeatConstants.LightArmorProficiency
            };

            base.DistinctCollection(CharacterClassConstants.Rogue, featNames);
        }

        [Test]
        public void AdditionalFeatGroup()
        {
            var featNames = new[]
            {
                FeatConstants.Acrobatic,
                FeatConstants.Agile,
                FeatConstants.Alertness,
                FeatConstants.AnimalAffinity,
                FeatConstants.Athletic,
                FeatConstants.AugmentSummoning,
                FeatConstants.BlindFight,
                FeatConstants.Cleave,
                FeatConstants.CombatCasting,
                FeatConstants.CombatExpertise,
                FeatConstants.CombatReflexes,
                FeatConstants.CripplingStrike,
                FeatConstants.Deceitful,
                FeatConstants.DefensiveRoll,
                FeatConstants.DeflectArrows,
                FeatConstants.DeftHands,
                FeatConstants.Diehard,
                FeatConstants.Diligent,
                FeatConstants.Dodge,
                FeatConstants.EmpowerSpell,
                FeatConstants.Endurance,
                FeatConstants.EnlargeSpell,
                FeatConstants.EschewMaterials,
                FeatConstants.ExoticWeaponProficiency,
                FeatConstants.ExtendSpell,
                FeatConstants.ExtraTurning,
                FeatConstants.FarShot,
                FeatConstants.GreatCleave,
                FeatConstants.GreaterSpellFocus,
                FeatConstants.GreaterSpellPenetration,
                FeatConstants.GreaterTwoWeaponFighting,
                FeatConstants.GreaterWeaponFocus,
                FeatConstants.GreaterWeaponSpecialization,
                FeatConstants.GreatFortitude,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.HeightenSpell,
                FeatConstants.ImprovedBullRush,
                FeatConstants.ImprovedCounterspell,
                FeatConstants.ImprovedCritical,
                FeatConstants.ImprovedDisarm,
                FeatConstants.ImprovedEvasion,
                FeatConstants.ImprovedFamiliar,
                FeatConstants.ImprovedFeint,
                FeatConstants.ImprovedGrapple,
                FeatConstants.ImprovedInitiative,
                FeatConstants.ImprovedOverrun,
                FeatConstants.ImprovedPreciseShot,
                FeatConstants.ImprovedShieldBash,
                FeatConstants.ImprovedSunder,
                FeatConstants.ImprovedTrip,
                FeatConstants.ImprovedTurning,
                FeatConstants.ImprovedTwoWeaponFighting,
                FeatConstants.ImprovedUnarmedStrike,
                FeatConstants.Investigator,
                FeatConstants.IronWill,
                FeatConstants.Leadership,
                FeatConstants.LightArmorProficiency,
                FeatConstants.LightningReflexes,
                FeatConstants.MagicalAptitude,
                FeatConstants.Manyshot,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.MaximizeSpell,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.Mobility,
                FeatConstants.MountedArchery,
                FeatConstants.MountedCombat,
                FeatConstants.NaturalSpell,
                FeatConstants.Negotiator,
                FeatConstants.NimbleFingers,
                FeatConstants.Opportunist,
                FeatConstants.Persuasive,
                FeatConstants.PointBlankShot,
                FeatConstants.PowerAttack,
                FeatConstants.PreciseShot,
                FeatConstants.QuickDraw,
                FeatConstants.QuickenSpell,
                FeatConstants.RapidReload,
                FeatConstants.RapidShot,
                FeatConstants.RideByAttack,
                FeatConstants.Run,
                FeatConstants.SelfSufficient,
                FeatConstants.ScribeScroll,
                FeatConstants.ShieldProficiency,
                FeatConstants.ShotOnTheRun,
                FeatConstants.SilentSpell,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.SkillFocus,
                FeatConstants.SkillMastery,
                FeatConstants.SlipperyMind,
                FeatConstants.SnatchArrows,
                FeatConstants.SpellFocus,
                FeatConstants.SpellMastery,
                FeatConstants.SpellPenetration,
                FeatConstants.SpiritedCharge,
                FeatConstants.SpringAttack,
                FeatConstants.Stealthy,
                FeatConstants.StillSpell,
                FeatConstants.StunningFist,
                FeatConstants.Toughness,
                FeatConstants.TowerShieldProficiency,
                FeatConstants.Track,
                FeatConstants.Trample,
                FeatConstants.TwoWeaponDefense,
                FeatConstants.TwoWeaponFighting,
                FeatConstants.WeaponFocus,
                FeatConstants.WeaponFinesse,
                FeatConstants.WeaponSpecialization,
                FeatConstants.WhirlwindAttack,
                FeatConstants.WidenSpell
            };

            base.DistinctCollection(GroupConstants.Additional, featNames);
        }

        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.ImprovedCombatStyle,
            FeatConstants.CombatStyle)]
        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.CombatStyleMastery,
            FeatConstants.ImprovedCombatStyle)]
        public void FeatGroupDependency(string name, string feat, string dependencyFeat)
        {
            var collection = table[name].ToList();
            var featIndex = collection.IndexOf(feat);
            var dependencyIndex = collection.IndexOf(dependencyFeat);

            Assert.That(featIndex, Is.Positive);
            Assert.That(dependencyIndex, Is.Positive);
            Assert.That(featIndex, Is.GreaterThan(dependencyIndex));
        }
    }
}