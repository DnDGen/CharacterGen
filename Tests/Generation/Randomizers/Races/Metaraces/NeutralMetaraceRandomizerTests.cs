using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NeutralMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            controlCase = RaceConstants.Metaraces.HalfDragon;
        }

        [Test]
        public void HalfCelestialIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.HalfCelestial);
        }

        [Test]
        public void HalfDragonIsAllowed()
        {
            AssertControlIsAllowed(RaceConstants.Metaraces.HalfFiend);
        }

        [Test]
        public void HalfFiendIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.HalfFiend);
        }

        [Test]
        public void WerebearIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Werebear);
        }

        [Test]
        public void WereboreIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Wereboar);
        }

        [Test]
        public void WereratIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Wererat);
        }

        [Test]
        public void WeretigerIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Weretiger);
        }

        [Test]
        public void WerewolfIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Werewolf);
        }
    }
}