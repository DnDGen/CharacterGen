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
        private Dice dice;
        private IAdjustmentsSelector adjustmentsSelector;
        private ICollectionsSelector collectionsSelector;

        public HitPointsGenerator(Dice dice, IAdjustmentsSelector adjustmentsSelector, ICollectionsSelector collectionsSelector)
        {
            this.dice = dice;
            this.adjustmentsSelector = adjustmentsSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public int GenerateWith(CharacterClass characterClass, int constitutionBonus, Race race, IEnumerable<Feat> feats)
        {
            var hitPoints = GetClassHitPoints(characterClass, constitutionBonus, race);

            var toughness = feats.Where(f => f.Name == FeatConstants.Toughness);
            hitPoints += toughness.Sum(f => f.Power);

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace) == false)
                return hitPoints;

            var monsterHitPoints = GetAdditionalMonsterHitDice(race, constitutionBonus);
            return hitPoints + monsterHitPoints;
        }

        private int GetClassHitPoints(CharacterClass characterClass, int constitutionBonus, Race race)
        {
            var undead = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);
            if (undead.Contains(race.Metarace))
                return RollHitPoints(characterClass.Level, 12, constitutionBonus);

            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice);
            return RollHitPoints(characterClass.Level, hitDice[characterClass.ClassName], constitutionBonus);
        }

        private int GetAdditionalMonsterHitDice(Race race, int constitutionBonus)
        {
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return RollHitPoints(hitDice[race.BaseRace], 10, constitutionBonus);

            var undead = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);
            if (undead.Contains(race.Metarace))
                return RollHitPoints(hitDice[race.BaseRace], 12, constitutionBonus);

            return RollHitPoints(hitDice[race.BaseRace], 8, constitutionBonus);
        }

        private int RollHitPoints(int quantity, int die, int constitutionBonus)
        {
            var rolls = dice.Roll(quantity).IndividualRolls(die);
            rolls = rolls.Select(r => Math.Max(r + constitutionBonus, 1));

            return rolls.Sum();
        }
    }
}