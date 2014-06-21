using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class LevelAdjustmentsSelector : ILevelAdjustmentsSelector
    {
        private IAdjustmentMapper adjustmentMapper;

        public LevelAdjustmentsSelector(IAdjustmentMapper adjustmentMapper)
        {
            this.adjustmentMapper = adjustmentMapper;
        }

        public Dictionary<String, Int32> GetAdjustments()
        {
            return adjustmentMapper.Map("LevelAdjustments");
        }
    }
}