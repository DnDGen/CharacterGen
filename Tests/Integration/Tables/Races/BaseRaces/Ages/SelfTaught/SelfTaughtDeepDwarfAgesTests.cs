using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.SelfTaught
{
    [TestFixture]
    public class SelfTaughtDeepDwarfAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, GroupConstants.SelfTaught, RaceConstants.BaseRaces.DeepDwarf); }
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

        [TestCase(AdjustmentConstants.Adulthood, 40)]
        [TestCase(AdjustmentConstants.Quantity, 5)]
        [TestCase(AdjustmentConstants.Die, 6)]
        [TestCase(AdjustmentConstants.MiddleAge, 125)]
        [TestCase(AdjustmentConstants.Old, 188)]
        [TestCase(AdjustmentConstants.Venerable, 250)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
