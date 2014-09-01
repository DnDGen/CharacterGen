using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class MageClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public MageClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var classNames = collectionsSelector.SelectFrom("ClassNameGroups", "Mages");
            if (!classNames.Contains(className))
                return false;

            switch (className)
            {
                case CharacterClassConstants.Bard: return !alignment.IsLawful();
                default: return true;
            }
        }
    }
}