using System;

namespace NPCGen.Tables.Interfaces
{
    public static class TableNameConstants
    {
        public static class Set
        {
            public static class Percentile
            {
                public const String AlignmentGoodness = "AlignmentGoodness";
                public const String Traits = "Traits";
            }

            public static class Collection
            {
                public const String LevelAdjustments = "LevelAdjustments";
                public const String ArmorCheckPenalties = "ArmorCheckPenalties";
            }
        }

        public static class Formattable
        {
            public static class Percentile
            {
                public const String GOODNESSCharacterClasses = "{0}CharacterClasses";
                public const String GOODNESSCLASSBaseRaces = "{0}{1}BaseRaces";
                public const String GOODNESSCLASSMetaraces = "{0}{1}Metaraces";
            }

            public static class Collection
            {

            }
        }
    }
}