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
    public class EvilBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public EvilBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        private IEnumerable<String> baseRaces;

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<EvilBaseRaceRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            baseRaces = new[]
                {
                    RaceConstants.BaseRaces.Bugbear,
                    RaceConstants.BaseRaces.Derro,
                    RaceConstants.BaseRaces.Drow,
                    RaceConstants.BaseRaces.DuergarDwarf,
                    RaceConstants.BaseRaces.Gnoll,
                    RaceConstants.BaseRaces.Goblin,
                    RaceConstants.BaseRaces.Hobgoblin,
                    RaceConstants.BaseRaces.Kobold,
                    RaceConstants.BaseRaces.Ogre,
                    RaceConstants.BaseRaces.OgreMage,
                    RaceConstants.BaseRaces.Orc,
                    RaceConstants.BaseRaces.Troglodyte,
                    RaceConstants.BaseRaces.MindFlayer,
                    RaceConstants.BaseRaces.Minotaur,
                    RaceConstants.BaseRaces.Tiefling,
                    RaceConstants.BaseRaces.Lizardfolk,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.LightfootHalfling,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.WoodElf
                };
        }

        [Test]
        public void EvilBaseRaceRandomizerAlwaysReturnsEvilBaseRace()
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