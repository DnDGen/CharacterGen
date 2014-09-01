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
        public override Boolean Equals(Object obj)
        {
            if (!(obj is Feat))
                return false;

            var otherFeat = obj as Feat;
            return Name == otherFeat.Name && SpecificApplication == otherFeat.SpecificApplication;
        }

        public override Int32 GetHashCode()
        {
            return Name.GetHashCode() + SpecificApplication.GetHashCode();
        }
    }
}