using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public class OverriddenIntegrationTestTests : IntegrationTest
    {
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }

        protected override IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<SetClassNameRandomizer>();
        }

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<SetBaseRaceRandomizer>();
        }

        protected override IMetaraceRandomizer GetMetaraceRandomizer(IKernel kernel)
        {
            return kernel.Get<SetMetaraceRandomizer>();
        }

        [Test]
        public void OverriddenClassNameRandomizerIsAnyClassNameRandomizer()
        {
            Assert.That(ClassNameRandomizer, Is.InstanceOf<SetClassNameRandomizer>());
        }

        [Test]
        public void OverriddenBaseRaceRandomizerIsAnyBaseRaceRandomizer()
        {
            Assert.That(BaseRaceRandomizer, Is.InstanceOf<SetBaseRaceRandomizer>());
        }

        [Test]
        public void OverriddenMetaraceRandomizerIsAnyMetaraceRandomizer()
        {
            Assert.That(MetaraceRandomizer, Is.InstanceOf<SetMetaraceRandomizer>());
        }
    }
}