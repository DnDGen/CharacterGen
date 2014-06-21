using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class AlignmentGeneratorTests : StressTests
    {
        private IEnumerable<String> goodnesses;
        private IEnumerable<String> lawfulnesses;

        [SetUp]
        public void Setup()
        {
            goodnesses = AlignmentConstants.GetGoodnesses();
            lawfulnesses = AlignmentConstants.GetLawfulnesses();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentGenerator.GenerateWith(AlignmentRandomizer);
            Assert.That(goodnesses, Contains.Item(alignment.Goodness));
            Assert.That(lawfulnesses, Contains.Item(alignment.Lawfulness));
        }
    }
}