using System;
using NPCGen.Common.Races;

namespace NPCGen.Common.Abilities.Feats
{
    public class Feat
    {
        public NameModel Name { get; set; }
        public String Focus { get; set; }
        public Int32 Strength { get; set; }
        public Frequency Frequency { get; set; }

        public Feat()
        {
            Name = new NameModel();
            Focus = String.Empty;
            Frequency = new Frequency();
        }
    }
}