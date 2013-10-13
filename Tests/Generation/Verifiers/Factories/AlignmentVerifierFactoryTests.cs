using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NPCGen.Core.Generation.Verifiers.Factories;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Factories
{
    [TestFixture]
    public class AlignmentVerifierFactoryTests
    {
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
        }

        [Test]
        public void ChaoticAlignmentRandomizerReturnsChaoticAlignmentVerifier()
        {
            var verifier = AlignmentVerifierFactory.CreateUsing(new ChaoticAlignmentRandomizer(mockDice.Object,
                mockPercentileResultProvider.Object));
            Assert.That(verifier, Is.TypeOf(typeof(ChaoticAlignmentVerifier)));
        }

        [Test]
        public void EvilAlignmentRandomizerReturnsEvilAlignmentVerifier()
        {
            var verifier = AlignmentVerifierFactory.CreateUsing(new EvilAlignmentRandomizer(mockDice.Object,
                mockPercentileResultProvider.Object));
            Assert.That(verifier, Is.TypeOf(typeof(EvilAlignmentVerifier)));
        }

        [Test]
        public void GoodAlignmentRandomizerReturnsGoodAlignmentVerifier()
        {
            var verifier = AlignmentVerifierFactory.CreateUsing(new GoodAlignmentRandomizer(mockDice.Object,
                mockPercentileResultProvider.Object));
            Assert.That(verifier, Is.TypeOf(typeof(GoodAlignmentVerifier)));
        }
    }
}