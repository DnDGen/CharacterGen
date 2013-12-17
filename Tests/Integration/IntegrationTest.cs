using Ninject;
using NPCGen.Bootstrap;
using NUnit.Framework;

namespace NPCGen.Tests.Integration
{
    [TestFixture]
    public class IntegrationTest
    {
        protected IKernel kernel;

        [SetUp]
        public void Setup()
        {
            InitializeKernel();
        }

        private void InitializeKernel()
        {
            kernel = new StandardKernel();
            var loader = new ModuleLoader();
            loader.LoadModules(kernel);
        }
    }
}