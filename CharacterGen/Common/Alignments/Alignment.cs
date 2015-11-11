using System;

namespace CharacterGen.Common.Alignments
{
    public class Alignment
    {
        public String Lawfulness { get; set; }
        public String Goodness { get; set; }

        public String Full
        {
            get
            {
                if (Lawfulness == AlignmentConstants.Neutral && Goodness == AlignmentConstants.Neutral)
                    return "True Neutral";

                return String.Format("{0} {1}", Lawfulness, Goodness);
            }
        }

        public Alignment()
        {
            Lawfulness = String.Empty;
            Goodness = String.Empty;
        }

        public Alignment(String alignment)
        {
            var parts = alignment.Split(' ');

            if (parts.Length != 2)
            {
                Lawfulness = String.Empty;
                Goodness = String.Empty;
            }
            else
            {
                Lawfulness = parts[0];
                Goodness = parts[1];
            }

            if (Lawfulness == "True")
                Lawfulness = AlignmentConstants.Neutral;
        }

        public override String ToString()
        {
            return Full;
        }

        public override Boolean Equals(Object toCompare)
        {
            if (!(toCompare is Alignment))
                return false;

            var alignment = toCompare as Alignment;
            return Full == alignment.Full;
        }

        public override Int32 GetHashCode()
        {
            return Full.GetHashCode();
        }
    }
}