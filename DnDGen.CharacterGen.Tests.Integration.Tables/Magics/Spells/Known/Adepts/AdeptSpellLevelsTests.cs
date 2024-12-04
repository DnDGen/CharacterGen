using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Adepts
{
    [TestFixture]
    public class AdeptSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Adept);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CreateWater,
                SpellConstants.CureMinorWounds,
                SpellConstants.DetectMagic,
                SpellConstants.GhostSound,
                SpellConstants.Guidance,
                SpellConstants.Light,
                SpellConstants.Mending,
                SpellConstants.PurifyFoodAndDrink,
                SpellConstants.ReadMagic,
                SpellConstants.TouchOfFatigue,
                SpellConstants.Bless,
                SpellConstants.BurningHands,
                SpellConstants.CauseFear,
                SpellConstants.Command,
                SpellConstants.ComprehendLanguages,
                SpellConstants.CureLightWounds,
                SpellConstants.DetectChaos,
                SpellConstants.DetectEvil,
                SpellConstants.DetectGood,
                SpellConstants.DetectLaw,
                SpellConstants.EndureElements,
                SpellConstants.ObscuringMist,
                SpellConstants.ProtectionFromChaos,
                SpellConstants.ProtectionFromEvil,
                SpellConstants.ProtectionFromGood,
                SpellConstants.ProtectionFromLaw,
                SpellConstants.Sleep,
                SpellConstants.Aid,
                SpellConstants.AnimalTrance,
                SpellConstants.BearsEndurance,
                SpellConstants.BullsStrength,
                SpellConstants.CatsGrace,
                SpellConstants.CureModerateWounds,
                SpellConstants.Darkness,
                SpellConstants.DelayPoison,
                SpellConstants.Invisibility,
                SpellConstants.MirrorImage,
                SpellConstants.ResistEnergy,
                SpellConstants.ScorchingRay,
                SpellConstants.SeeInvisibility,
                SpellConstants.Web,
                SpellConstants.AnimateDead,
                SpellConstants.BestowCurse,
                SpellConstants.Contagion,
                SpellConstants.ContinualFlame,
                SpellConstants.CureSeriousWounds,
                SpellConstants.Daylight,
                SpellConstants.DeeperDarkness,
                SpellConstants.LightningBolt,
                SpellConstants.NeutralizePoison,
                SpellConstants.RemoveCurse,
                SpellConstants.RemoveDisease,
                SpellConstants.Tongues,
                SpellConstants.CureCriticalWounds,
                SpellConstants.MinorCreation,
                SpellConstants.Polymorph,
                SpellConstants.Restoration,
                SpellConstants.Stoneskin,
                SpellConstants.WallOfFire,
                SpellConstants.BalefulPolymorph,
                SpellConstants.BreakEnchantment,
                SpellConstants.Commune,
                SpellConstants.Heal,
                SpellConstants.MajorCreation,
                SpellConstants.RaiseDead,
                SpellConstants.TrueSeeing,
                SpellConstants.WallOfStone
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllAdeptSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Adept]);
        }

        [TestCase(SpellConstants.CreateWater, 0)]
        [TestCase(SpellConstants.CureMinorWounds, 0)]
        [TestCase(SpellConstants.DetectMagic, 0)]
        [TestCase(SpellConstants.GhostSound, 0)]
        [TestCase(SpellConstants.Guidance, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.Mending, 0)]
        [TestCase(SpellConstants.PurifyFoodAndDrink, 0)]
        [TestCase(SpellConstants.ReadMagic, 0)]
        [TestCase(SpellConstants.TouchOfFatigue, 0)]
        [TestCase(SpellConstants.Bless, 1)]
        [TestCase(SpellConstants.BurningHands, 1)]
        [TestCase(SpellConstants.CauseFear, 1)]
        [TestCase(SpellConstants.Command, 1)]
        [TestCase(SpellConstants.ComprehendLanguages, 1)]
        [TestCase(SpellConstants.CureLightWounds, 1)]
        [TestCase(SpellConstants.DetectChaos, 1)]
        [TestCase(SpellConstants.DetectEvil, 1)]
        [TestCase(SpellConstants.DetectGood, 1)]
        [TestCase(SpellConstants.DetectLaw, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.ProtectionFromChaos, 1)]
        [TestCase(SpellConstants.ProtectionFromEvil, 1)]
        [TestCase(SpellConstants.ProtectionFromGood, 1)]
        [TestCase(SpellConstants.ProtectionFromLaw, 1)]
        [TestCase(SpellConstants.Sleep, 1)]
        [TestCase(SpellConstants.Aid, 2)]
        [TestCase(SpellConstants.AnimalTrance, 2)]
        [TestCase(SpellConstants.BearsEndurance, 2)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.CatsGrace, 2)]
        [TestCase(SpellConstants.CureModerateWounds, 2)]
        [TestCase(SpellConstants.Darkness, 2)]
        [TestCase(SpellConstants.DelayPoison, 2)]
        [TestCase(SpellConstants.Invisibility, 2)]
        [TestCase(SpellConstants.MirrorImage, 2)]
        [TestCase(SpellConstants.ResistEnergy, 2)]
        [TestCase(SpellConstants.ScorchingRay, 2)]
        [TestCase(SpellConstants.SeeInvisibility, 2)]
        [TestCase(SpellConstants.Web, 2)]
        [TestCase(SpellConstants.AnimateDead, 3)]
        [TestCase(SpellConstants.BestowCurse, 3)]
        [TestCase(SpellConstants.Contagion, 3)]
        [TestCase(SpellConstants.ContinualFlame, 3)]
        [TestCase(SpellConstants.CureSeriousWounds, 3)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.DeeperDarkness, 3)]
        [TestCase(SpellConstants.LightningBolt, 3)]
        [TestCase(SpellConstants.NeutralizePoison, 3)]
        [TestCase(SpellConstants.RemoveCurse, 3)]
        [TestCase(SpellConstants.RemoveDisease, 3)]
        [TestCase(SpellConstants.Tongues, 3)]
        [TestCase(SpellConstants.CureCriticalWounds, 4)]
        [TestCase(SpellConstants.MinorCreation, 4)]
        [TestCase(SpellConstants.Polymorph, 4)]
        [TestCase(SpellConstants.Restoration, 4)]
        [TestCase(SpellConstants.Stoneskin, 4)]
        [TestCase(SpellConstants.WallOfFire, 4)]
        [TestCase(SpellConstants.BalefulPolymorph, 5)]
        [TestCase(SpellConstants.BreakEnchantment, 5)]
        [TestCase(SpellConstants.Commune, 5)]
        [TestCase(SpellConstants.Heal, 5)]
        [TestCase(SpellConstants.MajorCreation, 5)]
        [TestCase(SpellConstants.RaiseDead, 5)]
        [TestCase(SpellConstants.TrueSeeing, 5)]
        [TestCase(SpellConstants.WallOfStone, 5)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
