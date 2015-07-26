using System;
using CharacterGen.Common.Races;

namespace CharacterGen.Common.Abilities.Feats
{
    public class Feat
    {
        public String Name { get; set; }
        public String Focus { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }

        public Feat()
        {
            Name = String.Empty;
            Focus = String.Empty;
            Frequency = new Frequency();
        }
    }
}