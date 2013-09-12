using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public class SetClass : IClassRandomizer
    {
        private String characterClass;

        public SetClass(String characterClass)
        {
            this.characterClass = characterClass;
        }

        public String Randomize()
        {
            return characterClass;
        }
    }
}