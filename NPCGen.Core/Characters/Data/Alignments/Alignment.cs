using System;

namespace NPCGen.Core.Characters.Data.Alignments
{
    public class Alignment
    {
        public Int32 Lawfulness { get; set; }
        public Int32 Goodness { get; set; }

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

            return String.Format("{0} {1}", GetLawfulnessString(), GetGoodnessString());
        }

        public String GetLawfulnessString()
        {
            switch (Lawfulness)
            {
                case AlignmentConstants.Chaotic: return "Chaotic";
                case AlignmentConstants.Neutral: return "Neutral";
                case AlignmentConstants.Lawful: return "Lawful";
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public String GetGoodnessString()
        {
            switch (Goodness)
            {
                case AlignmentConstants.Evil: return "Evil";
                case AlignmentConstants.Neutral: return "Neutral";
                case AlignmentConstants.Good: return "Good";
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}