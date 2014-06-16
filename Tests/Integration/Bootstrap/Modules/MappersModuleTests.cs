using NPCGen.Mappers.Interfaces;
using NPCGen.Mappers.Percentiles;
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
            AssertNotSingleton<ICollectionsMapper>();
        }

        [Test]
        public void PercentileXmlMappersAreGeneratedAsSingletons()
        {
            AssertSingleton<IPercentileMapper>();
        }

        [Test]
        public void PercentileXmlMappersAreDecorated()
        {
            var mapper = GetNewInstanceOf<IPercentileMapper>();
            Assert.That(mapper, Is.InstanceOf<PercentileMapperCachingProxy>());
        }

        [Test]
        public void StatPriorityXmlMappersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPriorityMapper>();
        }
    }
}