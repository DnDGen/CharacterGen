using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Domain.Combats
{
    public class CombatGenerator : ICombatGenerator
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

        private Int32 GetBaseAttackBonus(CharacterClass characterClass)
        {
            var goodBaseAttacks = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.GoodBaseAttack);
            if (goodBaseAttacks.Contains(characterClass.ClassName))
                return GetGoodBaseAttackBonus(characterClass.Level);

            var averageBaseAttacks = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.AverageBaseAttack);
            if (averageBaseAttacks.Contains(characterClass.ClassName))
                return GetAverageBaseAttackBonus(characterClass.Level);

            return GetPoorBaseAttackBonus(characterClass.Level);
        }

        private Int32 GetGoodBaseAttackBonus(Int32 level)
        {
            return level;
        }

        private Int32 GetAverageBaseAttackBonus(Int32 level)
        {
            return level * 3 / 4;
        }

        private Int32 GetPoorBaseAttackBonus(Int32 level)
        {
            return level / 2;
        }

        private Int32 GetRacialBaseAttackAdjustments(Race race)
        {
            var racialAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments);
            return racialAdjustments[race.BaseRace] + racialAdjustments[race.Metarace];
        }

        private Int32 GetSizeAdjustments(Race race)
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
            combat.AdjustedDexterityBonus = GetAdjustedDexterityBonus(stats, equipment);
            combat.ArmorClass = armorClassGenerator.GenerateWith(equipment, combat.AdjustedDexterityBonus, feats, race);
            combat.HitPoints = hitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race, feats);
            combat.SavingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            combat.InitiativeBonus = GetInitiativeBonus(combat.AdjustedDexterityBonus, feats);

            return combat;
        }

        private Int32 GetAdjustedDexterityBonus(Dictionary<String, Stat> stats, Equipment equipment)
        {
            if (equipment.Armor == null)
                return stats[StatConstants.Dexterity].Bonus;

            var maxDexterityBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MaxDexterityBonus);
            var armorBonus = maxDexterityBonuses[equipment.Armor.Name];

            if (equipment.Armor.Traits.Contains(TraitConstants.Mithral))
                armorBonus += 2;

            return Math.Min(stats[StatConstants.Dexterity].Bonus, armorBonus);
        }

        private Int32 GetInitiativeBonus(Int32 dexterityBonus, IEnumerable<Feat> feats)
        {
            var initiativeFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Initiative);
            var initiativeFeats = feats.Where(f => initiativeFeatNames.Contains(f.Name));
            var initiativeFeatBonus = initiativeFeats.Sum(f => f.Strength);

            return initiativeFeatBonus + dexterityBonus;
        }
    }
}