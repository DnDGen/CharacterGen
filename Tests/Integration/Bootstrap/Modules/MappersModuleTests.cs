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
            AssertSingleton<CollectionsMapper>();
        }

        [Test]
        public void CollectionsXmlMappersAreDecorated()
        {
            AssertIsInstanceOf<CollectionsMapper, CollectionsMapperCachingProxy>();
        }

        [Test]
        public void PercentileXmlMappersAreGeneratedAsSingletons()
        {
            AssertSingleton<PercentileMapper>();
        }

        [Test]
        public void PercentileXmlMappersAreDecorated()
        {
            AssertIsInstanceOf<PercentileMapper, PercentileMapperCachingProxy>();
        }
    }
}