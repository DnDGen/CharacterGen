using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Collections;
using CharacterGen.Mappers.Domain.Percentiles;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class MappersModuleTests : BootstrapTests
    {
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
    }
}