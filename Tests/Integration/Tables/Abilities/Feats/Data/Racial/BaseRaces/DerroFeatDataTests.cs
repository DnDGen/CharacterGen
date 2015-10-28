using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class DerroFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.Derro); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Madness,
                FeatConstants.PoisonUse,
                FeatConstants.SneakAttack,
                FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
                FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
                FeatConstants.SpellLikeAbility + SpellConstants.Daze,
                FeatConstants.SpellLikeAbility + SpellConstants.SoundBurst,
                FeatConstants.VulnerabilityToSunlight,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Madness,
            FeatConstants.Madness,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.PoisonUse,
            FeatConstants.PoisonUse,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SneakAttack,
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Darkness,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility,
            SpellConstants.GhostSound,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Daze,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Daze,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SoundBurst,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SoundBurst,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.VulnerabilityToSunlight,
            FeatConstants.VulnerabilityToSunlight,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
