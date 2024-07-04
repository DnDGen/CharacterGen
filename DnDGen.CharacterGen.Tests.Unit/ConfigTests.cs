using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Unit
{
    internal class ConfigTests
    {
        [Test]
        public void ConfigNameIsCorrect()
        {
            var configType = typeof(Config);
            Assert.That(Config.Name, Is.EqualTo("DnDGen.CharacterGen").And.EqualTo(configType.Namespace));
        }
    }
}
