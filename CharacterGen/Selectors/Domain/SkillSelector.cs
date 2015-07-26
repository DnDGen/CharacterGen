using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using System;
using System.Linq;

namespace CharacterGen.Selectors.Domain
{
    public class SkillSelector : ISkillSelector
    {
        private ICollectionsSelector innerSelector;

        public SkillSelector(ICollectionsSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public SkillSelection SelectFor(String skill)
        {
            var data = innerSelector.SelectFrom(TableNameConstants.Set.Collection.SkillData, skill);

            var selection = new SkillSelection();
            selection.ArmorCheckPenalty = Convert.ToBoolean(data.Last());
            selection.BaseStatName = data.First();

            return selection;
        }
    }
}