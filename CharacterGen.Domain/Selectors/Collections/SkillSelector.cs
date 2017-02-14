using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Domain.Tables;
using System;
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
            var data = innerSelector.SelectFrom(TableNameConstants.Set.Collection.SkillData, skill).ToArray();

            var selection = new SkillSelection();
            selection.BaseStatName = data[DataIndexConstants.SkillSelectionData.BaseStatName];
            selection.SkillName = data[DataIndexConstants.SkillSelectionData.SkillName];
            selection.RandomFociQuantity = Convert.ToInt32(data[DataIndexConstants.SkillSelectionData.RandomFociQuantity]);

            return selection;
        }
    }
}