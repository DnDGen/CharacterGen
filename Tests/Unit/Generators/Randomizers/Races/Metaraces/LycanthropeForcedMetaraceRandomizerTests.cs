using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeForcedMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new LycanthropeForcedMetaraceRandomizer(mockPercentileResultSelector.Object, mockLevelAdjustmentsSelector.Object);
        }

        [Test]
        public void HalfCelestialIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.HalfCelestial);
        }

        [Test]
        public void HalfDragonIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.Metaraces.HalfFiend);
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
        public void WereratIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Wererat);
        }

        [Test]
        public void WeretigerIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Weretiger);
        }

        [Test]
        public void WerewolfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.Metaraces.Werewolf);
        }
    }
}