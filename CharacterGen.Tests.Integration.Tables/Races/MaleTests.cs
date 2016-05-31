using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class MaleTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.TrueOrFalse.Male; }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 50, true)]
        [TestCase(51, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
