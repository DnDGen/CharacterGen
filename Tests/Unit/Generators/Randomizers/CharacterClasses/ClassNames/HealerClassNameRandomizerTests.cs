﻿using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class HealerClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<String> collectionClassNames
        {
            get
            {
                return new[]
                {
                    CharacterClassConstants.Bard,
                    CharacterClassConstants.Cleric,
                    CharacterClassConstants.Druid,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Ranger
                };
            }
        }

        protected override String classNameGroup
        {
            get { return "Healers"; }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new HealerClassNameRandomizer(mockPercentileResultSelector.Object, mockCollectionsSelector.Object);
        }

        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Ranger)]
        public void AlwaysAllowed(String className)
        {
            AssertClassIsAlwaysAllowed(className);
        }

        [TestCase(CharacterClassConstants.Bard)]
        public void AllowedIfNotLawful(String className)
        {
            AssertClassMustNotBeLawful(className);
        }

        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Wizard)]
        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Monk)]
        public void NeverAllowed(String className)
        {
            AssertClassIsNeverAllowed(className);
        }

        [Test]
        public void DruidAllowedIfAlignmentIsNeutral()
        {
            AssertDruidIsAllowed();
        }

        [Test]
        public void PaladinIsAllowedIfAlignmentIsLawfulGood()
        {
            AssertPaladinIsAllowed();
        }
    }
}