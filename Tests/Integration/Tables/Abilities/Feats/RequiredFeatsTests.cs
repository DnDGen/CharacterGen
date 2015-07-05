using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class RequiredFeatsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.RequiredFeats; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.ImprovedCombatStyleId,
                FeatConstants.CombatStyleMasteryId,
                FeatConstants.HeavyArmorProficiencyId,
                FeatConstants.MediumArmorProficiencyId,
                FeatConstants.AugmentSummoningId
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.ImprovedCombatStyleId,
            FeatConstants.CombatStyleId)]
        [TestCase(FeatConstants.CombatStyleMasteryId,
            FeatConstants.ImprovedCombatStyleId)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId)]
        [TestCase(FeatConstants.MediumArmorProficiencyId,
            FeatConstants.LightArmorProficiencyId)]
        [TestCase(FeatConstants.AugmentSummoningId,
            FeatConstants.SpellFocusId)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            throw new NotImplementedException("Have to figure out focus requirements, augment requires conjuration focus");

            base.DistinctCollection(name, collection);
        }
    }
}
