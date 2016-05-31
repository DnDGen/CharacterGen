using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.WildElf)]
        [TestCase(2, 3, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(4, 4, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(5, 5, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(6, 6, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(7, 29, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(30, 39, RaceConstants.BaseRaces.Human)]
        [TestCase(40, 44, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(45, 45, RaceConstants.BaseRaces.Goblin)]
        [TestCase(46, 46, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(47, 47, RaceConstants.BaseRaces.Kobold)]
        [TestCase(48, 77, RaceConstants.BaseRaces.Orc)]
        [TestCase(78, 78, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(79, 83, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(84, 84, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(85, 86, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(87, 90, RaceConstants.BaseRaces.Ogre)]
        [TestCase(91, 94, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(95, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}