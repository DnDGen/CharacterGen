using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class RacialFeatsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "RacialFeats"; }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar,
            FeatConstants.Darkvision,
            FeatConstants.AasimarDaylight,
            FeatConstants.ResistanceToAcid,
            FeatConstants.ResistanceToCold,
            FeatConstants.ResistanceToElectricity)]
        [TestCase(RaceConstants.BaseRaces.Bugbear,
            FeatConstants.Darkvision,
            FeatConstants.Scent)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning,
            FeatConstants.WeaponFamiliarity,
            FeatConstants.Stability)] //have more dwarf feats to add regarding racial bonuses
        public override void DistinctCollection(String name, params String[] collection)
        {
            Assert.Fail();
            base.DistinctCollection(name, collection);
        }
    }
}