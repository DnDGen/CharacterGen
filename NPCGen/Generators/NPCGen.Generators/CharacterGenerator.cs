using System;
using System.Collections.Generic;
using NPCGen.Common;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Abilities;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators
{
    public class CharacterGenerator : ICharacterGenerator
    {
        private ILanguageGenerator languageGenerator;
        private IAlignmentGenerator alignmentGenerator;
        private ICharacterClassGenerator characterClassGenerator;
        private IStatsGenerator statsGenerator;
        private IHitPointsGenerator hitPointsGenerator;
        private IRaceGenerator raceGenerator;
        private IAdjustmentsSelector adjustmentsSelector;
        private IRandomizerVerifier randomizerVerifier;
        private IPercentileSelector percentileSelector;

        public CharacterGenerator(IAlignmentGenerator alignmentGenerator, ICharacterClassGenerator characterClassGenerator, IRaceGenerator raceGenerator,
            IStatsGenerator statsGenerator, ILanguageGenerator languageGenerator, IHitPointsGenerator hitPointsGenerator, IAdjustmentsSelector adjustmentsSelector,
            IRandomizerVerifier randomizerVerifier, IPercentileSelector percentileSelector)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.characterClassGenerator = characterClassGenerator;
            this.raceGenerator = raceGenerator;
            this.statsGenerator = statsGenerator;
            this.languageGenerator = languageGenerator;
            this.hitPointsGenerator = hitPointsGenerator;

            this.adjustmentsSelector = adjustmentsSelector;
            this.randomizerVerifier = randomizerVerifier;
            this.percentileSelector = percentileSelector;
        }

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            VerifyRandomizers(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            var character = new Character();

            character.Alignment = GenerateAlignment(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer,
                metaraceRandomizer);
            var characterClassPrototype = GenerateCharacterClassPrototype(classNameRandomizer, levelRandomizer, character.Alignment,
                baseRaceRandomizer, metaraceRandomizer);

            var levelAdjustments = adjustmentsSelector.GetAdjustmentsFrom("LevelAdjustments");
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, levelAdjustments, character.Alignment, characterClassPrototype);

            characterClassPrototype.Level -= levelAdjustments[character.Race.BaseRace];
            characterClassPrototype.Level -= levelAdjustments[character.Race.Metarace];

            character.Class = characterClassGenerator.GenerateWith(characterClassPrototype);

            character.Stats = statsGenerator.GenerateWith(statsRandomizer, character.Class, character.Race);
            character.HitPoints = hitPointsGenerator.GenerateWith(character.Class, character.Stats[StatConstants.Constitution].Bonus, character.Race);
            character.InterestingTrait = percentileSelector.GetPercentileFrom("Traits");
            character.Languages = languageGenerator.GenerateWith(character.Race, character.Class.ClassName, character.Stats[StatConstants.Intelligence].Bonus);

            return character;
        }

        private void VerifyRandomizers(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var verified = randomizerVerifier.VerifyCompatibility(alignmentRandomizer, classNameRandomizer, levelRandomizer,
                baseRaceRandomizer, metaraceRandomizer);

            if (!verified)
                throw new IncompatibleRandomizersException();
        }

        private Alignment GenerateAlignment(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            Alignment alignment;

            do alignment = alignmentGenerator.GenerateWith(alignmentRandomizer);
            while (!randomizerVerifier.VerifyAlignmentCompatibility(alignment, classNameRandomizer, levelRandomizer, baseRaceRandomizer,
                metaraceRandomizer));

            return alignment;
        }

        private CharacterClassPrototype GenerateCharacterClassPrototype(IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            Alignment alignment, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            CharacterClassPrototype prototype;

            do prototype = characterClassGenerator.GeneratePrototypeWith(alignment, levelRandomizer, classNameRandomizer);
            while (!randomizerVerifier.VerifyCharacterClassCompatibility(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer));

            return prototype;
        }

        private Race GenerateRace(IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer, Dictionary<String, Int32> levelAdjustments, Alignment alignment, CharacterClassPrototype prototype)
        {
            Race race;

            do race = raceGenerator.GenerateWith(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer);
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= prototype.Level);

            return race;
        }
    }
}