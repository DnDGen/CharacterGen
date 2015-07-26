using System;
using Ninject;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTests
    {
        [Inject]
        public ISetClassNameRandomizer SetClassNameRandomizer { get; set; }

        [TestCase("SetClassNameRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            SetClassNameRandomizer.SetClassName = Guid.NewGuid().ToString();
            var alignment = GetNewAlignment();

            var className = SetClassNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(SetClassNameRandomizer.SetClassName));
        }
    }
}