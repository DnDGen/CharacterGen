using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class GoodBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public GoodBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        private IEnumerable<String> baseRaces;

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<GoodBaseRaceRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            baseRaces = new[]
                {
                    RaceConstants.BaseRaces.Aasimar,
                    RaceConstants.BaseRaces.Svirfneblin,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.ForestGnome,
                    RaceConstants.BaseRaces.GrayElf,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.LightfootHalfling,
                    RaceConstants.BaseRaces.MountainDwarf,
                    RaceConstants.BaseRaces.RockGnome,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.WoodElf
                };
        }

        [Test]
        public void GoodBaseRaceRandomizerAlwaysReturnsGoodBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var baseRace = BaseRaceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(baseRaces.Contains(baseRace), Is.True);
            }

            AssertIterations();
        }
    }
}