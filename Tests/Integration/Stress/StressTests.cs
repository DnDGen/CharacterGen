using System;
using System.Diagnostics;
using Ninject;
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

        private const Int32 ConfidentIterations = 1000000;
        private const Int32 TimeLimitInSeconds = 1;

        private Int32 iterations;

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
            Assert.Pass("Iterations: {0}\nTime: {1:hh\\:mm\\:ss}", iterations, Stopwatch.Elapsed);
        }

        protected DependentDataCollection GetNewDependentData()
        {
            var collection = new DependentDataCollection();

            do collection.Alignment = AlignmentGenerator.CreateWith(AlignmentRandomizer);
            while (!RandomizerVerifier.VerifyAlignmentCompatibility(collection.Alignment, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                MetaraceRandomizer));

            do collection.CharacterClassPrototype = CharacterClassGenerator.CreatePrototypeWith(collection.Alignment, LevelRandomizer,
                ClassNameRandomizer);
            while (!RandomizerVerifier.VerifyCharacterClassCompatibility(collection.Alignment.Goodness, collection.CharacterClassPrototype,
                BaseRaceRandomizer, MetaraceRandomizer));

            collection.CharacterClass = CharacterClassGenerator.CreateWith(collection.CharacterClassPrototype);
            collection.Race = RaceGenerator.CreateWith(collection.Alignment.Goodness, collection.CharacterClassPrototype, BaseRaceRandomizer,
                MetaraceRandomizer);

            return collection;
        }
    }
}