using System.Diagnostics;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration
{
    [TestFixture]
    public abstract class DurationTest : IntegrationTest
    {
        private Stopwatch stopwatch;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
        }

        protected void StartTest()
        {
            stopwatch.Start();
        }

        protected void AssertDuration()
        {
            Assert.Pass("Duration: {0}ms", stopwatch.ElapsedMilliseconds);
        }

        protected void StopTest()
        {
            stopwatch.Reset();
        }
    }
}