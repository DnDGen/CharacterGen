using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Mappers.Percentiles;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class MappersModuleTests : IoCTests
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