﻿using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using DnDGen.Core.Selectors.Collections;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CharacterGen.Tests.Integration.Stress.Verifiers
{
    [TestFixture]
    public class RandomizerVerifierTests : StressTests
    {
        [Inject]
        public ICollectionSelector CollectionsSelector { get; set; }
        [Inject]
        public Random Random { get; set; }
        [Inject]
        public Stopwatch Stopwatch { get; set; }

        private const string Set = "Set";

        private IEnumerable<string> alignmentRandomizers;
        private IEnumerable<string> alignments;
        private IEnumerable<string> classNameRandomizers;
        private IEnumerable<string> classNames;
        private IEnumerable<string> levelRandomizers;
        private IEnumerable<string> baseRaceRandomizers;
        private IEnumerable<string> baseRaces;
        private IEnumerable<string> metaraceRandomizers;
        private IEnumerable<string> metaraces;
        private TimeSpan timeLimit;

        [SetUp]
        public void Setup()
        {
            alignmentRandomizers = new[]
            {
                AlignmentRandomizerTypeConstants.Any,
                AlignmentRandomizerTypeConstants.Chaotic,
                AlignmentRandomizerTypeConstants.Evil,
                AlignmentRandomizerTypeConstants.Good,
                AlignmentRandomizerTypeConstants.Lawful,
                AlignmentRandomizerTypeConstants.Neutral,
                AlignmentRandomizerTypeConstants.NonChaotic,
                AlignmentRandomizerTypeConstants.NonEvil,
                AlignmentRandomizerTypeConstants.NonGood,
                AlignmentRandomizerTypeConstants.NonLawful,
                AlignmentRandomizerTypeConstants.NonNeutral,
                Set,
            };

            alignments = new[]
            {
                AlignmentConstants.ChaoticEvil,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.LawfulGood,
                AlignmentConstants.LawfulNeutral,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.TrueNeutral,
            };

            classNameRandomizers = new[]
            {
                ClassNameRandomizerTypeConstants.AnyNPC,
                ClassNameRandomizerTypeConstants.AnyPlayer,
                ClassNameRandomizerTypeConstants.ArcaneSpellcaster,
                ClassNameRandomizerTypeConstants.DivineSpellcaster,
                ClassNameRandomizerTypeConstants.NonSpellcaster,
                ClassNameRandomizerTypeConstants.PhysicalCombat,
                ClassNameRandomizerTypeConstants.Spellcaster,
                ClassNameRandomizerTypeConstants.Stealth,
                Set,
            };

            classNames = new[]
            {
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Warrior,
                CharacterClassConstants.Wizard,
            };

            levelRandomizers = new[]
            {
                LevelRandomizerTypeConstants.Any,
                LevelRandomizerTypeConstants.High,
                LevelRandomizerTypeConstants.Low,
                LevelRandomizerTypeConstants.Medium,
                LevelRandomizerTypeConstants.VeryHigh,
                Set,
            };

            baseRaceRandomizers = new[]
            {
                RaceRandomizerTypeConstants.BaseRace.AnyBase,
                RaceRandomizerTypeConstants.BaseRace.AquaticBase,
                RaceRandomizerTypeConstants.BaseRace.MonsterBase,
                RaceRandomizerTypeConstants.BaseRace.NonMonsterBase,
                RaceRandomizerTypeConstants.BaseRace.NonStandardBase,
                RaceRandomizerTypeConstants.BaseRace.StandardBase,
                Set,
            };

            baseRaces = new[]
            {
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.AquaticElf,
                RaceConstants.BaseRaces.Azer,
                RaceConstants.BaseRaces.BlueSlaad,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.Centaur,
                RaceConstants.BaseRaces.CloudGiant,
                RaceConstants.BaseRaces.DeathSlaad,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.FireGiant,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.FrostGiant,
                RaceConstants.BaseRaces.Gargoyle,
                RaceConstants.BaseRaces.Githyanki,
                RaceConstants.BaseRaces.Githzerai,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.BaseRaces.GraySlaad,
                RaceConstants.BaseRaces.GreenSlaad,
                RaceConstants.BaseRaces.Grimlock,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.Harpy,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.HillGiant,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.HoundArchon,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Janni,
                RaceConstants.BaseRaces.Kapoacinth,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.KuoToa,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.Locathah,
                RaceConstants.BaseRaces.Merfolk,
                RaceConstants.BaseRaces.Merrow,
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.Pixie,
                RaceConstants.BaseRaces.Rakshasa,
                RaceConstants.BaseRaces.RedSlaad,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Sahuagin,
                RaceConstants.BaseRaces.Satyr,
                RaceConstants.BaseRaces.Scorpionfolk,
                RaceConstants.BaseRaces.Scrag,
                RaceConstants.BaseRaces.StoneGiant,
                RaceConstants.BaseRaces.StormGiant,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.BaseRaces.Troll,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.YuanTiAbomination,
                RaceConstants.BaseRaces.YuanTiHalfblood,
                RaceConstants.BaseRaces.YuanTiPureblood,
            };

            metaraceRandomizers = new[]
            {
                RaceRandomizerTypeConstants.Metarace.AnyMeta,
                RaceRandomizerTypeConstants.Metarace.GeneticMeta,
                RaceRandomizerTypeConstants.Metarace.LycanthropeMeta,
                RaceRandomizerTypeConstants.Metarace.NoMeta,
                RaceRandomizerTypeConstants.Metarace.UndeadMeta,
                Set,
            };

            metaraces = new[]
            {
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Mummy,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Werewolf,
            };

            Stopwatch.Reset();
            timeLimit = new TimeSpan(TimeSpan.TicksPerSecond);
        }

        [Test]
        public void StressRandomizerVerification()
        {
            stressor.Stress(GenerateAndAssertRandomizers);
        }

        private void GenerateAndAssertRandomizers()
        {
            var alignmentRandomizerName = GetRandomAlignmentRandomizerName();
            var classNameRandomizerName = GetRandomClassNameRandomizerName();
            var levelRandomizerName = GetRandomLevelRandomizerName();
            var baseRaceRandomizerName = GetRandomBaseRaceRandomizerName();
            var metaraceRandomizerName = GetRandomMetaraceRandomizerName();

            var alignmentRandomizer = GetAlignmentRandomizer(alignmentRandomizerName);
            var classNameRandomizer = GetClassNameRandomizer(classNameRandomizerName);
            var levelRandomizer = GetLevelRandomizer(levelRandomizerName);
            var baseRaceRandomizer = GetBaseRaceRandomizer(baseRaceRandomizerName);
            var metaraceRandomizer = GetMetaraceRandomizer(metaraceRandomizerName);

            Stopwatch.Restart();
            var verified = RandomizerVerifier.VerifyCompatibility(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);
            Stopwatch.Stop();

            var message = $"{alignmentRandomizerName};{classNameRandomizerName};{levelRandomizerName};{baseRaceRandomizerName};{metaraceRandomizerName}";
            Assert.That(Stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        [Test]
        public void StressAlignmentVerification()
        {
            stressor.Stress(GenerateAndAssertAlignment);
        }

        private void GenerateAndAssertAlignment()
        {
            var alignmentPrototype = BuildRandomAlignmentPrototype();

            var classNameRandomizerName = GetRandomClassNameRandomizerName();
            var levelRandomizerName = GetRandomLevelRandomizerName();
            var baseRaceRandomizerName = GetRandomBaseRaceRandomizerName();
            var metaraceRandomizerName = GetRandomMetaraceRandomizerName();

            var classNameRandomizer = GetClassNameRandomizer(classNameRandomizerName);
            var levelRandomizer = GetLevelRandomizer(levelRandomizerName);
            var baseRaceRandomizer = GetBaseRaceRandomizer(baseRaceRandomizerName);
            var metaraceRandomizer = GetMetaraceRandomizer(metaraceRandomizerName);

            Stopwatch.Restart();
            var verified = RandomizerVerifier.VerifyAlignmentCompatibility(alignmentPrototype, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);
            Stopwatch.Stop();

            var message = $"{alignmentPrototype.Full};{classNameRandomizerName};{levelRandomizerName};{baseRaceRandomizerName};{metaraceRandomizerName}";
            Assert.That(Stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        private Alignment BuildRandomAlignmentPrototype()
        {
            var alignmentDescription = CollectionsSelector.SelectRandomFrom(alignments);
            var alignment = new Alignment(alignmentDescription);

            return alignment;
        }

        [Test]
        public void StressClassVerification()
        {
            stressor.Stress(GenerateAndAssertClass);
        }

        private void GenerateAndAssertClass()
        {
            var alignmentPrototype = BuildRandomAlignmentPrototype();
            var classPrototype = BuildRandomClassPrototype();

            var baseRaceRandomizerName = GetRandomBaseRaceRandomizerName();
            var metaraceRandomizerName = GetRandomMetaraceRandomizerName();

            var baseRaceRandomizer = GetBaseRaceRandomizer(baseRaceRandomizerName);
            var metaraceRandomizer = GetMetaraceRandomizer(metaraceRandomizerName);

            Stopwatch.Restart();
            var verified = RandomizerVerifier.VerifyCharacterClassCompatibility(alignmentPrototype, classPrototype, baseRaceRandomizer, metaraceRandomizer);
            Stopwatch.Stop();

            var message = $"{alignmentPrototype.Full};{classPrototype.Summary};{baseRaceRandomizerName};{metaraceRandomizerName}";
            Assert.That(Stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        private CharacterClassPrototype BuildRandomClassPrototype()
        {
            var classPrototype = new CharacterClassPrototype();
            classPrototype.Name = CollectionsSelector.SelectRandomFrom(classNames);
            classPrototype.Level = Random.Next(20) + 1;

            return classPrototype;
        }

        [Test]
        public void StressRaceVerification()
        {
            stressor.Stress(GenerateAndAssertRace);
        }

        private void GenerateAndAssertRace()
        {
            var alignmentPrototype = BuildRandomAlignmentPrototype();
            var classPrototype = BuildRandomClassPrototype();
            var racePrototype = BuildRandomRacePrototype();

            Stopwatch.Restart();
            var verified = RandomizerVerifier.VerifyRaceCompatibility(alignmentPrototype, classPrototype, racePrototype);
            Stopwatch.Stop();

            var message = $"{alignmentPrototype.Full};{classPrototype.Summary};{racePrototype.Summary}";
            Assert.That(Stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        private RacePrototype BuildRandomRacePrototype()
        {
            var racePrototype = new RacePrototype();
            racePrototype.BaseRace = CollectionsSelector.SelectRandomFrom(baseRaces);
            racePrototype.Metarace = CollectionsSelector.SelectRandomFrom(metaraces);

            return racePrototype;
        }

        private string GetRandomMetaraceRandomizerName()
        {
            var metaraceRandomizerName = CollectionsSelector.SelectRandomFrom(metaraceRandomizers);

            if (metaraceRandomizerName == RaceRandomizerTypeConstants.Metarace.NoMeta)
                return metaraceRandomizerName;

            if (metaraceRandomizerName != Set)
            {
                var force = Convert.ToBoolean(Random.Next(2));
                return $"{metaraceRandomizerName},,{force}";
            }

            var metarace = CollectionsSelector.SelectRandomFrom(metaraces);

            return $"{metaraceRandomizerName},{metarace}";
        }

        private RaceRandomizer GetMetaraceRandomizer(string name)
        {
            var sections = name.Split(',');

            if (sections.Length == 1)
                return GetNewInstanceOf<RaceRandomizer>(name);

            if (sections.Length == 3)
            {
                var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(sections[0]);
                var forceableMetaraceRandomizer = metaraceRandomizer as IForcableMetaraceRandomizer;
                forceableMetaraceRandomizer.ForceMetarace = Convert.ToBoolean(sections[2]);

                return forceableMetaraceRandomizer;
            }

            var setMetaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();
            setMetaraceRandomizer.SetMetarace = sections[1];

            return setMetaraceRandomizer;
        }

        private string GetRandomBaseRaceRandomizerName()
        {
            var baseRaceRandomizerName = CollectionsSelector.SelectRandomFrom(baseRaceRandomizers);
            if (baseRaceRandomizerName != Set)
                return baseRaceRandomizerName;

            var baseRace = CollectionsSelector.SelectRandomFrom(baseRaces);

            return $"{baseRaceRandomizerName},{baseRace}";
        }

        private RaceRandomizer GetBaseRaceRandomizer(string name)
        {
            var sections = name.Split(',');

            if (sections.Length == 1)
                return GetNewInstanceOf<RaceRandomizer>(name);

            var setBaseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            setBaseRaceRandomizer.SetBaseRace = sections[1];

            return setBaseRaceRandomizer;
        }

        private string GetRandomLevelRandomizerName()
        {
            var levelRandomizerName = CollectionsSelector.SelectRandomFrom(levelRandomizers);
            if (levelRandomizerName != Set)
                return levelRandomizerName;

            var level = Random.Next(20) + 1;

            return $"{levelRandomizerName},{level}";
        }

        private ILevelRandomizer GetLevelRandomizer(string name)
        {
            var sections = name.Split(',');

            if (sections.Length == 1)
                return GetNewInstanceOf<ILevelRandomizer>(name);

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = Convert.ToInt32(sections[1]);

            return setLevelRandomizer;
        }

        private string GetRandomClassNameRandomizerName()
        {
            var classNameRandomizerName = CollectionsSelector.SelectRandomFrom(classNameRandomizers);
            if (classNameRandomizerName != Set)
                return classNameRandomizerName;

            var className = CollectionsSelector.SelectRandomFrom(classNames);

            return $"{classNameRandomizerName},{className}";
        }

        private IClassNameRandomizer GetClassNameRandomizer(string name)
        {
            var sections = name.Split(',');

            if (sections.Length == 1)
                return GetNewInstanceOf<IClassNameRandomizer>(name);

            var setClassNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            setClassNameRandomizer.SetClassName = sections[1];

            return setClassNameRandomizer;
        }

        private string GetRandomAlignmentRandomizerName()
        {
            var alignmentRandomizerName = CollectionsSelector.SelectRandomFrom(alignmentRandomizers);
            if (alignmentRandomizerName != Set)
                return alignmentRandomizerName;

            var alignment = CollectionsSelector.SelectRandomFrom(alignments);
            return $"{alignmentRandomizerName},{alignment}";
        }

        private IAlignmentRandomizer GetAlignmentRandomizer(string name)
        {
            var sections = name.Split(',');

            if (sections.Length == 1)
                return GetNewInstanceOf<IAlignmentRandomizer>(name);

            var setAlignmentRandomizer = GetNewInstanceOf<ISetAlignmentRandomizer>();
            setAlignmentRandomizer.SetAlignment = new Alignment(sections[1]);

            return setAlignmentRandomizer;
        }
    }
}
