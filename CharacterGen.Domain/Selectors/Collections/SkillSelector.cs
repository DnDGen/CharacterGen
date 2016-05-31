using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class SkillSelector : ISkillSelector
    {
        private ICollectionsSelector innerSelector;

        public SkillSelector(ICollectionsSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public SkillSelection SelectFor(string skill)
        {
            var data = innerSelector.SelectFrom(TableNameConstants.Set.Collection.SkillData, skill);

            var selection = new SkillSelection();
            selection.BaseStatName = data.Single();

            return selection;
        }
    }
}