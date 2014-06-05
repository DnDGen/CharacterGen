using System;

namespace NPCGen.Core.Data.Alignments
{
    public class Alignment
    {
        public String Lawfulness { get; set; }
        public String Goodness { get; set; }

        public Boolean IsNeutral()
        {
            return Goodness == AlignmentConstants.Neutral || Lawfulness == AlignmentConstants.Neutral;
        }

        public Boolean IsLawful()
        {
            return Lawfulness == AlignmentConstants.Lawful;
        }

        public Boolean IsChaotic()
        {
            return Lawfulness == AlignmentConstants.Chaotic;
        }

        public Boolean IsGood()
        {
            return Goodness == AlignmentConstants.Good;
        }

        public Boolean IsEvil()
        {
            return Goodness == AlignmentConstants.Evil;
        }

        public override String ToString()
        {
            if (Lawfulness == AlignmentConstants.Neutral && Goodness == AlignmentConstants.Neutral)
                return "True Neutral";

            return String.Format("{0} {1}", Lawfulness, Goodness);
        }
    }
}