using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class WildElfAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.WildElf); }
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

        [TestCase(RaceConstants.Ages.Adulthood, 110)]
        [TestCase(RaceConstants.Ages.MiddleAge, 175)]
        [TestCase(RaceConstants.Ages.Old, 263)]
        [TestCase(RaceConstants.Ages.Venerable, 350)]
        public void RacialAges(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}