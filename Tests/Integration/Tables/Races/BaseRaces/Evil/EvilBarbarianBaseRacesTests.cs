using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.WildElf, 1)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 4)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 5)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 6)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 45)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 46)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 47)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 78)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 84)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElf, 2, 3)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 7, 29)]
        [TestCase(RaceConstants.BaseRaces.Human, 30, 39)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 40, 44)]
        [TestCase(RaceConstants.BaseRaces.Orc, 48, 77)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 79, 83)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 85, 86)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 87, 90)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 91, 94)]
        [TestCase(EmptyContent, 95, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}