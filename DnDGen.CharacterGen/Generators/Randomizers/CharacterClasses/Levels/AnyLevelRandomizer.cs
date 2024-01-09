﻿using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.Levels
{
    internal class AnyLevelRandomizer : ILevelRandomizer
    {
        private readonly Dice dice;

        public AnyLevelRandomizer(Dice dice)
        {
            this.dice = dice;
        }

        public int Randomize()
        {
            return dice.Roll().d20().AsSum();
        }

        public IEnumerable<int> GetAllPossibleResults()
        {
            return Enumerable.Range(1, 20);
        }
    }
}