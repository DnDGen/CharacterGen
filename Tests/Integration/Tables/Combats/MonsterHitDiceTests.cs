using System;
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
                RaceConstants.BaseRaces.BugbearId,
                RaceConstants.BaseRaces.DerroId, 
                RaceConstants.BaseRaces.DoppelgangerId,
                RaceConstants.BaseRaces.GnollId, 
                RaceConstants.BaseRaces.LizardfolkId, 
                RaceConstants.BaseRaces.MindFlayerId,
                RaceConstants.BaseRaces.MinotaurId, 
                RaceConstants.BaseRaces.OgreId,
                RaceConstants.BaseRaces.OgreMageId,
                RaceConstants.BaseRaces.TroglodyteId
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.BugbearId, 3)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 3)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 4)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 2)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 8)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 6)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 5)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}