using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodWizardMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodWizardMetaraces"; }
        }

        [Test]
        public void GoodWizardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 97);
        }

        [Test]
        public void GoodWizardHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodWizardHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodWizardWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}