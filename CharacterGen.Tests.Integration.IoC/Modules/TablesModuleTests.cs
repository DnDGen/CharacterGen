using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class TablesModuleTests : IoCTests
    {
        [Test]
        public void StreamLoadersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<StreamLoader>();
        }
    }
}