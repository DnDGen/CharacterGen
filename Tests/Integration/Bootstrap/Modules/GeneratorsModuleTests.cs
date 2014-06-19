using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Randomizers.Alignments;
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

        [Test]
        public void AlignmentRandomzierNamedAnyIsAnyAlignmentRandomizer()
        {
            var randomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            Assert.That(randomizer, Is.InstanceOf<AnyAlignmentRandomizer>());
        }

        [Test]
        public void AnyAlignmentRandomzierIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
        }
    }
}