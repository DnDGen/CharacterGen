using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.WildElf)]
        [TestCase(2, 16, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(17, 21, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(22, 22, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(23, 23, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(24, 28, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(29, 69, RaceConstants.BaseRaces.Human)]
        [TestCase(70, 71, RaceConstants.BaseRaces.Rakshasa)]
        [TestCase(72, 72, RaceConstants.BaseRaces.FrostGiant)]
        [TestCase(73, 73, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(74, 74, RaceConstants.BaseRaces.Goblin)]
        [TestCase(75, 75, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(76, 83, RaceConstants.BaseRaces.Kobold)]
        [TestCase(84, 84, RaceConstants.BaseRaces.DeathSlaad)]
        [TestCase(85, 85, RaceConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(86, 86, RaceConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(87, 87, RaceConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(88, 88, RaceConstants.BaseRaces.Scorpionfolk)]
        [TestCase(89, 89, RaceConstants.BaseRaces.Harpy)]
        [TestCase(90, 90, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(92, 94, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Ogre)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(98, 98, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, RaceConstants.BaseRaces.FireGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}