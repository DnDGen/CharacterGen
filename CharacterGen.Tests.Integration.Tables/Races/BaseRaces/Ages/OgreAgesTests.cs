using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class OgreAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.Ogre); }
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

        [TestCase(RaceConstants.Ages.Adulthood, 30)]
        [TestCase(RaceConstants.Ages.MiddleAge, 60)]
        [TestCase(RaceConstants.Ages.Old, 80)]
        [TestCase(RaceConstants.Ages.Venerable, 120)]
        public void Age(string ageDescription, int age)
        {
            base.Adjustment(ageDescription, age);
        }
    }
}
