using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class EvocationSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Evocation);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DancingLights,
                SpellConstants.Flare,
                SpellConstants.Light,
                SpellConstants.RayOfFrost,
                SpellConstants.BurningHands,
                SpellConstants.TensersFloatingDisk,
                SpellConstants.MagicMissile,
                SpellConstants.ShockingGrasp,
                SpellConstants.ContinualFlame,
                SpellConstants.Darkness,
                SpellConstants.FlamingSphere,
                SpellConstants.GustOfWind,
                SpellConstants.ScorchingRay,
                SpellConstants.Shatter,
                SpellConstants.Daylight,
                SpellConstants.Fireball,
                SpellConstants.LightningBolt,
                SpellConstants.LeomundsTinyHut,
                SpellConstants.WindWall,
                SpellConstants.FireShield,
                SpellConstants.IceStorm,
                SpellConstants.OtilukesResilientSphere,
                SpellConstants.Shout,
                SpellConstants.WallOfFire,
                SpellConstants.WallOfIce,
                SpellConstants.ChainLightning,
                SpellConstants.Contingency,
                SpellConstants.BigbysForcefulHand,
                SpellConstants.OtilukesFreezingSphere,
                SpellConstants.DelayedBlastFireball,
                SpellConstants.Forcecage,
                SpellConstants.BigbysGraspingHand,
                SpellConstants.MordenkainensSword,
                SpellConstants.PrismaticSpray,
                SpellConstants.BigbysClenchedFist,
                SpellConstants.PolarRay,
                SpellConstants.Shout_Greater,
                SpellConstants.Sunburst,
                SpellConstants.OtilukesTelekineticSphere,
                SpellConstants.BigbysCrushingHand,
                SpellConstants.ConeOfCold,
                SpellConstants.BigbysInterposingHand,
                SpellConstants.Sending,
                SpellConstants.WallOfForce,
                SpellConstants.MeteorSwarm
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllEvocationSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Evocation]);
        }

        [TestCase(SpellConstants.DancingLights, 0)]
        [TestCase(SpellConstants.Flare, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.RayOfFrost, 0)]
        [TestCase(SpellConstants.BurningHands, 1)]
        [TestCase(SpellConstants.TensersFloatingDisk, 1)]
        [TestCase(SpellConstants.MagicMissile, 1)]
        [TestCase(SpellConstants.ShockingGrasp, 1)]
        [TestCase(SpellConstants.ContinualFlame, 2)]
        [TestCase(SpellConstants.Darkness, 2)]
        [TestCase(SpellConstants.FlamingSphere, 2)]
        [TestCase(SpellConstants.GustOfWind, 2)]
        [TestCase(SpellConstants.ScorchingRay, 2)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.Fireball, 3)]
        [TestCase(SpellConstants.LightningBolt, 3)]
        [TestCase(SpellConstants.LeomundsTinyHut, 3)]
        [TestCase(SpellConstants.WindWall, 3)]
        [TestCase(SpellConstants.FireShield, 4)]
        [TestCase(SpellConstants.IceStorm, 4)]
        [TestCase(SpellConstants.OtilukesResilientSphere, 4)]
        [TestCase(SpellConstants.Shout, 4)]
        [TestCase(SpellConstants.WallOfFire, 4)]
        [TestCase(SpellConstants.WallOfIce, 4)]
        [TestCase(SpellConstants.ConeOfCold, 5)]
        [TestCase(SpellConstants.BigbysInterposingHand, 5)]
        [TestCase(SpellConstants.Sending, 5)]
        [TestCase(SpellConstants.WallOfForce, 5)]
        [TestCase(SpellConstants.ChainLightning, 6)]
        [TestCase(SpellConstants.Contingency, 6)]
        [TestCase(SpellConstants.BigbysForcefulHand, 6)]
        [TestCase(SpellConstants.OtilukesFreezingSphere, 6)]
        [TestCase(SpellConstants.DelayedBlastFireball, 7)]
        [TestCase(SpellConstants.Forcecage, 7)]
        [TestCase(SpellConstants.BigbysGraspingHand, 7)]
        [TestCase(SpellConstants.MordenkainensSword, 7)]
        [TestCase(SpellConstants.PrismaticSpray, 7)]
        [TestCase(SpellConstants.BigbysClenchedFist, 8)]
        [TestCase(SpellConstants.PolarRay, 8)]
        [TestCase(SpellConstants.Shout_Greater, 8)]
        [TestCase(SpellConstants.Sunburst, 8)]
        [TestCase(SpellConstants.OtilukesTelekineticSphere, 8)]
        [TestCase(SpellConstants.BigbysCrushingHand, 9)]
        [TestCase(SpellConstants.MeteorSwarm, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
