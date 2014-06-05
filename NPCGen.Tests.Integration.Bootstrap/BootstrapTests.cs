using D20Dice;
using Ninject;
using NPCGen.Bootstrap;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public class BootstrapTests
    {
        private IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = new StandardKernel();

            var npcGenLoader = new NPCGenModuleLoader();
            npcGenLoader.LoadModules(kernel);
        }

        [Test]
        public void AdjustmentXmlParsersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IAdjustmentXmlParser>();
            var second = kernel.Get<IAdjustmentXmlParser>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void AlignmentFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IAlignmentFactory>();
            var second = kernel.Get<IAlignmentFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void CharacterFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<ICharacterFactory>();
            var second = kernel.Get<ICharacterFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void CharacterClassFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<ICharacterClassFactory>();
            var second = kernel.Get<ICharacterClassFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void DiceAreGeneratedAsSingletons()
        {
            var first = kernel.Get<IDice>();
            var second = kernel.Get<IDice>();
            Assert.That(first, Is.EqualTo(second));
        }

        [Test]
        public void HitPointsFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IHitPointsFactory>();
            var second = kernel.Get<IHitPointsFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void LanguageFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<ILanguageFactory>();
            var second = kernel.Get<ILanguageFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void LanguageProvidersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<ILanguageProvider>();
            var second = kernel.Get<ILanguageProvider>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void LanguagesXmlParsersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<ILanguagesXmlParser>();
            var second = kernel.Get<ILanguagesXmlParser>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void LevelAdjustmentsProvidersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<ILevelAdjustmentsProvider>();
            var second = kernel.Get<ILevelAdjustmentsProvider>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void PercentileResultProvidersAreGeneratedAsSingletons()
        {
            var first = kernel.Get<IPercentileResultProvider>();
            var second = kernel.Get<IPercentileResultProvider>();
            Assert.That(first, Is.EqualTo(second));
        }

        [Test]
        public void PercentileXmlParsersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IPercentileXmlParser>();
            var second = kernel.Get<IPercentileXmlParser>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void RaceFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IRaceFactory>();
            var second = kernel.Get<IRaceFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void RandomizerVerifiersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IRandomizerVerifier>();
            var second = kernel.Get<IRandomizerVerifier>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void StatsFactoriesAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IStatsFactory>();
            var second = kernel.Get<IStatsFactory>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void StatPriorityProvidersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IStatPriorityProvider>();
            var second = kernel.Get<IStatPriorityProvider>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void StatPriorityXmlParsersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IStatPriorityXmlParser>();
            var second = kernel.Get<IStatPriorityXmlParser>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void StatAdjustmentsProvidersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IStatAdjustmentsProvider>();
            var second = kernel.Get<IStatAdjustmentsProvider>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        [Test]
        public void StreamLoadersAreNotGeneratedAsSingletons()
        {
            var first = kernel.Get<IStreamLoader>();
            var second = kernel.Get<IStreamLoader>();
            Assert.That(first, Is.Not.EqualTo(second));
        }
    }
}