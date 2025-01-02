using DnDGen.CharacterGen.Combats;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class DireWereboarFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.Wereboar_Dire); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
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
                FeatConstants.Scent,
                FeatConstants.Alertness,
                FeatConstants.Endurance,
                FeatConstants.IronWill,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.AlternateForm,
            FeatConstants.AlternateForm,
            RaceConstants.BaseRaces.Animals.DireBoar,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Empathy,
            FeatConstants.Empathy,
            RaceConstants.BaseRaces.Animals.DireBoar,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.Lycanthropy,
            FeatConstants.Lycanthropy,
            RaceConstants.Metaraces.Wereboar_Dire,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Ferocity,
            FeatConstants.Ferocity,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Alertness,
            FeatConstants.Alertness,
            "",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.Endurance,
            FeatConstants.Endurance,
            "",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.IronWill,
            FeatConstants.IronWill,
            "",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
