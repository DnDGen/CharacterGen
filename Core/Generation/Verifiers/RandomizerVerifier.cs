﻿using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private IClassNameRandomizer classNameRandomizer;
        private IBaseRaceRandomizer baseRaceRandomizer;
        private IMetaraceRandomizer metaraceRandomizer;
        private ILevelRandomizer levelRandomizer;
        private ILevelAdjustmentsProvider levelAdjustmentsProvider;

        public RandomizerVerifier(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            ILevelAdjustmentsProvider levelAdjustmentsProvider)
        {
            this.alignmentRandomizer = alignmentRandomizer;
            this.classNameRandomizer = classNameRandomizer;
            this.levelRandomizer = levelRandomizer;
            this.baseRaceRandomizer = baseRaceRandomizer;
            this.metaraceRandomizer = metaraceRandomizer;
            this.levelAdjustmentsProvider = levelAdjustmentsProvider;
        }

        public Boolean VerifyCompatibility()
        {
            var alignments = alignmentRandomizer.GetAllPossibleResults();
            return alignments.Any() && alignments.Any(a => VerifyAlignmentCompatibility(a));
        }

        public Boolean VerifyAlignmentCompatibility(Alignment alignment)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);
            var levels = levelRandomizer.GetAllPossibleResults();

            var characterClasses = GetAllCharacterClassPrototypes(classNames, levels);
            return characterClasses.Any() && characterClasses.Any(c => VerifyCharacterClassCompatibility(alignment.Goodness, c));
        }

        private IEnumerable<CharacterClassPrototype> GetAllCharacterClassPrototypes(IEnumerable<String> classNames, IEnumerable<Int32> levels)
        {
            var characterClasses = new List<CharacterClassPrototype>();

            foreach (var className in classNames)
                foreach (var level in levels)
                    characterClasses.Add(new CharacterClassPrototype() { ClassName = className, Level = level });

            return characterClasses;
        }

        public Boolean VerifyCharacterClassCompatibility(String goodness, CharacterClassPrototype prototype)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossibleResults(goodness, prototype);
            var metaraces = metaraceRandomizer.GetAllPossibleResults(goodness, prototype);

            return baseRaces.Any() && metaraces.Any() && LevelAdjustmentsAreAllowed(baseRaces, metaraces, prototype.Level);
        }

        private Boolean LevelAdjustmentsAreAllowed(IEnumerable<String> baseRaces, IEnumerable<String> metaraces, Int32 level)
        {
            var levelAdjustments = levelAdjustmentsProvider.GetLevelAdjustments();
            var minBaseRaceLevelAdjustment = levelAdjustments.Where(kvp => baseRaces.Contains(kvp.Key)).Min(kvp => kvp.Value);
            var minMetaraceLevelAdjustment = levelAdjustments.Where(kvp => metaraces.Contains(kvp.Key)).Min(kvp => kvp.Value);

            return minBaseRaceLevelAdjustment + minMetaraceLevelAdjustment < level;
        }
    }
}