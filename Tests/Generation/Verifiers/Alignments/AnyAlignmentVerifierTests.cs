using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Alignments
{
    [TestFixture]
    public class AnyAlignmentVerifierTests : AlignmentVerifierTests
    {
        [SetUp]
        public void Setup()
        {
            verifier = new AnyAlignmentVerifier();
        }

        [Test]
        public void AnyClassNameRandomizerIsAllowed()
        {
            var randomizer = new AnyClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HealerClassRandomizerIsAllowed()
        {
            var randomizer = new HealerClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MageClassRandomizerIsAllowed()
        {
            var randomizer = new MageClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonSpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new SpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void StealthClassRandomizerIsAllowed()
        {
            var randomizer = new StealthClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WarriorClassRandomizerIsAllowed()
        {
            var randomizer = new WarriorClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SetClassNameRandomizerIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AnyBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new AnyBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void EvilBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new EvilBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GoodBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new GoodBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NeutralBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NeutralBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonEvilBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonEvilBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonGoodBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonGoodBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonNeutralBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonNeutralBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonStandardBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonStandardBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void StandardBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new StandardBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SetBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AnyMetaraceRandomizerIsAllowed()
        {
            var randomizer = new AnyMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void EvilMetaraceRandomizerIsallowed()
        {
            var randomizer = new EvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GeneticMetaraceRandomizerIsallowed()
        {
            var randomizer = new GeneticMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GoodMetaraceRandomizerIsallowed()
        {
            var randomizer = new GoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void LycanthropeMetaraceRandomizerIsallowed()
        {
            var randomizer = new LycanthropeMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NeutralMetaraceRandomizerIsallowed()
        {
            var randomizer = new NeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonEvilMetaraceRandomizerIsallowed()
        {
            var randomizer = new NonEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NoMetaraceRandomizerIsallowed()
        {
            var randomizer = new NoMetaraceRandomizer();
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonGoodMetaraceRandomizerIsallowed()
        {
            var randomizer = new NonGoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonNeutralMetaraceRandomizerIsallowed()
        {
            var randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SetMetaraceRandomizerIsallowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            AssertRandomizerIsAllowed(randomizer);
        }
    }
}