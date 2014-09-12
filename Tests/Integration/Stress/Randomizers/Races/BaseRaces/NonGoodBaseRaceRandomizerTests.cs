using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonGoodBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.NonGood)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                var goodBaseRaces = new[]
                {
                    RaceConstants.BaseRaces.Aasimar,
                    RaceConstants.BaseRaces.Svirfneblin
                };

                return RaceConstants.BaseRaces.GetBaseRaces().Except(goodBaseRaces);
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