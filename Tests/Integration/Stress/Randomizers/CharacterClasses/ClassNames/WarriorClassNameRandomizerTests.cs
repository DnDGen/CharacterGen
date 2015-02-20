using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class WarriorClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [Inject, Named(ClassNameRandomizerTypeConstants.Warrior)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        protected override IEnumerable<String> allowedClassNames
        {
            get
            {
                return new[]
                {
                    CharacterClassConstants.Barbarian,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Ranger
                };
            }
        }

        [TestCase("WarriorClassNameRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}