using NPCGen.Mappers.Collections;
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
        public void CollectionsXmlMappersAreGeneratedAsSingletons()
        {
            AssertSingleton<ICollectionsMapper>();
        }

        [Test]
        public void CollectionsXmlMappersAreDecorated()
        {
            AssertIsInstanceOf<ICollectionsMapper, CollectionsMapperCachingProxy>();
        }

        [Test]
        public void PercentileXmlMappersAreGeneratedAsSingletons()
        {
            AssertSingleton<IPercentileMapper>();
        }

        [Test]
        public void PercentileXmlMappersAreDecorated()
        {
            AssertIsInstanceOf<IPercentileMapper, PercentileMapperCachingProxy>();
        }

        [Test]
        public void StatPriorityXmlMappersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPriorityMapper>();
        }
    }
}