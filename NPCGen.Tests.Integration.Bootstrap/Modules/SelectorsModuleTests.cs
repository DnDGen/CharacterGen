using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class SelectorsModuleTests : BootstrapTests
    {
        [Test]
        public void LanguageProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageProvider>();
        }

        [Test]
        public void LevelAdjustmentsProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILevelAdjustmentsProvider>();
        }

        [Test]
        public void PercentileResultProvidersAreGeneratedAsSingletons()
        {
            AssertNotSingleton<IPercentileResultProvider>();
        }

        [Test]
        public void StatPriorityProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPriorityProvider>();
        }

        [Test]
        public void StatAdjustmentsProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatAdjustmentsProvider>();
        }
    }
}