using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.CharacterClasses
{
    public class CharacterClass
    {
        public int Level { get; set; }
        public int LevelAdjustment { get; set; }
        public bool IsNPC { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> SpecialistFields { get; set; }
        public IEnumerable<string> ProhibitedFields { get; set; }

        public double EffectiveLevel
        {
            get
            {
                var divisor = IsNPC ? 2d : 1d;
                var effectiveLevel = (LevelAdjustment + Level) / divisor;

                if (effectiveLevel > 1)
                    return Math.Floor(effectiveLevel);

                return effectiveLevel;
            }
        }

        public string Summary
        {
            get
            {
                return $"Level {Level} {Name}";
            }
        }

        public CharacterClass()
        {
            Name = string.Empty;
            SpecialistFields = Enumerable.Empty<string>();
            ProhibitedFields = Enumerable.Empty<string>();
        }
    }
}