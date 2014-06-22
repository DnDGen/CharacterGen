using System;
using System.Diagnostics;
using System.Linq;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public abstract class StressTests : IntegrationTests
    {
        [Inject]
        public Stopwatch Stopwatch { get; set; }
        [Inject]
        public IAlignmentGenerator AlignmentGenerator { get; set; }
        [Inject]
        public IRandomizerVerifier RandomizerVerifier { get; set; }
        [Inject, Named(AlignmentRandomizerTypeConstants.Any)]
        public virtual IAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Any)]
        public virtual IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject, Named(LevelRandomizerTypeConstants.Any)]
        public ILevelRandomizer LevelRandomizer { get; set; }
        [Inject, Named(BaseRaceRandomizerTypeConstants.Any)]
        public virtual IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject, Named(MetaraceRandomizerTypeConstants.Any)]
        public virtual IMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public ICharacterClassGenerator CharacterClassGenerator { get; set; }
        [Inject]
        public IRaceGenerator RaceGenerator { get; set; }

        protected readonly String testType;

        private const Int32 ConfidentIterations = 1000000;
        private const Int32 TimeLimitInSeconds = 1;

        private Int32 iterations;

        public StressTests()
        {
            var classType = GetType().ToString();
            var segments = classType.Split('.');
            testType = segments.Last();
        }

        [SetUp]
        public void StressSetup()
        {
            iterations = 0;
            Stopwatch.Start();
        }

        [TearDown]
        protected void StressTearDown()
        {
            Stopwatch.Reset();
        }

        [Test]
        public void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        protected abstract void MakeAssertions();

        protected Boolean TestShouldKeepRunning()
        {
            iterations++;
            return Stopwatch.Elapsed.TotalSeconds < TimeLimitInSeconds && iterations < ConfidentIterations;
        }

        protected void AssertIterations()
        {
            Assert.That(iterations, Is.GreaterThan(0));
            Assert.Pass("Type: {0}\nIterations: {1}\nTime: {2:hh\\:mm\\:ss}", testType, iterations, Stopwatch.Elapsed);
        }

        protected Alignment GetNewAlignment()
        {
            var alignment = new Alignment();

            do alignment = AlignmentGenerator.GenerateWith(AlignmentRandomizer);
            while (!RandomizerVerifier.VerifyAlignmentCompatibility(alignment, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer));

            return alignment;
        }

        protected CharacterClassPrototype GetNewCharacterClassPrototype(Alignment alignment)
        {
            var prototype = new CharacterClassPrototype();

            do prototype = CharacterClassGenerator.GeneratePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
            while (!RandomizerVerifier.VerifyCharacterClassCompatibility(alignment.Goodness, prototype,
                BaseRaceRandomizer, MetaraceRandomizer));

            return prototype;
        }

        protected CharacterClass GetNewCharacterClass(CharacterClassPrototype prototype)
        {
            return CharacterClassGenerator.GenerateWith(prototype);
        }

        protected Race GetNewRace(Alignment alignment, CharacterClassPrototype prototype)
        {
            return RaceGenerator.GenerateWith(alignment.Goodness, prototype, BaseRaceRandomizer, MetaraceRandomizer);
        }
    }
}