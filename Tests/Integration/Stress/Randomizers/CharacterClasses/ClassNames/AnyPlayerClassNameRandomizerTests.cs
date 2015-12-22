using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyPlayerClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyPlayer)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        protected override IEnumerable<string> allowedClassNames
        {
            get
            {
                return new[] {
                    CharacterClassConstants.Barbarian,
                    CharacterClassConstants.Bard,
                    CharacterClassConstants.Cleric,
                    CharacterClassConstants.Druid,
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Ranger,
                    CharacterClassConstants.Rogue,
                    CharacterClassConstants.Sorcerer,
                    CharacterClassConstants.Wizard
                };
            }
        }

        [TestCase("AnyPlayerClassNameRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}