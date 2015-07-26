using System;
using RollGen.Bootstrap;
using TreasureGen.Bootstrap;
using Ninject;
using CharacterGen.Bootstrap;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Common
{
    [TestFixture]
    [Integration]
    public abstract class IntegrationTests
    {
        private IKernel kernel;

        [TestFixtureSetUp]
        public void IntegrationTestsFixtureSetup()
        {
            kernel = new StandardKernel();

            var diceLoader = new RollGenModuleLoader();
            diceLoader.LoadModules(kernel);

            var TreasureGenLoader = new TreasureGenModuleLoader();
            TreasureGenLoader.LoadModules(kernel);

            var npcGenLoader = new CharacterGenModuleLoader();
            npcGenLoader.LoadModules(kernel);
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