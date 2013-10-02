using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Alignments
{
    [TestFixture]
    public class ChaoticAlignmentVerifierTests : AlignmentVerifierTests
    {
        [SetUp]
        public void Setup()
        {
            verifier = new ChaoticAlignmentVerifier();
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
        public void BarbarianIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void ClericIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void DruidIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void FighterIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void MonkIsNotAllowed()
        {
            AssertClassNameIsNotAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void PaladinIsNotAllowed()
        {
            AssertClassNameIsNotAllowed(CharacterClassConstants.Paladin);
        }

        [Test]
        public void RangerIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void RogueIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void SorcererIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void WizardIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Wizard);
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
    }
}