using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class CharacterClassFeatSelection
    {
        public String FeatId { get; set; }
        public String FocusType { get; set; }
        public Int32 MinimumLevel { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }
        public IEnumerable<String> RequiredFeatIds { get; set; }

        public CharacterClassFeatSelection()
        {
            FeatId = String.Empty;
            FocusType = String.Empty;
            Frequency = new Frequency();
        }
    }
}