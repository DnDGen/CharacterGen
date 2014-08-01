using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class AdjustmentsSelector : IAdjustmentsSelector
    {
        private IAdjustmentMapper adjustmentMapper;

        public AdjustmentsSelector(IAdjustmentMapper adjustmentMapper)
        {
            this.adjustmentMapper = adjustmentMapper;
        }

        public Dictionary<String, Int32> SelectAdjustmentsFrom(String tableName)
        {
            return adjustmentMapper.Map(tableName);
        }
    }
}