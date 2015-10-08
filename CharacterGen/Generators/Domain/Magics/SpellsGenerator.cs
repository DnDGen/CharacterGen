using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Magics;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Magics
{
    public class SpellsGenerator : ISpellsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public SpellsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        //public Dictionary<Int32, IEnumerable<String>> GenerateFrom(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment)
        //{
        //    throw new NotImplementedException();
        //}

        public Dictionary<Int32, Int32> GenerateFrom(CharacterClass characterClass, Dictionary<String, Stat> stats)
        {
            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
            var spellQuantities = new Dictionary<Int32, Int32>();

            if (spellcasters.Contains(characterClass.ClassName) == false)
                return spellQuantities;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellQuantities, characterClass.Level, characterClass.ClassName);
            var spellsForClass = adjustmentsSelector.SelectFrom(tableName);

            var spellStat = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.ClassName + GroupConstants.Spellcasters).Single();
            var maxSpellLevel = stats[spellStat].Value - 10;

            foreach (var kvp in spellsForClass)
            {
                var spellLevel = Convert.ToInt32(kvp.Key);
                if (spellLevel > maxSpellLevel)
                    continue;

                spellQuantities[spellLevel] = kvp.Value;

                if (spellLevel == 0 || stats[spellStat].Bonus - spellLevel < 0)
                    continue;

                var bonusSpells = (stats[spellStat].Bonus - spellLevel) / 4 + 1;
                spellQuantities[spellLevel] += bonusSpells;
            }

            return spellQuantities;
        }
    }
}
