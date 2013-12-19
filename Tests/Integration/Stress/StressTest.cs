using System;
using System.Diagnostics;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public abstract class StressTest : IntegrationTest
    {
        private const Int32 ConfidentIterations = 1000000;
        private const Int32 TimeLimitInSeconds = 60;

        private Stopwatch stopwatch;
        private Int32 iterations;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
        }

        protected void StartTest()
        {
            stopwatch.Start();
            iterations = 0;
        }

        protected void StopTest()
        {
            stopwatch.Reset();
        }

        protected Boolean TestShouldKeepRunning()
        {
            return stopwatch.Elapsed.Seconds < TimeLimitInSeconds && iterations++ < ConfidentIterations;
        }
    }
}