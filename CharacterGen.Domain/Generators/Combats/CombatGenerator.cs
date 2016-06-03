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

        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race)
        {
            var baseAttack = new BaseAttack();
            baseAttack.Bonus = GetBaseAttackBonus(characterClass);
            baseAttack.Bonus += GetRacialBaseAttackAdjustments(race);
            baseAttack.Bonus += GetSizeAdjustments(race);

            return baseAttack;
        }

        private int GetBaseAttackBonus(CharacterClass characterClass)
        {
            var goodBaseAttacks = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.GoodBaseAttack);
            if (goodBaseAttacks.Contains(characterClass.Name))
                return GetGoodBaseAttackBonus(characterClass.Level);

            var averageBaseAttacks = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.AverageBaseAttack);
            if (averageBaseAttacks.Contains(characterClass.Name))
                return GetAverageBaseAttackBonus(characterClass.Level);

            var poorBaseAttacks = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.PoorBaseAttack);
            if (poorBaseAttacks.Contains(characterClass.Name))
                return GetPoorBaseAttackBonus(characterClass.Level);

            var message = string.Format("{0} has no base attack", characterClass.Name);
            throw new ArgumentException(message);
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
            var racialAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments);
            return racialAdjustments[race.BaseRace] + racialAdjustments[race.Metarace];
        }

        private int GetSizeAdjustments(Race race)
        {
            if (race.Size == RaceConstants.Sizes.Large)
                return -1;
            else if (race.Size == RaceConstants.Sizes.Small)
                return 1;

            return 0;
        }

        public Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<String, Stat> stats, Equipment equipment)
        {
            var combat = new Combat();

            combat.BaseAttack = baseAttack;
            combat.BaseAttack.Bonus += GetBonusFromFeats(feats);
            combat.BaseAttack.CircumstantialBonus = IsAttackBonusCircumstantial(feats);
            combat.AdjustedDexterityBonus = GetAdjustedDexterityBonus(stats, equipment);
            combat.ArmorClass = armorClassGenerator.GenerateWith(equipment, combat.AdjustedDexterityBonus, feats, race);
            combat.HitPoints = hitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race, feats);
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

        private int GetAdjustedDexterityBonus(Dictionary<string, Stat> stats, Equipment equipment)
        {
            if (equipment.Armor == null)
                return stats[StatConstants.Dexterity].Bonus;

            var maxDexterityBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MaxDexterityBonus);
            var armorBonus = maxDexterityBonuses[equipment.Armor.Name];

            if (equipment.Armor.Traits.Contains(TraitConstants.Mithral))
                armorBonus += 2;

            return Math.Min(stats[StatConstants.Dexterity].Bonus, armorBonus);
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