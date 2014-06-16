using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilWizardMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilWizardMetaraces"; }
        }

        [Test]
        public void EvilWizardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 96);
        }

        [Test]
        public void EvilWizardWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 97);
        }

        [Test]
        public void EvilWizardWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 98);
        }

        [Test]
        public void EvilWizardHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilWizardHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}