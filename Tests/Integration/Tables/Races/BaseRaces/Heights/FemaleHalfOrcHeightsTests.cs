using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class FemaleHalfOrcHeightsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.GENDERRACEHeights, "Female", RaceConstants.BaseRaces.HalfOrc); }
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

        [TestCase(AdjustmentConstants.Base, 53)]
        [TestCase(AdjustmentConstants.Quantity, 2)]
        [TestCase(AdjustmentConstants.Die, 12)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
