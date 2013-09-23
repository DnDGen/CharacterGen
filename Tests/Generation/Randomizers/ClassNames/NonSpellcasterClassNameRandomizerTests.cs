﻿using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            controlCase = CharacterClassConstants.Fighter;
        }

        [Test]
        public void FighterAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void ClericNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void RangerNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Wizard);
        }

        [Test]
        public void BarbarianNotAllowedIfAlignmentIsLawful()
        {
            AssertClassMustNotBeLawful(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void MonkNotAllowedIfAlignmentIsChaotic()
        {
            AssertMonkIsAllowed();
        }

        [Test]
        public void PaladinNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Paladin);
        }
    }
}