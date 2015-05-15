﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private IFeatsGenerator featsGenerator;
        private Mock<IRacialFeatsGenerator> mockRacialFeatsGenerator;
        private Mock<IClassFeatsGenerator> mockClassFeatsGenerator;
        private Mock<IAdditionalFeatsGenerator> mockAdditionalFeatsGenerator;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private BaseAttack baseAttack;

        [SetUp]
        public void Setup()
        {
            mockRacialFeatsGenerator = new Mock<IRacialFeatsGenerator>();
            mockClassFeatsGenerator = new Mock<IClassFeatsGenerator>();
            mockAdditionalFeatsGenerator = new Mock<IAdditionalFeatsGenerator>();
            featsGenerator = new FeatsGenerator(mockRacialFeatsGenerator.Object, mockClassFeatsGenerator.Object, mockAdditionalFeatsGenerator.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            baseAttack = new BaseAttack();
        }

        [Test]
        public void GetRacialFeats()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "racialFeat1";
            racialFeats[0].Name.Name = "racial feat 1";
            racialFeats[0].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 42;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Name.Id = "racialFeat2";
            racialFeats[1].Name.Name = "racial feat 2";

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("racialFeat1"));
            Assert.That(first.Name.Name, Is.EqualTo("racial feat 1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("racialFeat2"));
            Assert.That(last.Name.Name, Is.EqualTo("racial feat 2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeats()
        {
            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());
            classFeats.Add(new Feat());

            classFeats[0].Focus = "focus";
            classFeats[0].Name.Id = "classFeat1";
            classFeats[0].Name.Name = "class feat 1";
            classFeats[0].Strength = 9266;
            classFeats[1].Frequency.Quantity = 42;
            classFeats[1].Frequency.TimePeriod = "fortnight";
            classFeats[1].Name.Id = "classFeat2";
            classFeats[1].Name.Name = "class feat 2";

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("classFeat1"));
            Assert.That(first.Name.Name, Is.EqualTo("class feat 1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("classFeat2"));
            Assert.That(last.Name.Name, Is.EqualTo("class feat 2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[1].Frequency.Quantity = 42;
            additionalFeats[1].Frequency.TimePeriod = "fortnight";
            additionalFeats[1].Name.Id = "feat2";
            additionalFeats[1].Name.Name = "feat 2";

            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("feat2"));
            Assert.That(last.Name.Name, Is.EqualTo("feat 2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllFeats()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "racialFeat1";
            racialFeats[0].Name.Name = "racial feat 1";
            racialFeats[0].Strength = 9266;

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "other focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 42;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Name.Id = "classFeat1";
            classFeats[0].Name.Name = "class feat 1";
            classFeats[0].Strength = 9266;

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void IfIdAndFocusAndStrengthAndFrequencyTimePeriodAreEqual_CombineFrequencyQuantities()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Name.Name = "feat 1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 2;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Focus = "focus";
            classFeats[0].Name.Id = "feat1";
            classFeats[0].Name.Name = "feat 1";
            classFeats[0].Strength = 9266;
            classFeats[0].Frequency.Quantity = 3;
            classFeats[0].Frequency.TimePeriod = "fortnight";

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Focus, Is.EqualTo("focus"));
            Assert.That(onlyFeat.Frequency.Quantity, Is.EqualTo(6));
            Assert.That(onlyFeat.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(onlyFeat.Name.Id, Is.EqualTo("feat1"));
            Assert.That(onlyFeat.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(onlyFeat.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void IfIdsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat2";
            racialFeats[0].Name.Name = "feat 1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 2;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Focus = "focus";
            classFeats[0].Name.Id = "feat1";
            classFeats[0].Name.Name = "feat 1";
            classFeats[0].Strength = 9266;
            classFeats[0].Frequency.Quantity = 3;
            classFeats[0].Frequency.TimePeriod = "fortnight";

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat2"));
            Assert.That(first.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFociDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Name.Name = "feat 1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "focus 2";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 2;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Focus = "focus";
            classFeats[0].Name.Id = "feat1";
            classFeats[0].Name.Name = "feat 1";
            classFeats[0].Strength = 9266;
            classFeats[0].Frequency.Quantity = 3;
            classFeats[0].Frequency.TimePeriod = "fortnight";

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(4));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat2"));
            Assert.That(first.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus 2"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(2));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfStrengthsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Name.Name = "feat 1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 2;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Focus = "focus";
            classFeats[0].Name.Id = "feat1";
            classFeats[0].Name.Name = "feat 1";
            classFeats[0].Strength = 42;
            classFeats[0].Frequency.Quantity = 3;
            classFeats[0].Frequency.TimePeriod = "fortnight";

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat2"));
            Assert.That(first.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus 2"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(last.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFrequencyTimePeriodsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            var racialFeats = new List<Feat>();
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Name.Name = "feat 1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "day";

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Name.Name = "feat 1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 2;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Focus = "focus";
            classFeats[0].Name.Id = "feat1";
            classFeats[0].Name.Name = "feat 1";
            classFeats[0].Strength = 42;
            classFeats[0].Frequency.Quantity = 3;
            classFeats[0].Frequency.TimePeriod = "fortnight";

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("day"));
            Assert.That(first.Name.Id, Is.EqualTo("feat2"));
            Assert.That(first.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus 2"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Name.Name, Is.EqualTo("feat 1"));
            Assert.That(last.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfIdsAndFocusAndStrengthAreEqual_ConstantWinsOut()
        {
            Assert.Fail();
        }

        [Test]
        public void IfIdsAndFocusAndStrengthAreEqual_AtWillWinsOut()
        {
            Assert.Fail();
        }

        [Test]
        public void ConstantBeatsAtWill()
        {
            Assert.Fail();
        }
    }
}