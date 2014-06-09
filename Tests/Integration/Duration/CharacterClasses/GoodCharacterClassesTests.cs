using System;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.CharacterClasses
{
    [TestFixture]
    public class GoodCharacterClassesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodCharacterClasses"; }
        }

        [TestCase(CharacterClassConstants.Barbarian, 1, 5)]
        [TestCase(CharacterClassConstants.Bard, 6, 10)]
        [TestCase(CharacterClassConstants.Cleric, 11, 30)]
        [TestCase(CharacterClassConstants.Druid, 31, 35)]
        [TestCase(CharacterClassConstants.Fighter, 36, 45)]
        [TestCase(CharacterClassConstants.Monk, 46, 50)]
        [TestCase(CharacterClassConstants.Paladin, 51, 55)]
        [TestCase(CharacterClassConstants.Ranger, 56, 65)]
        [TestCase(CharacterClassConstants.Rogue, 66, 75)]
        [TestCase(CharacterClassConstants.Sorcerer, 76, 80)]
        [TestCase(CharacterClassConstants.Wizard, 81, 100)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }
    }
}