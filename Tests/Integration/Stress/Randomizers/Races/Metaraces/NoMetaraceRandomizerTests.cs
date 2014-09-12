using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [Inject, Named(MetaraceRandomizerTypeConstants.None)]
        public override IMetaraceRandomizer MetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get { return new[] { String.Empty }; }
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