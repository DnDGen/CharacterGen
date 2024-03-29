﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWarriorBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Warrior); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(11, 29, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(30, 34, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(35, 35, RaceConstants.BaseRaces.HighElf)]
        [TestCase(36, 41, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(42, 46, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(47, 47, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 48, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(49, 58, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 100, RaceConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}