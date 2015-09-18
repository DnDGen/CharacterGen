using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class LizardfolkAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Lizardfolk); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AdjustmentConstants.Quantity + GroupConstants.Intuitive,
                AdjustmentConstants.Die + GroupConstants.Intuitive,
                AdjustmentConstants.Quantity + GroupConstants.SelfTaught,
                AdjustmentConstants.Die + GroupConstants.SelfTaught,
                AdjustmentConstants.Quantity + GroupConstants.Trained,
                AdjustmentConstants.Die + GroupConstants.Trained,
                RaceConstants.Ages.Adulthood,
                RaceConstants.Ages.MiddleAge,
                RaceConstants.Ages.Old,
                RaceConstants.Ages.Venerable
            };

            AssertCollectionNames(names);
        }

        [TestCase(AdjustmentConstants.Quantity + GroupConstants.Intuitive, 1)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.Intuitive, 3)]
        [TestCase(AdjustmentConstants.Quantity + GroupConstants.SelfTaught, 1)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.SelfTaught, 6)]
        [TestCase(AdjustmentConstants.Quantity + GroupConstants.Trained, 1)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.Trained, 10)]
        [TestCase(RaceConstants.Ages.Adulthood, 10)]
        [TestCase(RaceConstants.Ages.MiddleAge, 30)]
        [TestCase(RaceConstants.Ages.Old, 50)]
        [TestCase(RaceConstants.Ages.Venerable, 65)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
