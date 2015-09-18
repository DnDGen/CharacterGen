using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class BugbearAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Bugbear); }
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
        [TestCase(AdjustmentConstants.Die + GroupConstants.Intuitive, 4)]
        [TestCase(AdjustmentConstants.Quantity + GroupConstants.SelfTaught, 1)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.SelfTaught, 6)]
        [TestCase(AdjustmentConstants.Quantity + GroupConstants.Trained, 1)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.Trained, 6)]
        [TestCase(RaceConstants.Ages.Adulthood, 11)]
        [TestCase(RaceConstants.Ages.MiddleAge, 30)]
        [TestCase(RaceConstants.Ages.Old, 45)]
        [TestCase(RaceConstants.Ages.Venerable, 60)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
