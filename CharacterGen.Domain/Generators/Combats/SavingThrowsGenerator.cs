using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Combats
{
    internal class SavingThrowsGenerator : ISavingThrowsGenerator
    {
        private ICollectionsSelector collectionsSelector;

        public SavingThrowsGenerator(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public SavingThrows GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<string, Stat> stats)
        {
            var savingThrows = new SavingThrows();

            savingThrows.HasFortitudeSave = stats.ContainsKey(StatConstants.Constitution);

            savingThrows.Reflex = stats[StatConstants.Dexterity].Bonus;
            savingThrows.Will = stats[StatConstants.Wisdom].Bonus;

            savingThrows.Reflex += GetClassSavingThrowBonus(characterClass, SavingThrowConstants.Reflex);
            savingThrows.Will += GetClassSavingThrowBonus(characterClass, SavingThrowConstants.Will);

            savingThrows.Reflex += GetFeatSavingThrowBonus(feats, SavingThrowConstants.Reflex);
            savingThrows.Will += GetFeatSavingThrowBonus(feats, SavingThrowConstants.Will);

            var anySavingThrowFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SavingThrows);
            var anySavingThrowFeats = feats.Where(f => anySavingThrowFeatNames.Contains(f.Name));

            foreach (var feat in anySavingThrowFeats)
            {
                savingThrows.Reflex += GetBonus(feat, SavingThrowConstants.Reflex);
                savingThrows.Will += GetBonus(feat, SavingThrowConstants.Will);
                savingThrows.CircumstantialBonus |= HasCircumstantialBonus(feat);
            }

            if (savingThrows.HasFortitudeSave)
            {
                savingThrows.Fortitude = stats[StatConstants.Constitution].Bonus;
                savingThrows.Fortitude += GetClassSavingThrowBonus(characterClass, SavingThrowConstants.Fortitude);
                savingThrows.Fortitude += GetFeatSavingThrowBonus(feats, SavingThrowConstants.Fortitude);

                foreach (var feat in anySavingThrowFeats)
                {
                    savingThrows.Fortitude += GetBonus(feat, SavingThrowConstants.Fortitude);
                    savingThrows.CircumstantialBonus |= HasCircumstantialBonus(feat);
                }
            }

            return savingThrows;
        }

        private int GetBonus(Feat feat, string savingThrow)
        {
            if (feat.Foci.Contains(FeatConstants.Foci.All) || feat.Foci.Contains(savingThrow))
                return feat.Power;

            return 0;
        }

        private bool HasCircumstantialBonus(Feat feat)
        {
            var saveFoci = new[]
            {
                FeatConstants.Foci.All,
                SavingThrowConstants.Fortitude,
                SavingThrowConstants.Reflex,
                SavingThrowConstants.Will
            };

            return feat.Foci.Intersect(saveFoci).Any() == false;
        }

        private int GetClassSavingThrowBonus(CharacterClass characterClass, string savingThrow)
        {
            var strong = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, savingThrow);
            if (strong.Contains(characterClass.Name))
                return characterClass.Level / 2 + 2;

            return characterClass.Level / 3;
        }

        private int GetFeatSavingThrowBonus(IEnumerable<Feat> feats, string savingThrow)
        {
            var saveFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, savingThrow);
            var saveFeats = feats.Where(f => saveFeatNames.Contains(f.Name));

            return saveFeats.Sum(f => f.Power);
        }
    }
}