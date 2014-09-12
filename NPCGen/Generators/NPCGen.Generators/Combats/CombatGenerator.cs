using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Combats
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
            var goodBaseAttacks = collectionsSelector.SelectFrom("ClassNameGroups", "Good Base Attack");
            if (goodBaseAttacks.Contains(characterClass.ClassName))
                return GetGoodBaseAttackBonus(characterClass.Level);

            var averageBaseAttacks = collectionsSelector.SelectFrom("ClassNameGroups", "Average Base Attack");
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
            var racialAdjustments = adjustmentsSelector.SelectFrom("RacialBaseAttackAdjustments");
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
            combat.HitPoints = hitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race);
            combat.SavingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            combat.InitiativeBonus = GetInitiativeBonus(race, feats);

            return combat;
        }

        private Int32 GetAdjustedDexterityBonus(Dictionary<String, Stat> stats, Equipment equipment)
        {
            var maxDexterityBonuses = adjustmentsSelector.SelectFrom("MaxDexterityBonuses");
            var dexterityBonus = stats[StatConstants.Dexterity].Bonus;
            var maxArmorBonus = maxDexterityBonuses[equipment.Armor.Name];

            return Math.Min(dexterityBonus, maxArmorBonus);
        }

        private Int32 GetInitiativeBonus(Race race, IEnumerable<Feat> feats)
        {
            var racialBonuses = adjustmentsSelector.SelectFrom("RacialInitiativeBonuses");
            var featBonuses = adjustmentsSelector.SelectFrom("FeatInitiativeBonuses");

            var raceBonus = racialBonuses[race.BaseRace] + racialBonuses[race.Metarace];
            var relevantFeatBonuses = featBonuses.Where(kvp => feats.Any(f => f.Name == kvp.Key));
            var featBonus = relevantFeatBonuses.Sum(kvp => kvp.Value);

            return raceBonus + featBonus;
        }
    }
}