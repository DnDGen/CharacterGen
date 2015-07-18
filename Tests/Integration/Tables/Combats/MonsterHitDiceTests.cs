﻿using System;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class MonsterHitDiceTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.MonsterHitDice; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.Derro, 
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Gnoll, 
                RaceConstants.BaseRaces.Lizardfolk, 
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur, 
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Troglodyte
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Bugbear, 3)]
        [TestCase(RaceConstants.BaseRaces.Derro, 3)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 6)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 5)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}