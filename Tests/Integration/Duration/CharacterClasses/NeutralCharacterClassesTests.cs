using System;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class NeutralCharacterClassesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralCharacterClasses"; }
        }

        [TestCase(CharacterClassConstants.Barbarian, 1, 5)]
        [TestCase(CharacterClassConstants.Bard, 6, 10)]
        [TestCase(CharacterClassConstants.Cleric, 11, 15)]
        [TestCase(CharacterClassConstants.Druid, 16, 25)]
        [TestCase(CharacterClassConstants.Fighter, 26, 45)]
        [TestCase(CharacterClassConstants.Monk, 46, 50)]
        [TestCase(CharacterClassConstants.Ranger, 51, 55)]
        [TestCase(CharacterClassConstants.Rogue, 56, 75)]
        [TestCase(CharacterClassConstants.Sorcerer, 76, 80)]
        [TestCase(CharacterClassConstants.Wizard, 81, 100)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }
    }
}