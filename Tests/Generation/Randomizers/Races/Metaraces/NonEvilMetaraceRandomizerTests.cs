using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonEvilMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonEvilMetaraceRandomizer(mockPercentileResultProvider.Object, mockLevelAdjustmentsProvider.Object);
        }

        [Test]
        public void HalfCelestialIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.HalfCelestial);
        }

        [Test]
        public void HalfDragonIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.HalfDragon);
        }

        [Test]
        public void HalfFiendIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.HalfFiend);
        }

        [Test]
        public void WerebearIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Werebear);
        }

        [Test]
        public void WereboarIsAllowed()
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