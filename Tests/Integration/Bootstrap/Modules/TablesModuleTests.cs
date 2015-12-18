using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class TablesModuleTests : BootstrapTests
    {
        [Test]
        public void StreamLoadersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<StreamLoader>();
        }
    }
}