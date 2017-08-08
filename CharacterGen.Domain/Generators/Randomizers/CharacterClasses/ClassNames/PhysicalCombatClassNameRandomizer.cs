using CharacterGen.Alignments;
using CharacterGen.Domain.Tables;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class PhysicalCombatClassNameRandomizer : BaseClassNameRandomizer
    {
        private readonly ICollectionsSelector collectionsSelector;

        public PhysicalCombatClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator, collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
        {
            var warriors = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.PhysicalCombat);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.ToString());

            var allowedClasses = warriors.Intersect(alignmentClasses);
            return allowedClasses.Contains(className);
        }
    }
}