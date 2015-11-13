using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Combats
{
    public class HitPointsGenerator : IterativeGenerator, IHitPointsGenerator
    {
        private IDice dice;
        private IAdjustmentsSelector adjustmentsSelector;
        private ICollectionsSelector collectionsSelector;

        public HitPointsGenerator(IDice dice, IAdjustmentsSelector adjustmentsSelector, ICollectionsSelector collectionsSelector)
        {
            this.dice = dice;
            this.adjustmentsSelector = adjustmentsSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Int32 GenerateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race, IEnumerable<Feat> feats)
        {
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice);
            var hitPoints = RollHitPoints(characterClass.Level, hitDice[characterClass.ClassName], constitutionBonus);

            var toughness = feats.Where(f => f.Name == FeatConstants.Toughness);
            hitPoints += toughness.Sum(f => f.Strength);

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace) == false)
                return hitPoints;

            var monsterHitPoints = GetAdditionalMonsterHitDice(race, constitutionBonus);
            return hitPoints + monsterHitPoints;
        }

        private Int32 GetAdditionalMonsterHitDice(Race race, Int32 constitutionBonus)
        {
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return RollHitPoints(hitDice[race.BaseRace], 10, constitutionBonus);

            return RollHitPoints(hitDice[race.BaseRace], 8, constitutionBonus);
        }

        private Int32 RollHitPoints(Int32 quantity, Int32 die, Int32 constitutionBonus)
        {
            var hitPoints = dice.Roll(quantity).d(die);
            hitPoints += constitutionBonus * quantity;
            return Math.Max(hitPoints, quantity);
        }
    }
}