using CharacterGen.Common.Abilities;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using System;

namespace CharacterGen.Common.Magics
{
    public class Animal
    {
        public Race Race { get; set; }
        public Ability Ability { get; set; }
        public Combat Combat { get; set; }
        public Int32 Tricks { get; set; }

        public Animal()
        {
            Race = new Race();
            Ability = new Ability();
            Combat = new Combat();
        }
    }
}