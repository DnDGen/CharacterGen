using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPCGen.Roll
{
    public enum ROLLMETHOD { BEST_OF_4, ONE_AS_6, STRAIGHT, TWO_d10, AVERAGE, MEDIUM, HEROIC, POOR };
    
    public class StatDice : Dice
    {
        public static String[] RollMethodArray { get { return Enum.GetNames(typeof(ROLLMETHOD)); } }

        public static Int32[] Roll(ROLLMETHOD RollMethod)
        {
            var stats = new Int32[6];
            var tempRolls = new Int32[4];
            Int32 tempRoll;

            switch (RollMethod)
            {
                case ROLLMETHOD.STRAIGHT:
                    for (var i = 0; i < stats.Length; i++)
                        stats[i] = Roll(3, 6, 0);
                    return stats;
                case ROLLMETHOD.BEST_OF_4:
                    for (var i = 0; i < stats.Length; i++)
                    {
                        for (var j = 0; j < 4; j++)
                            tempRolls[j] = d6();
                        var orderedRolls = tempRolls.OrderByDescending(x => x).ToArray();
                        orderedRolls[4] = 0;
                        stats[i] = orderedRolls.Sum();
                    }
                    return stats;
                case ROLLMETHOD.ONE_AS_6:
                    for (var i = 0; i < stats.Length; i++)
                    {
                        for (var j = 0; j < 3; j++)
                        {
                            tempRoll = d6();
                            if (tempRoll == 1)
                                tempRoll = 6;
                            tempRolls[j] = tempRoll;
                        }
                        stats[i] = tempRolls.Sum();
                    }
                    return stats;
                case ROLLMETHOD.TWO_d10:
                    for (var i = 0; i < stats.Length; i++)
                        stats[i] = Roll(2, 10, 0);
                    return stats;
                case ROLLMETHOD.POOR:
                    do { stats = Roll(ROLLMETHOD.STRAIGHT); }
                    while (stats.Average() > 9);
                    return stats;
                case ROLLMETHOD.AVERAGE:
                    do { stats = Roll(ROLLMETHOD.STRAIGHT); }
                    while (stats.Average() < 10 || stats.Average() > 12);
                    return stats;
                case ROLLMETHOD.MEDIUM:
                    do { stats = Roll(ROLLMETHOD.STRAIGHT); }
                    while (stats.Average() < 13 || stats.Average() > 15);
                    return stats;
                case ROLLMETHOD.HEROIC:
                    do { stats = Roll(ROLLMETHOD.ONE_AS_6); }
                    while (stats.Average() < 16);
                    return stats;
                default: return stats;
            }
        }
    }
}