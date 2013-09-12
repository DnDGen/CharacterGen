using System;

namespace NPCGen.Core.Characters.Data
{
    public class Alignment
    {
        public Int32 Lawfulness 
        {
            get { return lawfulness; }
            set
            {
                if (value >= -1 && value <= 1)
                    lawfulness = value;
            }
        }
        public Int32 Goodness
        {
            get { return goodness; }
            set
            {
                if (value >= -1 && value <= 1)
                    goodness = value;
            }
        }

        private Int32 lawfulness;
        private Int32 goodness;

        public Boolean IsNeutral()
        {
            return Goodness == 0 || Lawfulness == 0;
        }

        public Boolean IsLawful()
        {
            return Lawfulness == 1;
        }

        public Boolean IsChaotic()
        {
            return Lawfulness == -1;
        }

        public Boolean IsGood()
        {
            return Goodness == 1;
        }

        public Boolean IsEvil()
        {
            return Goodness == -1;
        }

        public override String ToString()
        {
            if (lawfulness == 0 && goodness == 0)
                return "True Neutral";

            var lawfulnessString = String.Empty;
            var goodnessString = String.Empty;

            switch (lawfulness)
            {
                case -1: lawfulnessString = "Chaotic"; break;
                case 0: lawfulnessString = "Neutral"; break;
                case 1: lawfulnessString = "Lawful"; break;
            }

            switch (goodness)
            {
                case -1: goodnessString = "Evil"; break;
                case 0: goodnessString = "Neutral"; break;
                case 1: goodnessString = "Good"; break;
            }

            return String.Format("{0} {1}", lawfulnessString, goodnessString);
        }
    }
}