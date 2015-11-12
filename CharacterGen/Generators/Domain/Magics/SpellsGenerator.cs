using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Magics;
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

        public IEnumerable<Spells> GenerateFrom(CharacterClass characterClass, Dictionary<String, Stat> stats)
        {
            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
            var spellsPerDay = new List<Spells>();

            if (spellcasters.Contains(characterClass.ClassName) == false)
                return spellsPerDay;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, characterClass.Level, characterClass.ClassName);
            var spellsForClass = adjustmentsSelector.SelectFrom(tableName);

            var spellStat = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.ClassName + GroupConstants.Spellcasters).Single();
            var maxSpellLevel = stats[spellStat].Value - 10;
            var spellsForCharacter = spellsForClass.Where(kvp => Convert.ToInt32(kvp.Key) <= maxSpellLevel);

            foreach (var kvp in spellsForCharacter)
            {
                var spells = new Spells();
                spells.Level = Convert.ToInt32(kvp.Key);
                spells.Quantity = kvp.Value;
                spells.HasDomainSpell = characterClass.SpecialistFields.Any() && spells.Level > 0;

                spellsPerDay.Add(spells);

                if (spells.Level == 0 || spells.Level > stats[spellStat].Bonus)
                    continue;

                var bonusSpells = (stats[spellStat].Bonus - spells.Level) / 4 + 1;
                spells.Quantity += bonusSpells;
            }

            return spellsPerDay.Where(s => s.Quantity > 0 || s.HasDomainSpell);
        }
    }
}
