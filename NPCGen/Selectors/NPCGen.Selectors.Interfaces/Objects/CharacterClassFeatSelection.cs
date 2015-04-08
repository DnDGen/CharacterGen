using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class CharacterClassFeatSelection
    {
        public String FeatName { get; set; }
        public Dictionary<String, Int32> LevelRequirements { get; set; }
        public Int32 Strength { get; set; }

        public CharacterClassFeatSelection()
        {
            FeatName = String.Empty;
            LevelRequirements = new Dictionary<String, Int32>();
        }

        public Boolean RequirementsSatisfied(CharacterClass characterClass)
        {
            var baseClassSatisfied = LevelRequirements.ContainsKey(characterClass.ClassName) && LevelRequirements[characterClass.ClassName] <= characterClass.Level;
            var specialistClassSatisfied = characterClass.SpecialistFields.Any(f => LevelRequirements.ContainsKey(f));

            return baseClassSatisfied || specialistClassSatisfied;
        }
    }
}