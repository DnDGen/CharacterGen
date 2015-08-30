using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class MaleHumanHeightsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.GENDERRACEHeights, "Male", RaceConstants.BaseRaces.Human); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AdjustmentConstants.Base,
                AdjustmentConstants.Quantity,
                AdjustmentConstants.Die
            };

            AssertCollectionNames(names);
        }

        [TestCase(AdjustmentConstants.Base, 58)]
        [TestCase(AdjustmentConstants.Quantity, 2)]
        [TestCase(AdjustmentConstants.Die, 10)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
