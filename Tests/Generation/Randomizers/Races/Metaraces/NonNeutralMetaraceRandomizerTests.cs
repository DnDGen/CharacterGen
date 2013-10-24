using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonNeutralMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
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
        public void HalfFiendIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.HalfFiend);
        }

        [Test]
        public void WerebearIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Werebear);
        }

        [Test]
        public void WereboarIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Wereboar);
        }

        [Test]
        public void WereratIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Wererat);
        }

        [Test]
        public void WeretigerIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Weretiger);
        }

        [Test]
        public void WerewolfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Werewolf);
        }
    }
}