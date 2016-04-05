using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyNPCClassNameRandomizerTests : ClassNameRandomizerTests
    {
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

        [SetUp]
        public void Setup()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
        }

        [TestCase("Any NPC Class Name Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}
