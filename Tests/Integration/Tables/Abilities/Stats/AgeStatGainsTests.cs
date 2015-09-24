using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class AgeStatGainsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.AgeStatGains;
            }
        }

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

        [TestCase(RaceConstants.Ages.Adulthood, 0)]
        [TestCase(RaceConstants.Ages.MiddleAge, 1)]
        [TestCase(RaceConstants.Ages.Old, 2)]
        [TestCase(RaceConstants.Ages.Venerable, 3)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
