using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Combats
{
    internal class CombatGenerator : ICombatGenerator
    {
        private IArmorClassGenerator armorClassGenerator;
        private IHitPointsGenerator hitPointsGenerator;
        private ISavingThrowsGenerator savingThrowsGenerator;
        private IAdjustmentsSelector adjustmentsSelector;
        private ICollectionsSelector collectionsSelector;

        public CombatGenerator(IArmorClassGenerator armorClassGenerator, IHitPointsGenerator hitPointsGenerator, ISavingThrowsGenerator savingThrowsGenerator,
            IAdjustmentsSelector adjustmentsSelector, ICollectionsSelector collectionsSelector)
        {
            this.armorClassGenerator = armorClassGenerator;
            this.hitPointsGenerator = hitPointsGenerator;
            this.savingThrowsGenerator = savingThrowsGenerator;
            this.adjustmentsSelector = adjustmentsSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats)
        {
            var baseAttack = new BaseAttack();
            baseAttack.BaseBonus = GetBaseAttackBonus(characterClass);
            baseAttack.SizeModifier = GetSizeAdjustments(race);
            baseAttack.StrengthBonus = stats[StatConstants.Strength].Bonus;
            baseAttack.DexterityBonus = stats[StatConstants.Dexterity].Bonus;
            baseAttack.RacialModifier = GetRacialBaseAttackAdjustments(race);

            return baseAttack;
        }

        private int GetBaseAttackBonus(CharacterClass characterClass)
        {
            var baseAttackQuality = collectionsSelector.FindGroupOf(TableNameConstants.Set.Collection.ClassNameGroups, characterClass.Name, GroupConstants.GoodBaseAttack, GroupConstants.AverageBaseAttack, GroupConstants.PoorBaseAttack);

            switch (baseAttackQuality)
            {
                case GroupConstants.GoodBaseAttack: return GetGoodBaseAttackBonus(characterClass.Level);
                case GroupConstants.AverageBaseAttack: return GetAverageBaseAttackBonus(characterClass.Level);
                case GroupConstants.PoorBaseAttack: return GetPoorBaseAttackBonus(characterClass.Level);
                default: throw new ArgumentException($"{characterClass.Name} has no base attack");
            }
        }

        private int GetGoodBaseAttackBonus(int level)
        {
            return level;
        }

        private int GetAverageBaseAttackBonus(int level)
        {
            return level * 3 / 4;
        }

        private int GetPoorBaseAttackBonus(int level)
        {
            return level / 2;
        }

        private int GetRacialBaseAttackAdjustments(Race race)
        {
            var baseRaceAdjustment = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments, race.BaseRace);
            var metaraceAdjustment = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments, race.Metarace);

            return baseRaceAdjustment + metaraceAdjustment;
        }

        private int GetSizeAdjustments(Race race)
        {
            var sizeModifier = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SizeModifiers, race.Size);
            return sizeModifier;
        }

        public Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<string, Stat> stats, Equipment equipment)
        {
            var combat = new Combat();

            combat.BaseAttack = baseAttack;
            combat.BaseAttack.BaseBonus += GetBonusFromFeats(feats);
            combat.BaseAttack.CircumstantialBonus = IsAttackBonusCircumstantial(feats);
            combat.AdjustedDexterityBonus = GetAdjustedDexterityBonus(stats[StatConstants.Dexterity], equipment);
            combat.ArmorClass = armorClassGenerator.GenerateWith(equipment, combat.AdjustedDexterityBonus, feats, race);

            if (stats.ContainsKey(StatConstants.Constitution))
                combat.HitPoints = hitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race, feats);
            else
                combat.HitPoints = hitPointsGenerator.GenerateWith(characterClass, 0, race, feats);

            combat.SavingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            combat.InitiativeBonus = GetInitiativeBonus(combat.AdjustedDexterityBonus, feats);

            return combat;
        }

        private int GetBonusFromFeats(IEnumerable<Feat> feats)
        {
            var attackBonusFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.AttackBonus);
            var attackBonusFeats = feats.Where(f => attackBonusFeatNames.Contains(f.Name) && f.Foci.Any() == false);

            if (attackBonusFeats.Any() == false)
                return 0;

            return attackBonusFeats.Sum(f => f.Power);
        }

        private bool IsAttackBonusCircumstantial(IEnumerable<Feat> feats)
        {
            var attackBonusFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.AttackBonus);
            var attackBonusFeats = feats.Where(f => attackBonusFeatNames.Contains(f.Name));

            return attackBonusFeats.Any(f => f.Foci.Any());
        }

        private int GetAdjustedDexterityBonus(Stat dexterity, Equipment equipment)
        {
            if (equipment.Armor == null)
                return dexterity.Bonus;

            var maxDexterityBonus = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MaxDexterityBonus, equipment.Armor.Name);

            if (equipment.Armor.Traits.Contains(TraitConstants.SpecialMaterials.Mithral))
                maxDexterityBonus += 2;

            return Math.Min(dexterity.Bonus, maxDexterityBonus);
        }

        private int GetInitiativeBonus(int dexterityBonus, IEnumerable<Feat> feats)
        {
            var initiativeFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Initiative);
            var initiativeFeats = feats.Where(f => initiativeFeatNames.Contains(f.Name));
            var initiativeFeatBonus = initiativeFeats.Sum(f => f.Power);

            return initiativeFeatBonus + dexterityBonus;
        }
    }
}