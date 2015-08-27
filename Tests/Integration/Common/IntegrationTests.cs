using CharacterGen.Bootstrap;
using Ninject;
using NUnit.Framework;
using RollGen.Bootstrap;
using System;
using TreasureGen.Bootstrap;

namespace CharacterGen.Tests.Integration.Common
{
    [TestFixture]
    public abstract class IntegrationTests
    {
        private IKernel kernel;

        [TestFixtureSetUp]
        public void IntegrationTestsFixtureSetup()
        {
            kernel = new StandardKernel();

            var diceLoader = new RollGenModuleLoader();
            diceLoader.LoadModules(kernel);

            var treasureGenLoader = new TreasureGenModuleLoader();
            treasureGenLoader.LoadModules(kernel);

            var characterGenLoader = new CharacterGenModuleLoader();
            characterGenLoader.LoadModules(kernel);
        }

        [SetUp]
        public void IntegrationTestsSetup()
        {
            kernel.Inject(this);
        }

        protected T GetNewInstanceOf<T>()
        {
            return kernel.Get<T>();
        }

        protected T GetNewInstanceOf<T>(String name)
        {
            return kernel.Get<T>(name);
        }
    }
}