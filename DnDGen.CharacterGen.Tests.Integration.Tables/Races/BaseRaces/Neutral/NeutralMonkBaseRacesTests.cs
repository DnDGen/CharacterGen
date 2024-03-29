﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralMonkBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.HighElf)]
        [TestCase(3, 3, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(4, 13, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(14, 14, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(15, 15, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(16, 25, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 95, RaceConstants.BaseRaces.Human)]
        [TestCase(96, 97, RaceConstants.BaseRaces.Githzerai)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Azer)]
        [TestCase(99, 99, RaceConstants.BaseRaces.Janni)]
        [TestCase(100, 100, RaceConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}