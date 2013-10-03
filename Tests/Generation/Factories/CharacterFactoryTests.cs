using System;
using System.Collections.Generic;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        private IAlignmentRandomizer alignmentRandomizer;
        private IClassNameRandomizer classNameRandomizer;
        private ILevelRandomizer levelRandomizer;
        private IBaseRaceRandomizer baseRaceRandomizer;
        private IMetaraceRandomizer metaraceRandomizer;
        private IStatsRandomizer statsRandomizer;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();

            alignmentRandomizer = new AnyAlignmentRandomizer(mockDice.Object);
            classNameRandomizer = new AnyClassNameRandomizer(mockPercentileResultProvider.Object);
            levelRandomizer = new AnyLevelRandomizer(mockDice.Object);
            baseRaceRandomizer = new AnyBaseRaceRandomizer(mockPercentileResultProvider.Object);
            metaraceRandomizer = new AllowedAnyMetaraceRandomizer(mockPercentileResultProvider.Object);
            statsRandomizer = new RawStatsRandomizer(mockDice.Object);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            alignmentRandomizer = new GoodAlignmentRandomizer(mockDice.Object);
            metaraceRandomizer = new ForcedEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            
            CreateCharacter();
        }

        [Test]
        public void GeneratesStatsFromStatsFactory()
        {
            var stats = StatsFactory.CreateUsing(statsRandomizer);
            
            var character = CreateCharacter();
            Assert.That(character.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void GeneratesAlignmentFromAlignmentFactory()
        {
            var alignment = AlignmentFactory.CreateUsing(alignmentRandomizer);
            
            var character = CreateCharacter();
            Assert.That(character.Alignment, Is.EqualTo(alignment));
        }

        [Test]
        public void GeneratesCharacterClassFromCharacterClassFactory()
        {
            var alignment = AlignmentFactory.CreateUsing(alignmentRandomizer);
            var characterClass = CharacterClassFactory.CreateUsing(alignment, levelRandomizer, classNameRandomizer);
            
            var character = CreateCharacter();
            Assert.That(character.Class, Is.EqualTo(characterClass));
        }

        [Test]
        public void GeneratesRaceFromRaceFactory()
        {
            var alignment = AlignmentFactory.CreateUsing(alignmentRandomizer);
            var characterClass = CharacterClassFactory.CreateUsing(alignment, levelRandomizer, classNameRandomizer);
            var race = RaceFactory.CreateUsing(alignment.GetGoodnessString(), characterClass.ClassName, baseRaceRandomizer, metaraceRandomizer);
            
            var character = CreateCharacter();
            Assert.That(character.Race, Is.EqualTo(race));
        }

        [Test]
        public void AlignmentIncompatibleWithCharacterClassIsRegenerated()
        {
            classNameRandomizer = new SetClassNameRandomizer() { ClassName = CharacterClassConstants.Paladin };
            alignmentRandomizer = new EvilAlignmentRandomizer(mockDice.Object);

            mockCharacterClassVerifier.SetupSequence(v => v.VerifyCompatibility(alignment)).Returns(false).Returns(true);
            characterFactory.Generate();
            mockAlignmentFactory.Verify(f => f.Generate(), Times.Exactly(2));
        }

        [Test]
        public void AlignmentIncompatibleWithBaseRaceIsRegenerated()
        {
            mockBaseRaceVerifier.SetupSequence(v => v.VerifyCompatibility(alignment)).Returns(false).Returns(true);
            characterFactory.Generate();
            mockAlignmentFactory.Verify(f => f.Generate(), Times.Exactly(2));
        }

        [Test]
        public void AlignmentIncompatibleWithMetaraceIsRegnerated()
        {
            
        }

        private Character CreateCharacter()
        {
            return CharacterFactory.CreateUsing(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, 
                metaraceRandomizer, statsRandomizer);
        }
    }
}