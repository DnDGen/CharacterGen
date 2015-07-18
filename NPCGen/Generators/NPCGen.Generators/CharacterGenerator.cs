using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Magics;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators
{
    public class CharacterGenerator : ICharacterGenerator
    {
        private IAlignmentGenerator alignmentGenerator;
        private ICharacterClassGenerator characterClassGenerator;
        private IRaceGenerator raceGenerator;
        private IAdjustmentsSelector adjustmentsSelector;
        private IRandomizerVerifier randomizerVerifier;
        private IPercentileSelector percentileSelector;
        private IAbilitiesGenerator abilitiesGenerator;
        private ICombatGenerator combatGenerator;
        private IEquipmentGenerator equipmentGenerator;
        private ISetAlignmentRandomizer setAlignmentRandomizer;
        private ISetLevelRandomizer setLevelRandomizer;
        private IAlignmentRandomizer anyAlignmentRandomizer;
        private IClassNameRandomizer anyClassNameRandomizer;
        private IBaseRaceRandomizer anyBaseRaceRandomizer;
        private IMetaraceRandomizer anyMetaraceRandomizer;
        private IStatsRandomizer rawStatsRandomizer;
        private IBooleanPercentileSelector booleanPercentileSelector;
        private ILeadershipSelector leadershipSelector;
        private ICollectionsSelector collectionsSelector;
        private IMagicGenerator magicGenerator;

        public CharacterGenerator(IAlignmentGenerator alignmentGenerator, ICharacterClassGenerator characterClassGenerator, IRaceGenerator raceGenerator,
            IAdjustmentsSelector adjustmentsSelector, IRandomizerVerifier randomizerVerifier, IPercentileSelector percentileSelector,
            IAbilitiesGenerator abilitiesGenerator, ICombatGenerator combatGenerator, IEquipmentGenerator equipmentGenerator,
            ISetAlignmentRandomizer setAlignmentRandomizer, ISetLevelRandomizer setLevelRandomizer, IAlignmentRandomizer anyAlignmentRandomizer,
            IClassNameRandomizer anyClassNameRandomizer, IBaseRaceRandomizer anyBaseRaceRandomizer, IMetaraceRandomizer anyMetaraceRandomizer,
            IStatsRandomizer rawStatsRandomizer, IBooleanPercentileSelector booleanPercentileSelector, ILeadershipSelector leadershipSelector,
            ICollectionsSelector collectionsSelector, IMagicGenerator magicGenerator)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.characterClassGenerator = characterClassGenerator;
            this.raceGenerator = raceGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.combatGenerator = combatGenerator;
            this.equipmentGenerator = equipmentGenerator;

            this.adjustmentsSelector = adjustmentsSelector;
            this.randomizerVerifier = randomizerVerifier;
            this.percentileSelector = percentileSelector;
            this.setAlignmentRandomizer = setAlignmentRandomizer;
            this.setLevelRandomizer = setLevelRandomizer;
            this.anyAlignmentRandomizer = anyAlignmentRandomizer;
            this.anyClassNameRandomizer = anyClassNameRandomizer;
            this.anyBaseRaceRandomizer = anyBaseRaceRandomizer;
            this.anyMetaraceRandomizer = anyMetaraceRandomizer;
            this.rawStatsRandomizer = rawStatsRandomizer;
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.leadershipSelector = leadershipSelector;
            this.collectionsSelector = collectionsSelector;
            this.magicGenerator = magicGenerator;
        }

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            VerifyRandomizers(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            var character = GenerateCharacter(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer,
                statsRandomizer);

            if (character.Ability.Feats.Any(f => f.Name == FeatConstants.Leadership))
                character.Leadership = GenerateLeadership(character);

            return character;
        }

        private Character GenerateCharacter(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            var character = new Character();

            character.Alignment = GenerateAlignment(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);
            character.Class = GenerateCharacterClass(classNameRandomizer, levelRandomizer, character.Alignment, baseRaceRandomizer, metaraceRandomizer);

            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, levelAdjustments, character.Alignment, character.Class);

            character.Class.Level -= levelAdjustments[character.Race.BaseRace];
            character.Class.Level -= levelAdjustments[character.Race.Metarace];

            var baseAttack = combatGenerator.GenerateBaseAttackWith(character.Class, character.Race);

            character.Ability = abilitiesGenerator.GenerateWith(character.Class, character.Race, statsRandomizer, baseAttack);
            character.Equipment = equipmentGenerator.GenerateWith(character.Ability.Feats, character.Class);

            var armorCheckPenalties = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorCheckPenalties);

            foreach (var skill in character.Ability.Skills)
            {
                if (skill.Value.ArmorCheckPenalty)
                    skill.Value.Bonus -= armorCheckPenalties[character.Equipment.Armor.Name];

                if (skill.Key == SkillConstants.Swim)
                    skill.Value.Bonus -= armorCheckPenalties[character.Equipment.Armor.Name];
            }

            character.Combat = combatGenerator.GenerateWith(baseAttack, character.Class, character.Race, character.Ability.Feats, character.Ability.Stats, character.Equipment);
            character.InterestingTrait = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Traits);
            character.Magic = magicGenerator.GenerateWith(character.Class, character.Ability.Feats, character.Equipment);

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

        private CharacterClass GenerateCharacterClass(IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            Alignment alignment, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            CharacterClass characterClass;

            do characterClass = characterClassGenerator.GenerateWith(alignment, levelRandomizer, classNameRandomizer);
            while (randomizerVerifier.VerifyCharacterClassCompatibility(alignment.Goodness, characterClass, baseRaceRandomizer, metaraceRandomizer) == false);

            return characterClass;
        }

        private Race GenerateRace(IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer, Dictionary<String, Int32> levelAdjustments,
            Alignment alignment, CharacterClass characterClass)
        {
            Race race;

            do race = raceGenerator.GenerateWith(alignment, characterClass, baseRaceRandomizer, metaraceRandomizer);
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= characterClass.Level);

            return race;
        }

        private Leadership GenerateLeadership(Character character)
        {
            var leadership = new Leadership();

            leadership.Score = character.Class.Level + character.Ability.Stats[StatConstants.Charisma].Bonus;

            var leadershipModifiers = new List<String>();
            var reputation = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Reputation);
            var leadershipAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers);

            if (String.IsNullOrEmpty(reputation) == false)
            {
                leadershipModifiers.Add(reputation);
                leadership.Score += leadershipAdjustments[reputation];
            }

            var cohortScore = leadership.Score;
            var cohortDeaths = 0;

            while (booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledCohort))
                cohortDeaths++;

            cohortScore -= cohortDeaths * 2;

            if (cohortDeaths > 0)
            {
                var modifier = String.Format("Caused the death of {0} cohort(s)", cohortDeaths);
                leadershipModifiers.Add(modifier);
            }

            if (String.IsNullOrEmpty(character.Magic.Familiar.Animal) == false)
                cohortScore -= 2;

            var followerScore = leadership.Score;
            var leaderMovement = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement);

            if (String.IsNullOrEmpty(leaderMovement) == false)
            {
                leadershipModifiers.Add(leaderMovement);
                followerScore += leadershipAdjustments[leaderMovement];
            }

            if (booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledFollowers))
            {
                leadershipModifiers.Add("Caused the death of followers");
                followerScore--;
            }

            leadership.Cohort = GenerateCohort(character, cohortScore);
            leadership.Followers = GenerateFollowers(character, followerScore);
            leadership.LeadershipModifiers = leadershipModifiers;

            return leadership;
        }

        private IEnumerable<Character> GenerateFollowers(Character leader, Int32 followerScore)
        {
            var followerQuantities = leadershipSelector.SelectFollowerQuantitiesFor(followerScore);

            var followers = new List<Character>();
            followers.AddRange(GenerateFollowers(1, followerQuantities.Level1, leader));
            followers.AddRange(GenerateFollowers(2, followerQuantities.Level2, leader));
            followers.AddRange(GenerateFollowers(3, followerQuantities.Level3, leader));
            followers.AddRange(GenerateFollowers(4, followerQuantities.Level4, leader));
            followers.AddRange(GenerateFollowers(5, followerQuantities.Level5, leader));
            followers.AddRange(GenerateFollowers(6, followerQuantities.Level6, leader));

            return followers;
        }

        private IEnumerable<Character> GenerateFollowers(Int32 level, Int32 quantity, Character leader)
        {
            var followers = new List<Character>();

            while (quantity-- > 0)
            {
                var follower = GenerateFollower(level, leader);
                followers.Add(follower);
            }

            return followers;
        }

        private Character GenerateCohort(Character leader, Int32 cohortScore)
        {
            var alignmentDiffers = booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment);
            if (alignmentDiffers)
                cohortScore--;

            var cohortLevel = leadershipSelector.SelectCohortLevelFor(cohortScore);
            cohortLevel = Math.Min(leader.Class.Level - 2, cohortLevel);

            if (cohortLevel <= 0)
                return null;

            if (alignmentDiffers == false)
            {
                setAlignmentRandomizer.SetAlignment = leader.Alignment;
                return GenerateFollower(setAlignmentRandomizer, cohortLevel, leader);
            }

            return GenerateFollower(cohortLevel, leader);
        }

        private Character GenerateFollower(IAlignmentRandomizer alignmentRandomizer, Int32 level, Character leader)
        {
            setLevelRandomizer.SetLevel = level;
            var allowedAlignments = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leader.Alignment.ToString());

            do setAlignmentRandomizer.SetAlignment = alignmentGenerator.GenerateWith(alignmentRandomizer);
            while (allowedAlignments.Contains(setAlignmentRandomizer.SetAlignment.ToString()) == false);

            return GenerateCharacter(setAlignmentRandomizer, anyClassNameRandomizer, setLevelRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer,
                rawStatsRandomizer);
        }

        private Character GenerateFollower(Int32 level, Character leader)
        {
            return GenerateFollower(anyAlignmentRandomizer, level, leader);
        }
    }
}