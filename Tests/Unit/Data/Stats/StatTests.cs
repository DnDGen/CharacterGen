using System;
using NPCGen.Core.Data.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Data.Stats
{
    [TestFixture]
    public class StatTests
    {
        [Test]
        public void StatBonus()
        {
            var stat = new Stat();

            for (var v = 0; v < 30; v++)
            {
                stat.Value = v;
                var expectedBonus = GetExpectedBonus(v);
                Assert.That(stat.Bonus, Is.EqualTo(expectedBonus));
            }
        }

        private Int32 GetExpectedBonus(Int32 statValue)
        {
            return (statValue - 10) / 2;
        }
    }
}