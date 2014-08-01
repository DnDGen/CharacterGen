using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators
{
    public class CharacterClassGenerator : ICharacterClassGenerator
    {
        public CharacterClass GenerateWith(Alignment alignment, ILevelRandomizer levelRandomizer,
            IClassNameRandomizer classNameRandomizer)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = levelRandomizer.Randomize();
            characterClass.ClassName = classNameRandomizer.Randomize(alignment);

            return characterClass;
        }
    }
}