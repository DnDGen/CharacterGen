using System.IO;
using NPCGen.Core.Generation.Xml.Parsers;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Parsers
{
    [TestFixture]
    public class EmbeddedResourceStreamLoaderTests
    {
        private IStreamLoader streamLoader;

        [SetUp]
        public void Setup()
        {
            streamLoader = new EmbeddedResourceStreamLoader();
        }

        [Test]
        public void GetsFileIfIsAnEmbeddeResource()
        {
            using(var stream = streamLoader.LoadStream("GoodCharacterClasses"))
            {
                Assert.Pass();
            }
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void ThrowErrorIfFileIsNotEmbeddedResource()
        {
            using (var stream = streamLoader.LoadStream("GoodCharacterClasses"))
            {
                Assert.Pass();
            }
        }
    }
}