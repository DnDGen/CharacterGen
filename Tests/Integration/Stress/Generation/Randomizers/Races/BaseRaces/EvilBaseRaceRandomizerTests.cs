using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class EvilBaseRaceRandomizerTests : StressTest
    {
        [Inject]
        public EvilBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void EvilBaseRaceRandomizationReturnsBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();

                var baseRace = BaseRaceRandomizer.Randomize(alignment.Goodness, prototype);
                Assert.That(baseRace, Is.Not.Null);
                Assert.That(baseRace, Is.Not.Empty);
            }

            AssertIterations();
        }

        [Test]
        public void EvilBaseRaceRandomizerAlwaysReturnsEvilBaseRace()
        {
            var evilBaseRaces = new[]
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

            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();

                var baseRace = BaseRaceRandomizer.Randomize(alignment.Goodness, prototype);
                Assert.That(evilBaseRaces.Contains(baseRace), Is.True);
            }

            AssertIterations();
        }
    }
}