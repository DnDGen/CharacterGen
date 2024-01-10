using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;
using DnDGen.TreasureGen.Items;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class RockGnomeFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.RockGnome); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
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
                FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.WeaponFamiliarity,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.GnomeHookedHammer,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            CharacterClassConstants.Schools.Illusion,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.ImprovedSpell,
            FeatConstants.ImprovedSpell,
            CharacterClassConstants.Schools.Illusion,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus,
            "Goblinoids",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.AttackBonus,
            RaceConstants.BaseRaces.Kobold,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            "Giant",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SpeakWithAnimals + " (burrowing animals only), duration 1 minute",
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DancingLights,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, AbilityConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility,
            SpellConstants.GhostSound,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, AbilityConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Prestidigitation,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, AbilityConstants.Charisma)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
