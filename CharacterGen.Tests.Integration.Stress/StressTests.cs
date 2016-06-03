using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers;
using CharacterGen.Verifiers.Exceptions;
using Ninject;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    [Stress]
    public abstract class StressTests : IntegrationTests
    {
        [Inject]
        public Stopwatch Stopwatch { get; set; }
        [Inject]
        public IRandomizerVerifier RandomizerVerifier { get; set; }
        [Inject, Named(AlignmentRandomizerTypeConstants.Any)]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyPlayer)]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject, Named(LevelRandomizerTypeConstants.Any)]
        public ILevelRandomizer LevelRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.AnyBase)]
        public RaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.AnyMeta)]
        public RaceRandomizer MetaraceRandomizer { get; set; }

        private const int ConfidentIterations = 1000000;
        private const int TenMinutesInSeconds = 600;
        private const int TwoHoursInSeconds = 3600 * 2;

        private readonly int timeLimitInSeconds;

        private int iterations;

        public StressTests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var methods = types.SelectMany(t => t.GetMethods());
            var stressTestsCount = methods.Sum(m => m.GetCustomAttributes<TestAttribute>(true).Count());
            var stressTestCasesCount = methods.Sum(m => m.GetCustomAttributes<TestCaseAttribute>().Count());
            var stressTestsTotal = stressTestsCount + stressTestCasesCount;

            var twoHourTimeLimitPerTest = TwoHoursInSeconds / stressTestsTotal;
#if STRESS
            timeLimitInSeconds = Math.Min(twoHourTimeLimitPerTest, TenMinutesInSeconds - 10);
#else
            timeLimitInSeconds = 1;
#endif
        }

        [SetUp]
        public void StressSetup()
        {
            iterations = 0;
            Stopwatch.Start();
        }

        [TearDown]
        public void StressTearDown()
        {
            Stopwatch.Reset();
        }

        public abstract void Stress(string stressSubject);

        protected void Stress()
        {
            Stress(MakeAssertions);
        }

        protected void Stress(Action makeAssertions)
        {
            do makeAssertions();
            while (TestShouldKeepRunning());

            Console.WriteLine($"Stress test complete after {Stopwatch.Elapsed} and {iterations} iterations");

            if (Stopwatch.Elapsed.TotalSeconds > timeLimitInSeconds + 5)
                Assert.Fail("Something took way too long");
        }

        protected abstract void MakeAssertions();

        protected T Generate<T>(Func<T> generate, Func<T, bool> isValid)
        {
            T generatedObject;

            do generatedObject = generate();
            while (isValid(generatedObject) == false);

            return generatedObject;
        }

        protected T GenerateOrFail<T>(Func<T> generate, Func<T, bool> isValid)
        {
            T generatedObject;

            do generatedObject = generate();
            while (TestShouldKeepRunning() && isValid(generatedObject) == false);

            Console.WriteLine($"Generation complete after {Stopwatch.Elapsed} and {iterations} iterations");

            if (TestShouldKeepRunning() == false && isValid(generatedObject) == false)
                Assert.Fail("Stress test timed out.");

            return generatedObject;
        }

        private bool TestShouldKeepRunning()
        {
            iterations++;
            return Stopwatch.Elapsed.TotalSeconds < timeLimitInSeconds && iterations < ConfidentIterations;
        }

        protected Alignment GetNewAlignment()
        {
            var compatible = RandomizerVerifier.VerifyCompatibility(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer);

            if (compatible == false)
                throw new IncompatibleRandomizersException();

            var alignment = Generate(
                () => AlignmentRandomizer.Randomize(),
                a => RandomizerVerifier.VerifyAlignmentCompatibility(a, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer));

            return alignment;
        }

        protected CharacterClass GetNewCharacterClass(Alignment alignment)
        {
            var characterClass = Generate(
                () => GenerateCharacterClass(alignment),
                c => RandomizerVerifier.VerifyCharacterClassCompatibility(alignment, c, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer));

            return characterClass;
        }

        private CharacterClass GenerateCharacterClass(Alignment alignment)
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassNameRandomizer.Randomize(alignment);
            characterClass.Level = LevelRandomizer.Randomize();

            return characterClass;
        }
    }
}