using System;
using NPCGen.Common.Races;

namespace NPCGen.Common.Abilities.Feats
{
    public class Feat
    {
        public NameModel Name { get; set; }
        public String SpecificApplication { get; set; }

        public Feat()
        {
            Name = new NameModel();
            SpecificApplication = String.Empty;
        }

        public override Boolean Equals(Object obj)
        {
            if (!(obj is Feat))
                return false;

            var otherFeat = obj as Feat;
            return Name.Id == otherFeat.Name.Id && SpecificApplication == otherFeat.SpecificApplication;
        }

        public override Int32 GetHashCode()
        {
            return Name.Id.GetHashCode() + SpecificApplication.GetHashCode();
        }
    }
}