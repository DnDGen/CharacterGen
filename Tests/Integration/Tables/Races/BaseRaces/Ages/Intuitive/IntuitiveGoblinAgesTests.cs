using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Intuitive
{
    [TestFixture]
    public class IntuitiveGoblinAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, GroupConstants.Intuitive, RaceConstants.BaseRaces.Goblin); }
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

        [TestCase(AdjustmentConstants.Adulthood, 12)]
        [TestCase(AdjustmentConstants.Quantity, 1)]
        [TestCase(AdjustmentConstants.Die, 4)]
        [TestCase(AdjustmentConstants.MiddleAge, 20)]
        [TestCase(AdjustmentConstants.Old, 30)]
        [TestCase(AdjustmentConstants.Venerable, 40)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
