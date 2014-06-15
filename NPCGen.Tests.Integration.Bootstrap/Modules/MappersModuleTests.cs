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
            AssertNotSingleton<IAdjustmentMapper>();
        }

        [Test]
        public void LanguagesXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguagesMapper>();
        }

        [Test]
        public void PercentileXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IPercentileMapper>();
        }

        [Test]
        public void StatPriorityXmlParsersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPriorityMapper>();
        }
    }
}