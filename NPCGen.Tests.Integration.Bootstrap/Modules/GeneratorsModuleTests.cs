using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Verifiers;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : BootstrapTests
    {
        [Test]
        public void AlignmentFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAlignmentFactory>();
        }

        [Test]
        public void CharacterFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterFactory>();
        }

        [Test]
        public void CharacterClassFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterClassFactory>();
        }

        [Test]
        public void HitPointsFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IHitPointsFactory>();
        }

        [Test]
        public void LanguageFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageFactory>();
        }

        [Test]
        public void RaceFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRaceFactory>();
        }

        [Test]
        public void RandomizerVerifiersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRandomizerVerifier>();
        }

        [Test]
        public void StatsFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatsFactory>();
        }
    }
}