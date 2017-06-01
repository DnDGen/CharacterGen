using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class KuoToaAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, RaceConstants.BaseRaces.KuoToa); }
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

        [TestCase(RaceConstants.Ages.Adulthood, 10)]
        [TestCase(RaceConstants.Ages.MiddleAge, 20)]
        [TestCase(RaceConstants.Ages.Old, 40)]
        [TestCase(RaceConstants.Ages.Venerable, 50)]
        public void KuoToaAges(string name, int age)
        {
            base.Adjustment(name, age);
        }
    }
}