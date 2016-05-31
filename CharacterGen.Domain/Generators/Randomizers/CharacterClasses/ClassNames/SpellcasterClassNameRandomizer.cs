using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class SpellcasterClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public SpellcasterClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
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