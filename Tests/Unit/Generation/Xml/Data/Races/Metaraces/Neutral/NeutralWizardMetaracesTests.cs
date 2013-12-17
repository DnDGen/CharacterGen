using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralWizardMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralWizardMetaraces";
        }

        [Test]
        public void NeutralWizardEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralWizardWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralWizardWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}