using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class RockGnomeFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.RockGnome); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
            FeatConstants.LowLightVision,
            FeatConstants.WeaponFamiliarity,
            FeatConstants.SaveBonus,
            FeatConstants.ImprovedSpell,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus,
            FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
            FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation
            };

            AssertCollectionNames(names);
        }
        
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Prestidigitation,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement);
        }
    }
}
