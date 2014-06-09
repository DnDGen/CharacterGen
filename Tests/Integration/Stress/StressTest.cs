using System;
using System.Diagnostics;
using Ninject;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public abstract class StressTest : IntegrationTest
    {
        [Inject]
        public Stopwatch Stopwatch { get; set; }

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

        protected Boolean TestShouldKeepRunning()
        {
            iterations++;
            return Stopwatch.Elapsed.TotalSeconds < TimeLimitInSeconds && iterations < ConfidentIterations;
        }

        protected void AssertIterations()
        {
            Assert.That(iterations, Is.GreaterThan(0));
            Assert.Pass("Iterations: {0}\nTimes: {1:hh.\\mm.\\ss}", iterations, Stopwatch.Elapsed);
        }
    }
}