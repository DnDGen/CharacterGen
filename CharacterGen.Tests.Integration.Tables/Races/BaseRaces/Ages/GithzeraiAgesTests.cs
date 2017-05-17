using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class GithzeraiAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Githzerai); }
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
        [TestCase(RaceConstants.Ages.MiddleAge, 50)]
        [TestCase(RaceConstants.Ages.Old, 75)]
        [TestCase(RaceConstants.Ages.Venerable, 100)]
        public void GithzeraiAges(string name, int age)
        {
            base.Adjustment(name, age);
        }
    }
}
