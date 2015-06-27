using System;
using D20Dice.Bootstrap;
using EquipmentGen.Bootstrap;
using Ninject;
using NPCGen.Bootstrap;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
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

            var diceLoader = new D20DiceModuleLoader();
            diceLoader.LoadModules(kernel);

            var equipmentGenLoader = new EquipmentGenModuleLoader();
            equipmentGenLoader.LoadModules(kernel);

            var npcGenLoader = new NPCGenModuleLoader();
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