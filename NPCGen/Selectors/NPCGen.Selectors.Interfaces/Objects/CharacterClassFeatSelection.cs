using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class CharacterClassFeatSelection
    {
        public String FeatId { get; set; }
        public String FocusType { get; set; }
        public Int32 MinimumLevel { get; set; }
        public Int32 MaximumLevel { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }
        public IEnumerable<String> RequiredFeatIds { get; set; }
        public String FrequencyQuantityStat { get; set; }

        public CharacterClassFeatSelection()
        {
            FeatId = String.Empty;
            FocusType = String.Empty;
            Frequency = new Frequency();
            RequiredFeatIds = Enumerable.Empty<String>();
            FrequencyQuantityStat = String.Empty;
        }

        public Boolean RequirementsMet(CharacterClass characterClass)
        {
            if (MaximumLevel > 0)
                return MinimumLevel <= characterClass.Level && characterClass.Level <= MaximumLevel;

            return MinimumLevel <= characterClass.Level;
        }
    }
}