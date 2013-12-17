using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class GeneticMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new GeneticMetaraceRandomizer(mockPercentileResultProvider.Object, mockLevelAdjustmentsProvider.Object);
        }

        [Test]
        public void HalfCelestialIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.HalfCelestial);
        }

        [Test]
        public void HalfDragonIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.HalfFiend);
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
        public void WereratIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Wererat);
        }

        [Test]
        public void WeretigerIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Weretiger);
        }

        [Test]
        public void WerewolfIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.Werewolf);
        }
    }
}