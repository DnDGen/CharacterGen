using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Data.Races;
using NPCGen.Core.Characters.Generation.Factories.Interfaces;
using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Level;
using System;

namespace NPCGen.Core.Characters.Generation.Factories
{
    public class CharacterClassFactory : ICharacterClassFactory
    {
        public ILevelRandomizer LevelRandomizer { get; set; }
        public IClassRandomizer ClassRandomizer { get; set; }

        public CharacterClass Generate()
        {
            var characterClass = new CharacterClass();

            characterClass.Level = LevelRandomizer.Randomize();
            characterClass.ClassName = ClassRandomizer.Randomize();

            return characterClass;
        }

        public CharacterClass Restrict(this CharacterClass characterClass, Alignment alignment, Race race)
        {
            throw new NotImplementedException();
        }
    }
}