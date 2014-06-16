using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class LevelAdjustmentsSelector : ILevelAdjustmentsSelector
    {
        private IAdjustmentMapper adjustmentXmlMapper;

        public LevelAdjustmentsSelector(IAdjustmentMapper adjustmentXmlMapper)
        {
            this.adjustmentXmlMapper = adjustmentXmlMapper;
        }

        public Dictionary<String, Int32> GetLevelAdjustments()
        {
            return adjustmentXmlMapper.Map("LevelAdjustments");
        }
    }
}