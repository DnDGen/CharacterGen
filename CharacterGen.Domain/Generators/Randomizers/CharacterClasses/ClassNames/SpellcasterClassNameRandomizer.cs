﻿using CharacterGen.Alignments;
using CharacterGen.Domain.Tables;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class SpellcasterClassNameRandomizer : BaseClassNameRandomizer
    {
        private readonly ICollectionSelector collectionsSelector;

        public SpellcasterClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator, collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
        {
            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups,
                GroupConstants.Spellcasters);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups,
                alignment.ToString());

            var allowedClasses = spellcasters.Intersect(alignmentClasses);
            return allowedClasses.Contains(className);
        }
    }
}