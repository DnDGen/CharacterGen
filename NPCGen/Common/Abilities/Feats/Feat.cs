using System;

namespace NPCGen.Common.Feats
{
    public class Feat
    {
        public String Name { get; set; }
        public String Description { get; set; }

        public Feat()
        {
            Name = String.Empty;
            Description = String.Empty;
        }
    }
}