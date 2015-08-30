using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Trained
{
    [TestFixture]
    public class TrainedTallfellowHalflingAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, GroupConstants.Trained, RaceConstants.BaseRaces.TallfellowHalfling); }
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
        [TestCase(AdjustmentConstants.Quantity, 4)]
        [TestCase(AdjustmentConstants.Die, 6)]
        [TestCase(AdjustmentConstants.MiddleAge, 50)]
        [TestCase(AdjustmentConstants.Old, 75)]
        [TestCase(AdjustmentConstants.Venerable, 100)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
