using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Selectors.Objects
{
    public class CharacterClassFeatSelection
    {
        public String Feat { get; set; }
        public String FocusType { get; set; }
        public Int32 MinimumLevel { get; set; }
        public Int32 MaximumLevel { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }
        public IEnumerable<RequiredFeat> RequiredFeats { get; set; }
        public String FrequencyQuantityStat { get; set; }
        public String SizeRequirement { get; set; }

        public CharacterClassFeatSelection()
        {
            Feat = String.Empty;
            FocusType = String.Empty;
            Frequency = new Frequency();
            RequiredFeats = Enumerable.Empty<RequiredFeat>();
            FrequencyQuantityStat = String.Empty;
        }

        public Boolean RequirementsMet(CharacterClass characterClass, Race race)
        {
            if (String.IsNullOrEmpty(SizeRequirement) == false && SizeRequirement != race.Size)
                return false;

            if (MaximumLevel > 0 && characterClass.Level > MaximumLevel)
                return false;

            return MinimumLevel <= characterClass.Level;
        }
    }
}