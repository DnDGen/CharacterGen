using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class RequiredFeat
    {
        public String FeatId { get; set; }
        public String Focus { get; set; }

        public RequiredFeat()
        {
            FeatId = String.Empty;
            Focus = String.Empty;
        }

        public Boolean RequirementMet(IEnumerable<Feat> otherFeats)
        {
            var requiredFeats = otherFeats.Where(f => f.Name.Id == FeatId);

            if (requiredFeats.Any() == false)
                return false;

            if (String.IsNullOrEmpty(Focus))
                return true;

            return requiredFeats.Any(f => f.Focus == Focus);
        }
    }
}