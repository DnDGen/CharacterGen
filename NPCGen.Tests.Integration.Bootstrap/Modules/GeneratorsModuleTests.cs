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
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void CharacterFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterGenerator>();
        }

        [Test]
        public void CharacterClassFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterClassGenerator>();
        }

        [Test]
        public void HitPointsFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void LanguageFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageGenerator>();
        }

        [Test]
        public void RaceFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRaceGenerator>();
        }

        [Test]
        public void RandomizerVerifiersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRandomizerVerifier>();
        }

        [Test]
        public void StatsFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatsGenerator>();
        }
    }
}