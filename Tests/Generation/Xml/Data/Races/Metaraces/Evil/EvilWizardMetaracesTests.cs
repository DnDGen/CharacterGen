using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilWizardMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilWizardMetaraces";
        }

        [Test]
        public void EvilWizardEmptyPercentile()
        {
            AssertEmpty(1, 96);
        }

        [Test]
        public void EvilWizardWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 97);
        }

        [Test]
        public void EvilWizardWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 98);
        }

        [Test]
        public void EvilWizardHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilWizardHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}