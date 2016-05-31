using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class TieflingAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Tiefling); }
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

        [TestCase(RaceConstants.Ages.Adulthood, 20)]
        [TestCase(RaceConstants.Ages.MiddleAge, 35)]
        [TestCase(RaceConstants.Ages.Old, 53)]
        [TestCase(RaceConstants.Ages.Venerable, 70)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
