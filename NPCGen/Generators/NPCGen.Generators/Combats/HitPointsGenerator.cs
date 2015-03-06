using System;
using System.Linq;
using D20Dice;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Combats
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

        public Int32 GenerateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race)
        {
            var hitPoints = 0;
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice);

            for (var i = 0; i < characterClass.Level; i++)
            {
                var rolledHitPoints = dice.Roll().d(hitDice[characterClass.ClassName]) + constitutionBonus;
                hitPoints += Math.Max(rolledHitPoints, 1);
            }

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups,
                TableNameConstants.Set.Collection.Groups.Monsters);
            if (!monsters.Contains(race.BaseRace.Id))
                return hitPoints;

            var monsterHitPoints = GetAdditionalMonsterHitDice(race);
            return hitPoints + monsterHitPoints;
        }

        private Int32 GetAdditionalMonsterHitDice(Race race)
        {
            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);

            if (race.Metarace.Id == RaceConstants.Metaraces.HalfDragonId)
                return dice.Roll(hitDice[race.BaseRace.Id]).d10();

            return dice.Roll(hitDice[race.BaseRace.Id]).d8();
        }
    }
}