using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class WarriorClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<String> classNamesInGroup
        {
            get 
            {
                return new[]
                {
                    CharacterClassConstants.Barbarian,
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Ranger
                };
            }
        }

        protected override String classNameGroup
        {
            get { return "Warriors"; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new WarriorClassNameRandomizer(mockPercentileResultSelector.Object, mockCollectionsSelector.Object);
        }

        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Ranger)]
        public void AlwaysAllowed(String className)
        {
            AssertClassIsAlwaysAllowed(className);
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        public void AllowedIfNotLawful(String className)
        {
            AssertClassMustNotBeLawful(className);
        }

        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Druid)]
        public void NeverAllowed(String className)
        {
            AssertClassIsNeverAllowed(className);
        }

        [Test]
        public void MonkAllowedIfAlignmentIsLawful()
        {
            AssertMonkIsAllowed();
        }

        [Test]
        public void PaladinAllowedIfAlignmentIsLawfulGood()
        {
            AssertPaladinIsAllowed();
        }
    }
}