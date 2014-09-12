using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [Inject, Named(ClassNameRandomizerTypeConstants.NonSpellcaster)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        protected override IEnumerable<String> allowedClassNames
        {
            get
            {
                return new[]
                {
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Rogue,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Barbarian
                };
            }
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }
    }
}