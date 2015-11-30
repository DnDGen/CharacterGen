using System;
using System.Collections.Generic;
using System.Linq;
using CharacterGen.Common.Abilities.Feats;

namespace CharacterGen.Selectors.Objects
{
    public class RequiredFeat
    {
        public String Feat { get; set; }
        public String Focus { get; set; }

        public RequiredFeat()
        {
            Feat = String.Empty;
            Focus = String.Empty;
        }

        public Boolean RequirementMet(IEnumerable<Feat> otherFeats)
        {
            var requiredFeats = otherFeats.Where(f => f.Name == Feat);

            if (requiredFeats.Any() == false)
                return false;

            if (String.IsNullOrEmpty(Focus))
                return true;

            return requiredFeats.Any(f => f.Foci.Contains(Focus));
        }
    }
}