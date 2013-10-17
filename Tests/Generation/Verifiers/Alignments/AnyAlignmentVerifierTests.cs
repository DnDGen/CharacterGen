using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
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
        public void AllSetClassNamesAreAllowed()
        {
            var randomizer = new SetClassNameRandomizer();

            foreach (var className in CharacterClassConstants.GetClassNames())
            {
                randomizer.ClassName = className;
                AssertRandomizerIsAllowed(randomizer);
            }
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
        public void AllSetBaseRacesAreAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();

            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
            {
                randomizer.BaseRace = baseRace;
                AssertRandomizerIsAllowed(randomizer);
            }
        }

        [Test]
        public void AllowedAnyMetaraceRandomizerIsAllowed()
        {
            var randomizer = new AnyMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedAnyMetaraceRandomizerIsAllowed()
        {
            var randomizer = new AnyMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new EvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new EvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedGeneticMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GeneticMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedGeneticMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GeneticMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedLycanthropeMetaraceRandomizerIsAllowed()
        {
            var randomizer = new LycanthropeMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedLycanthropeMetaraceRandomizerIsAllowed()
        {
            var randomizer = new LycanthropeMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNonEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNonEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NoMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NoMetaraceRandomizer();
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNonGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonGoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNonGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonGoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNonNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNonNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllSetMetaracesAreAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();

            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
            {
                randomizer.Metarace = metarace;
                AssertRandomizerIsAllowed(randomizer);
            }
        }
    }
}