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
    public class HitPointsGenerator : IHitPointsGenerator
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
            var hitPoints = 0;
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice);

            for (var i = 0; i < characterClass.Level; i++)
            {
                var rolledHitPoints = dice.Roll().d(hitDice[characterClass.ClassName]) + constitutionBonus;
                hitPoints += Math.Max(rolledHitPoints, 1);
            }

            var toughness = feats.Where(f => f.Name == FeatConstants.Toughness);
            hitPoints += toughness.Sum(f => f.Strength);

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups,
                GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace) == false)
                return hitPoints;

            var monsterHitPoints = GetAdditionalMonsterHitDice(race);
            return hitPoints + monsterHitPoints;
        }

        private Int32 GetAdditionalMonsterHitDice(Race race)
        {
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return dice.Roll(hitDice[race.BaseRace]).d10();

            return dice.Roll(hitDice[race.BaseRace]).d8();
        }
    }
}