using NPCGen.Mappers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class MappersModuleTests : BootstrapTests
    {
        [Test]
        public void AdjustmentXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAdjustmentXmlParser>();
        }

        [Test]
        public void LanguagesXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguagesXmlParser>();
        }

        [Test]
        public void PercentileXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IPercentileXmlParser>();
        }

        [Test]
        public void StatPriorityXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPriorityXmlParser>();
        }
    }
}