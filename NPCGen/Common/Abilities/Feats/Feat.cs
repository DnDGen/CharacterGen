using System;

namespace NPCGen.Common.Abilities.Feats
{
    public class Feat
    {
        public String Name { get; set; }
        public String SpecificApplication { get; set; }

        public Feat()
        {
            Name = String.Empty;
            SpecificApplication = String.Empty;
        }
    }
}