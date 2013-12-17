using Ninject;
using NPCGen.Bootstrap;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Integration
{
    [TestFixture]
    public abstract class IntegrationTest
    {
        private IKernel kernel;

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

        protected void Inject(Object instance)
        {
            kernel.Inject(instance);
        }
    }
}