using NPCGen.Core.Data.Races;
using NPCGen.Core.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class EvilMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new EvilMetaraceRandomizer(mockPercentileResultProvider.Object, mockLevelAdjustmentsProvider.Object);
        }

        [Test]
        public void HalfCelestialIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.HalfCelestial);
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
        public void WerebearIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Werebear);
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