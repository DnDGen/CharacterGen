using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class GoodForcedMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [Inject, Named(MetaraceRandomizerTypeConstants.GoodForced)]
        public override IMetaraceRandomizer MetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfCelestial,
                    RaceConstants.Metaraces.Werebear
                };
            }
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }
    }
}