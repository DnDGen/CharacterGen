using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class SizeModifiersTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.SizeModifiers;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                RaceConstants.Sizes.Colossal,
                RaceConstants.Sizes.Gargantuan,
                RaceConstants.Sizes.Huge,
                RaceConstants.Sizes.Large,
                RaceConstants.Sizes.Medium,
                RaceConstants.Sizes.Small,
                RaceConstants.Sizes.Tiny,
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.Sizes.Colossal, -8)]
        [TestCase(RaceConstants.Sizes.Gargantuan, -4)]
        [TestCase(RaceConstants.Sizes.Huge, -2)]
        [TestCase(RaceConstants.Sizes.Large, -1)]
        [TestCase(RaceConstants.Sizes.Medium, 0)]
        [TestCase(RaceConstants.Sizes.Small, 1)]
        [TestCase(RaceConstants.Sizes.Tiny, 2)]
        public void SizeModifier(string size, int adjustment)
        {
            base.Adjustment(size, adjustment);
        }
    }
}
