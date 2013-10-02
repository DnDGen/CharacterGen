using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class ClassNameRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected IClassNameRandomizer randomizer;
        protected String controlCase;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            alignment = new Alignment();
        }

        protected void AssertClassIsAlwaysAllowed(String className)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };
            var lawfulnesses = new[] { AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.Lawful };

            foreach (var lawfulness in lawfulnesses)
            {
                foreach (var goodness in goodnesses)
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;
                    
                    AssertClassIsAllowed(className);
                }
            }
        }

        protected void AssertPaladinIsAllowed()
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };
            var lawfulnesses = new[] { AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.Lawful };

            foreach (var lawfulness in lawfulnesses)
            {
                foreach (var goodness in goodnesses)
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
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };
            var lawfulnesses = new[] { AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.Lawful };

            foreach (var lawfulness in lawfulnesses)
            {
                foreach (var goodness in goodnesses)
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
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };
            var lawfulnesses = new[] { AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.Lawful };

            foreach (var lawfulness in lawfulnesses)
            {
                foreach (var goodness in goodnesses)
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
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };
            var lawfulnesses = new[] { AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.Lawful };

            foreach (var lawfulness in lawfulnesses)
            {
                foreach (var goodness in goodnesses)
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
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };
            var lawfulnesses = new[] { AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.Lawful };

            foreach (var lawfulness in lawfulnesses)
            {
                foreach (var goodness in goodnesses)
                {
                    alignment.Lawfulness = lawfulness;
                    alignment.Goodness = goodness;

                    AssertClassIsNotAllowed(className);
                }
            }
        }

        protected void AssertControlIsAlwaysAllowed(String secondaryControl)
        {
            var storedControlCase = controlCase;
            controlCase = secondaryControl;

            AssertClassIsAlwaysAllowed(storedControlCase);

            controlCase = storedControlCase;
        }

        private void AssertClassIsAllowed(String className)
        {
            var result = GetResult(className, controlCase);
            Assert.That(result, Is.EqualTo(className));
        }

        private void AssertClassIsNotAllowed(String className)
        {
            var result = GetResult(className, controlCase);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        private String GetResult(String testCase, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(testCase)
                .Returns(controlCase);

            return randomizer.Randomize(alignment);
        }
    }
}