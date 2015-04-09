using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class CharacterClassFeatSelection
    {
        public NameModel Name { get; set; }
        public Dictionary<String, Int32> LevelRequirements { get; set; }
        public Int32 Strength { get; set; }

        public CharacterClassFeatSelection()
        {
            Name = new NameModel();
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