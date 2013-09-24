using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NPCGen.Core.Generation.Verifiers.Factories;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Factories
{
    [TestFixture]
    public class AlignmentVerifierFactoryTests
    {
        private IAlignmentVerifierFactory factory;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            factory = new AlignmentVerifierFactory();
            mockDice = new Mock<IDice>();
        }

        [Test]
        public void ChaoticAlignmentRandomizerReturnsChaoticAlignmentVerifier()
        {
            var verifier = factory.Create(new ChaoticAlignmentRandomizer(mockDice.Object));
            Assert.That(verifier, Is.TypeOf(typeof(ChaoticAlignmentVerifier)));
        }

        [Test]
        public void EvilAlignmentRandomizerReturnsEvilAlignmentVerifier()
        {
            var verifier = factory.Create(new EvilAlignmentRandomizer(mockDice.Object));
            Assert.That(verifier, Is.TypeOf(typeof(EvilAlignmentVerifier)));
        }

        [Test]
        public void GoodAlignmentRandomizerReturnsGoodAlignmentVerifier()
        {
            var verifier = factory.Create(new GoodAlignmentRandomizer(mockDice.Object));
            Assert.That(verifier, Is.TypeOf(typeof(GoodAlignmentVerifier)));
        }
    }
}