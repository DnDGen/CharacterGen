using System;

namespace NPCGen.Roll
{
    public class Dice
    {
        private static Random NewCryptoSeededRandom()
        {
            return new Random();
        }

        public static Int32 Roll(Int32 number, Int32 dNumber, Int32 plus = 0)
        {
            var random = NewCryptoSeededRandom();

            var total = 0;
            while (number-- > 0)
                total += random.Next(0, dNumber) + 1;

            return total + plus;
        }

        public static Int32 Percentile(Int32 plus = 0)
        {
            return Roll(1, 100, plus);
        }

        public static Int32 d20(Int32 plus = 0)
        {
            return Roll(1, 20, plus);
        }

        public static Int32 d12(Int32 plus = 0)
        {
            return Roll(1, 12, plus);
        }

        public static Int32 d10(Int32 plus = 0)
        {
            return Roll(1, 10, plus);
        }

        public static Int32 d8(Int32 plus = 0)
        {
            return Roll(1, 8, plus);
        }

        public static Int32 d6(Int32 plus = 0)
        {
            return Roll(1, 6, plus);
        }

        public static Int32 d4(Int32 plus = 0)
        {
            return Roll(1, 4, plus);
        }
    }
}