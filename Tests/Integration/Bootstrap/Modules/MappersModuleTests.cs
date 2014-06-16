using NPCGen.Mappers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class MappersModuleTests : BootstrapTests
    {
        [Test]
        public void AdjustmentXmlMappersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAdjustmentMapper>();
        }

        [Test]
        public void LanguagesXmlMappersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguagesMapper>();
        }

        [Test]
        public void PercentileXmlMappersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IPercentileMapper>();
        }

        [Test]
        public void StatPriorityXmlMappersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPriorityMapper>();
        }
    }
}