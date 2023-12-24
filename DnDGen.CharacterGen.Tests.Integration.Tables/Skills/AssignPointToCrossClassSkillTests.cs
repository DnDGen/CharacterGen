using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Skills
{
    [TestFixture]
    public class AssignPointToCrossClassSkillTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill; }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 67, false)]
        [TestCase(68, 100, true)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}