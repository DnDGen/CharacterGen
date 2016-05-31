using CharacterGen.Abilities.Feats;
using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.Metaraces.Species
{
    [TestFixture]
    public class GreenFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.Species.Green); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.NaturalWeapon,
                FeatConstants.ImmuneToEffect
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.NaturalWeapon,
            FeatConstants.NaturalWeapon,
            "Breath (30 ft cone of acid gas) (see Half-Dragon monster listing)",
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Acid,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
