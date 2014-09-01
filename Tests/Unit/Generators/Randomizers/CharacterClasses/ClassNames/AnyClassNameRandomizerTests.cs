using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<String> collectionClassNames
        {
            get { return Enumerable.Empty<String>(); }
        }

        protected override String classNameGroup
        {
            get { return String.Empty; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyClassNameRandomizer(mockPercentileResultSelector.Object);
        }

        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void AlwaysAllowed(String className)
        {
            AssertClassIsAlwaysAllowed(className);
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        public void AllowedIfNotLawful(String className)
        {
            AssertClassMustNotBeLawful(className);
        }

        [Test]
        public void DruidIsAllowedIfAlignmentIsNeutral()
        {
            AssertDruidIsAllowed();
        }

        [Test]
        public void MonkIsAllowedIfAlignmentIsLawful()
        {
            AssertMonkIsAllowed();
        }

        [Test]
        public void PaladinIsAllowedIfAlignmentIsLawfulGood()
        {
            AssertPaladinIsAllowed();
        }
    }
}