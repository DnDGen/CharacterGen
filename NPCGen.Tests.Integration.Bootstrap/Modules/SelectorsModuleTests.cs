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
            AssertNotSingleton<ILanguagesSelector>();
        }

        [Test]
        public void LevelAdjustmentsProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILevelAdjustmentsSelector>();
        }

        [Test]
        public void PercentileResultProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IPercentileSelector>();
        }

        [Test]
        public void StatPriorityProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPrioritySelector>();
        }

        [Test]
        public void StatAdjustmentsProvidersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatAdjustmentsSelector>();
        }
    }
}