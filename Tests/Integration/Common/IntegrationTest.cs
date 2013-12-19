using Ninject;
using NPCGen.Bootstrap;
using NPCGen.Tests.Integration.Common.Modules;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public class IntegrationTest
    {
        [SetUp]
        public void Setup()
        {
            var kernel = new StandardKernel();

            var npcGenLoader = new NPCGenModuleLoader();
            npcGenLoader.LoadModules(kernel);

            var testLoader = new IntegrationTestModuleLoader();
            testLoader.LoadModules(kernel);

            kernel.Inject(this);
        }
    }
}