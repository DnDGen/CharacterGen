using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class TablesModuleTests : BootstrapTests
    {
        [Test]
        public void StreamLoadersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStreamLoader>();
        }
    }
}