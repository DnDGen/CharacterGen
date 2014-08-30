﻿using System;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class ClassNameRandomizerTests
    {
        protected Mock<IPercentileSelector> mockPercentileResultSelector;
        protected IClassNameRandomizer randomizer;

        private Alignment alignment;

        [SetUp]
        public void ClassNameRandomizerTestsSetup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(CharacterClassConstants.GetClassNames());
            alignment = new Alignment();
        }

        protected void AssertClassIsAlwaysAllowed(String className)
        {
            foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
            {
                foreach (var goodness in AlignmentConstants.GetGoodnesses())
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    AssertClassIsAllowed(className);
                }
            }
        }

        protected void AssertPaladinIsAllowed()
        {
            foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
            {
                foreach (var goodness in AlignmentConstants.GetGoodnesses())
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    if (alignment.IsLawful() && alignment.IsGood())
                        AssertClassIsAllowed(CharacterClassConstants.Paladin);
                    else
                        AssertClassIsNotAllowed(CharacterClassConstants.Paladin);
                }
            }
        }

        protected void AssertDruidIsAllowed()
        {
            foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
            {
                foreach (var goodness in AlignmentConstants.GetGoodnesses())
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    if (alignment.IsNeutral())
                        AssertClassIsAllowed(CharacterClassConstants.Druid);
                    else
                        AssertClassIsNotAllowed(CharacterClassConstants.Druid);
                }
            }
        }

        protected void AssertMonkIsAllowed()
        {
            foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
            {
                foreach (var goodness in AlignmentConstants.GetGoodnesses())
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    if (alignment.IsLawful())
                        AssertClassIsAllowed(CharacterClassConstants.Monk);
                    else
                        AssertClassIsNotAllowed(CharacterClassConstants.Monk);
                }
            }
        }

        protected void AssertClassMustNotBeLawful(String className)
        {
            foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
            {
                foreach (var goodness in AlignmentConstants.GetGoodnesses())
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    if (alignment.IsLawful())
                        AssertClassIsNotAllowed(className);
                    else
                        AssertClassIsAllowed(className);
                }
            }
        }

        protected void AssertClassIsNeverAllowed(String className)
        {
            foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
            {
                foreach (var goodness in AlignmentConstants.GetGoodnesses())
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    AssertClassIsNotAllowed(className);
                }
            }
        }

        private void AssertClassIsAllowed(String className)
        {
            var results = randomizer.GetAllPossibleResults(alignment);
            Assert.That(results, Contains.Item(className));
        }

        private void AssertClassIsNotAllowed(String className)
        {
            var results = randomizer.GetAllPossibleResults(alignment);
            Assert.That(results, Is.Not.Contains(className));
        }
    }
}