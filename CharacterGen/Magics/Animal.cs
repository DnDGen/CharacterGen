using CharacterGen.Abilities;
using CharacterGen.Combats;
using CharacterGen.Races;

namespace CharacterGen.Magics
{
    public class Animal
    {
        public Race Race { get; set; }
        public Ability Ability { get; set; }
        public Combat Combat { get; set; }
        public int Tricks { get; set; }

        public Animal()
        {
            Race = new Race();
            Ability = new Ability();
            Combat = new Combat();
        }
    }
}