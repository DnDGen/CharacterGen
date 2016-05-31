using CharacterGen.Domain.IoC;
using Ninject;
using NUnit.Framework;
using RollGen.Bootstrap;
using TreasureGen.Bootstrap;

namespace CharacterGen.Tests.Integration
{
    [TestFixture]
    public abstract class IntegrationTests
    {
        private IKernel kernel;

        [OneTimeSetUp]
        public void IntegrationTestsFixtureSetup()
        {
            kernel = new StandardKernel(new NinjectSettings() { InjectNonPublic = true });

            var rollGenLoader = new RollGenModuleLoader();
            rollGenLoader.LoadModules(kernel);

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

        protected T GetNewInstanceOf<T>(string name)
        {
            return kernel.Get<T>(name);
        }
    }
}