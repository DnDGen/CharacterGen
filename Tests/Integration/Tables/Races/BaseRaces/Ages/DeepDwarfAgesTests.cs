using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class DeepDwarfAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.DeepDwarf); }
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

        [TestCase(AdjustmentConstants.Quantity + GroupConstants.Intuitive, 3)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.Intuitive, 6)]
        [TestCase(AdjustmentConstants.Quantity + GroupConstants.SelfTaught, 5)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.SelfTaught, 6)]
        [TestCase(AdjustmentConstants.Quantity + GroupConstants.Trained, 7)]
        [TestCase(AdjustmentConstants.Die + GroupConstants.Trained, 6)]
        [TestCase(RaceConstants.Ages.Adulthood, 40)]
        [TestCase(RaceConstants.Ages.MiddleAge, 125)]
        [TestCase(RaceConstants.Ages.Old, 188)]
        [TestCase(RaceConstants.Ages.Venerable, 250)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
