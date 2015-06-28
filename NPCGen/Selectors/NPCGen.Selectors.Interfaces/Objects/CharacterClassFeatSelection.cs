using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class CharacterClassFeatSelection
    {
        public const Int32 FeatIdIndex = 0;
        public const Int32 MinimumLevelRequirementIndex = 1;
        public const Int32 FocusTypeIndex = 2;
        public const Int32 StrengthIndex = 3;
        public const Int32 FrequencyQuantityIndex = 4;
        public const Int32 FrequencyTimePeriodIndex = 5;
        public const Int32 MaximumLevelRequirementIndex = 6;

        public String FeatId { get; set; }
        public String FocusType { get; set; }
        public Int32 MinimumLevel { get; set; }
        public Int32 MaximumLevel { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }
        public IEnumerable<String> RequiredFeatIds { get; set; }

        public CharacterClassFeatSelection()
        {
            FeatId = String.Empty;
            FocusType = String.Empty;
            Frequency = new Frequency();
            RequiredFeatIds = Enumerable.Empty<String>();
        }

        public Boolean RequirementsMet(CharacterClass characterClass)
        {
            if (MaximumLevel > 0)
                return MinimumLevel <= characterClass.Level && characterClass.Level <= MaximumLevel;

            return MinimumLevel <= characterClass.Level;
        }
    }
}