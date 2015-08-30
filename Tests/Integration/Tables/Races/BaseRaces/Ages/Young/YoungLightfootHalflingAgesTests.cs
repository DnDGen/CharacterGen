using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Young
{
    [TestFixture]
    public class YoungLightfootHalflingAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, GroupConstants.Young, RaceConstants.BaseRaces.LightfootHalfling); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AdjustmentConstants.Adulthood,
                AdjustmentConstants.Quantity,
                AdjustmentConstants.Die,
                AdjustmentConstants.MiddleAge,
                AdjustmentConstants.Old,
                AdjustmentConstants.Venerable
            };

            AssertCollectionNames(names);
        }

        [TestCase(AdjustmentConstants.Adulthood, 20)]
        [TestCase(AdjustmentConstants.Quantity, 2)]
        [TestCase(AdjustmentConstants.Die, 4)]
        [TestCase(AdjustmentConstants.MiddleAge, 50)]
        [TestCase(AdjustmentConstants.Old, 75)]
        [TestCase(AdjustmentConstants.Venerable, 100)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
