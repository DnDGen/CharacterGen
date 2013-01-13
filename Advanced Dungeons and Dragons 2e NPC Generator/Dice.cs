using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen
{
    class Dice
    {
        public enum ROLLMETHOD { BEST_OF_4, ONE_AS_6, STRAIGHT, TWO_d10, AVERAGE, MEDIUM, HEROIC, POOR};

        public static string[] RollMethodArray
        {
            get
            {
                return Enum.GetNames(typeof(ROLLMETHOD));
            }
        }

        public static int[] Roll(ROLLMETHOD RollMethod, ref Random random)
        {
            System.Windows.Forms.Application.DoEvents();
            int[] Stats = new int[6];
            int[] TempRolls = new int[4]; int TempRoll;
            
            switch (RollMethod)
            {
                case ROLLMETHOD.STRAIGHT:
                    for (int i = 0; i < Stats.Length; i++)
                        Stats[i] = Roll(3, 6, 0, ref random);
                    return Stats;
                case ROLLMETHOD.BEST_OF_4:
                    for (int i = 0; i < Stats.Length; i++)
                    {
                        for (int j = 0; j < 4; j++)
                            TempRolls[j] = d6(ref random);
                        TempRolls[FindMin(TempRolls)] = 0;
                        Stats[i] = Sum(TempRolls);
                    }
                    return Stats;
                case ROLLMETHOD.ONE_AS_6:
                    for (int i = 0; i < Stats.Length; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            TempRoll = d6(ref random);
                            if (TempRoll == 1)
                                TempRoll = 6;
                            TempRolls[j] = TempRoll;
                        }
                        Stats[i] = Sum(TempRolls);
                    }
                    return Stats;
                case ROLLMETHOD.TWO_d10:
                    for (int i = 0; i < Stats.Length; i++)
                        Stats[i] = Roll(2, 10, 0, ref random);
                    return Stats;
                case ROLLMETHOD.POOR:
                    do
                    {
                        Stats = Roll(ROLLMETHOD.STRAIGHT, ref random);
                        System.Windows.Forms.Application.DoEvents();
                    } while (Average(Stats) > 9);
                    return Stats;
                case ROLLMETHOD.AVERAGE:
                    do
                    {
                        Stats = Roll(ROLLMETHOD.STRAIGHT, ref random);
                        System.Windows.Forms.Application.DoEvents();
                    } while (Average(Stats) < 10 || Average(Stats) > 12);
                    return Stats;
                case ROLLMETHOD.MEDIUM:
                    do
                    {
                        Stats = Roll(ROLLMETHOD.STRAIGHT, ref random);
                        System.Windows.Forms.Application.DoEvents();
                    } while (Average(Stats) < 13 || Average(Stats) > 15);
                    return Stats;
                case ROLLMETHOD.HEROIC:
                    do
                    {
                        Stats = Roll(ROLLMETHOD.ONE_AS_6, ref random);
                        System.Windows.Forms.Application.DoEvents();
                    } while (Average(Stats) < 16);
                    return Stats;
                default: return Stats;
            }
        }

        private static int Sum(int[] Input)
        {
            int sum = 0;

            foreach (int number in Input)
                sum += number;

            return sum;
        }

        private static int FindMin(int[] Input)
        {
            int MinimumIndex = 0;

            for (int i = 1; i < Input.Length; i++)
                if (Input[i] < Input[MinimumIndex])
                    MinimumIndex = i;

            return MinimumIndex;
        }

        private static int Average(int[] Input)
        {
            int average = 0;

            foreach (int number in Input)
                average += number;

            average = average / Input.Length;

            return average;
        }
        
        public static int Roll(int Number, int dNumber, int Plus, ref Random random)
        {
            int total = 0;
            while (Number > 0)
            {
                total += random.Next(0, dNumber) + 1;
                Number--;
            }

            return total + Plus;
        }

        public static int Percentile(ref Random random)
        {
            return Roll(1, 100, 0, ref random);
        }

        public static int d20(ref Random random)
        {
            return Roll(1, 20, 0, ref random);
        }

        public static int d12(ref Random random)
        {
            return Roll(1, 12, 0, ref random);
        }

        public static int d10(ref Random random)
        {
            return Roll(1, 10, 0, ref random);
        }

        public static int d8(ref Random random)
        {
            return Roll(1, 8, 0, ref random);
        }

        public static int d6(ref Random random)
        {
            return Roll(1, 6, 0, ref random);
        }

        public static int d4(ref Random random)
        {
            return Roll(1, 4, 0, ref random);
        }
    }
}