using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer AnyNPCClassNameRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;
        }

        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            MetaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);

        }

        [Test]
        public void StressMetarace()
        {
            Stress(AssertMetarace);
        }

        protected void AssertMetarace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            SetMetaraceRandomizer.SetMetarace = MetaraceRandomizer.Randomize(alignment, characterClass);

            var metarace = SetMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }

        [Test]
        public void StressNPCSetMetarace()
        {
            ClassNameRandomizer = AnyNPCClassNameRandomizer;
            Stress(AssertNPCSetMetarace);
        }

        private void AssertNPCSetMetarace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            SetMetaraceRandomizer.SetMetarace = MetaraceRandomizer.Randomize(alignment, characterClass);

            var metarace = SetMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}