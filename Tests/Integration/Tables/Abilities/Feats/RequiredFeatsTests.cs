using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
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

        [TestCase(FeatConstants.ImprovedCombatStyleId, FeatConstants.CombatStyleId)]
        [TestCase(FeatConstants.CombatStyleMasteryId, FeatConstants.ImprovedCombatStyleId)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId, FeatConstants.MediumArmorProficiencyId)]
        [TestCase(FeatConstants.MediumArmorProficiencyId, FeatConstants.LightArmorProficiencyId)]
        public void RequiredFeat(String name, String requiredFeatId)
        {
            var collection = new[] { requiredFeatId };
            DistinctCollection(name, collection);
        }

        [TestCase(FeatConstants.AugmentSummoningId, FeatConstants.SpellFocusId, CharacterClassConstants.Schools.Conjuration)]
        public void RequiredFeat(String name, String requiredFeatId, String requiredFocus)
        {
            var collection = new[] { String.Format("{0}/{1}", requiredFeatId, requiredFocus) };
            DistinctCollection(name, collection);
        }
    }
}
