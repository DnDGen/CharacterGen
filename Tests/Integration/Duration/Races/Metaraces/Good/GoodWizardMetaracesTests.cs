using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodWizardMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodWizardMetaraces";
        }

        [Test]
        public void GoodWizardEmptyPercentile()
        {
            AssertEmpty(1, 97);
        }

        [Test]
        public void GoodWizardHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodWizardHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodWizardWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}