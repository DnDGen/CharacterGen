using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers;
using CharacterGen.Verifiers.Exceptions;
using EventGen;
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
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }
        [Inject]
        public GenEventQueue EventQueue { get; set; }

        private const int ConfidentIterations = 1000000;
        private const int TravisJobOutputTimeLimit = 60 * 10;
        private const int TravisJobBuildTimeLimit = 60 * 50;

        private readonly int timeLimitInSeconds;

        private int iterations;
        private Guid clientId;
        private DateTime eventCheckpoint;

        public StressTests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var methods = types.SelectMany(t => t.GetMethods());
            var stressTestsCount = methods.Sum(m => m.GetCustomAttributes<TestAttribute>(true).Count());
            var stressTestCasesCount = methods.Sum(m => m.GetCustomAttributes<TestCaseAttribute>().Count());
            var stressTestsTotal = stressTestsCount + stressTestCasesCount;

            var timeLimitPerTest = TravisJobBuildTimeLimit / stressTestsTotal - 15;
            Assert.That(timeLimitPerTest, Is.AtLeast(10));
#if STRESS
            timeLimitInSeconds = Math.Min(timeLimitPerTest, TravisJobOutputTimeLimit - 10);
#else
            timeLimitInSeconds = 1;
#endif
        }

        [SetUp]
        public void StressSetup()
        {
            clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);

            iterations = 0;
            eventCheckpoint = new DateTime();

            Stopwatch.Start();
        }

        [TearDown]
        public void StressTearDown()
        {
            WriteStressSummary();
            WriteEventSummary();

            Stopwatch.Reset();
        }

        private void AssertEventSpacing()
        {
            var events = EventQueue.DequeueAll(clientId);

            //INFO: Have to put the events back in the queue for the summary at the end of the test
            foreach (var genEvent in events)
                EventQueue.Enqueue(genEvent);

            Assert.That(events, Is.Ordered.By("When"));

            var newEvents = events.Where(e => e.When > eventCheckpoint).ToArray();

            Assert.That(newEvents, Is.Ordered.By("When"));

            for (var i = 1; i < newEvents.Length; i++)
            {
                var failureMessage = $"{GetMessage(newEvents[i - 1])}\n{GetMessage(newEvents[i])}";
                Assert.That(newEvents[i].When, Is.EqualTo(newEvents[i - 1].When).Within(1).Seconds, failureMessage);
            }

            if (newEvents.Any())
                eventCheckpoint = newEvents.Last().When;
        }

        private void WriteStressSummary()
        {
            var iterationsPerSecond = Math.Round(iterations / Stopwatch.Elapsed.TotalSeconds, 2);
            Console.WriteLine($"Stress test complete after {Stopwatch.Elapsed} and {iterations} iterations, or {iterationsPerSecond} iterations per second");
        }

        private void WriteEventSummary()
        {
            var events = EventQueue.DequeueAll(clientId);

            //INFO: Get the 10 most recent events for CharacterGen.  We assume the events are ordered chronologically already
            events = events.Where(e => e.Source == "CharacterGen");
            events = events.Reverse();
            events = events.Take(10);
            events = events.Reverse();

            foreach (var genEvent in events)
                Console.WriteLine(GetMessage(genEvent));
        }

        private string GetMessage(GenEvent genEvent)
        {
            return $"[{genEvent.When.ToLongTimeString()}] {genEvent.Source}: {genEvent.Message}";
        }

        protected void Stress(Action generateAndMakeAssertions)
        {
            do
            {
                generateAndMakeAssertions();
                AssertEventSpacing();
            }
            while (TestShouldKeepRunning());

        }

        protected T Generate<T>(Func<T> generate, Func<T, bool> isValid)
        {
            T generatedObject;

            do
            {
                generatedObject = generate();
                AssertEventSpacing();
            }
            while (isValid(generatedObject) == false);

            return generatedObject;
        }

        protected T GenerateOrFail<T>(Func<T> generate, Func<T, bool> isValid)
        {
            T generatedObject;

            do
            {
                generatedObject = generate();
                AssertEventSpacing();
            }
            while (TestShouldKeepRunning() && isValid(generatedObject) == false);


            if (TestShouldKeepRunning() == false && isValid(generatedObject) == false)
                Assert.Fail($"Generation timed out after {Stopwatch.Elapsed} and {iterations} iterations");

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