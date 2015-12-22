using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyNPCClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public override IClassNameRandomizer ClassNameRandomizer { get; set; }

        protected override IEnumerable<string> allowedClassNames
        {
            get
            {
                return new[] {
                    CharacterClassConstants.Adept,
                    CharacterClassConstants.Aristocrat,
                    CharacterClassConstants.Commoner,
                    CharacterClassConstants.Expert,
                    CharacterClassConstants.Warrior
                };
            }
        }

        [TestCase("AnyNPCClassNameRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}
