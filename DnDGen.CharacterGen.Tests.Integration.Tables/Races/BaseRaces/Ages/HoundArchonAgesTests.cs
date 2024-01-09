using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class HoundArchonAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.HoundArchon); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                RaceConstants.Ages.Adulthood,
                RaceConstants.Ages.MiddleAge,
                RaceConstants.Ages.Old,
                RaceConstants.Ages.Venerable
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.Ages.Adulthood, 14)]
        [TestCase(RaceConstants.Ages.MiddleAge, 72)]
        [TestCase(RaceConstants.Ages.Old, 144)]
        [TestCase(RaceConstants.Ages.Venerable, 216)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}