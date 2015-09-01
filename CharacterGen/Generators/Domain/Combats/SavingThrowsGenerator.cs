using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Generators.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Combats
{
    public class SavingThrowsGenerator : Generator, ISavingThrowsGenerator
    {
        private ICollectionsSelector collectionsSelector;

        public SavingThrowsGenerator(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<String, Stat> stats)
        {
            var savingThrows = new SavingThrows();

            savingThrows.Fortitude = stats[StatConstants.Constitution].Bonus;
            savingThrows.Reflex = stats[StatConstants.Dexterity].Bonus;
            savingThrows.Will = stats[StatConstants.Wisdom].Bonus;

            savingThrows.Fortitude += GetClassSavingThrowBonus(characterClass, SavingThrowConstants.Fortitude);
            savingThrows.Reflex += GetClassSavingThrowBonus(characterClass, SavingThrowConstants.Reflex);
            savingThrows.Will += GetClassSavingThrowBonus(characterClass, SavingThrowConstants.Will);

            savingThrows.Fortitude += GetFeatSavingThrowBonus(feats, SavingThrowConstants.Fortitude);
            savingThrows.Reflex += GetFeatSavingThrowBonus(feats, SavingThrowConstants.Reflex);
            savingThrows.Will += GetFeatSavingThrowBonus(feats, SavingThrowConstants.Will);

            var anySavingThrowFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SavingThrows);
            var anySavingThrowFeats = feats.Where(f => anySavingThrowFeatNames.Contains(f.Name));

            foreach (var feat in anySavingThrowFeats)
            {
                if (feat.Focus == ProficiencyConstants.All || feat.Focus == SavingThrowConstants.Fortitude)
                    savingThrows.Fortitude += feat.Strength;

                if (feat.Focus == ProficiencyConstants.All || feat.Focus == SavingThrowConstants.Reflex)
                    savingThrows.Reflex += feat.Strength;

                if (feat.Focus == ProficiencyConstants.All || feat.Focus == SavingThrowConstants.Will)
                    savingThrows.Will += feat.Strength;
            }

            return savingThrows;
        }

        private Int32 GetClassSavingThrowBonus(CharacterClass characterClass, String savingThrow)
        {
            var strong = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, savingThrow);
            if (strong.Contains(characterClass.ClassName))
                return characterClass.Level / 2 + 2;

            return characterClass.Level / 3;
        }

        private Int32 GetFeatSavingThrowBonus(IEnumerable<Feat> feats, String savingThrow)
        {
            var saveFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, savingThrow);
            var saveFeats = feats.Where(f => saveFeatNames.Contains(f.Name));

            return saveFeats.Sum(f => f.Strength);
        }
    }
}