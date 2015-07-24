using System;

namespace NPCGen.Common.Alignments
{
    public class Alignment
    {
        public String Lawfulness { get; set; }
        public String Goodness { get; set; }

        public Alignment()
        {
            Lawfulness = String.Empty;
            Goodness = String.Empty;
        }

        public override String ToString()
        {
            if (Lawfulness == AlignmentConstants.Neutral && Goodness == AlignmentConstants.Neutral)
                return "True Neutral";

            return String.Format("{0} {1}", Lawfulness, Goodness);
        }

        public override Boolean Equals(Object toCompare)
        {
            if (!(toCompare is Alignment))
                return false;

            var alignment = toCompare as Alignment;
            return Goodness == alignment.Goodness && Lawfulness == alignment.Lawfulness;
        }

        public override Int32 GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}