using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [Inject, Named(MetaraceRandomizerTypeConstants.Lycanthrope)]
        public override IMetaraceRandomizer MetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Werewolf,
                    String.Empty
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