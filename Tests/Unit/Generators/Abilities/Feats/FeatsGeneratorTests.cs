using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Domain.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private IFeatsGenerator featsGenerator;
        private Mock<IRacialFeatsGenerator> mockRacialFeatsGenerator;
        private Mock<IClassFeatsGenerator> mockClassFeatsGenerator;
        private Mock<IAdditionalFeatsGenerator> mockAdditionalFeatsGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private BaseAttack baseAttack;
        private List<Feat> racialFeats;

        [SetUp]
        public void Setup()
        {
            mockRacialFeatsGenerator = new Mock<IRacialFeatsGenerator>();
            mockClassFeatsGenerator = new Mock<IClassFeatsGenerator>();
            mockAdditionalFeatsGenerator = new Mock<IAdditionalFeatsGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            featsGenerator = new FeatsGenerator(mockRacialFeatsGenerator.Object, mockClassFeatsGenerator.Object, mockAdditionalFeatsGenerator.Object,
                mockCollectionsSelector.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            baseAttack = new BaseAttack();
            racialFeats = new List<Feat>();

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race, skills, stats)).Returns(racialFeats);
        }

        [Test]
        public void GetRacialFeats()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "racialFeat1";
            racialFeats[0].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 42;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Name = "racialFeat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("racialFeat1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("racialFeat2"));
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
            classFeats[0].Name = "classFeat1";
            classFeats[0].Strength = 9266;
            classFeats[1].Frequency.Quantity = 42;
            classFeats[1].Frequency.TimePeriod = "fortnight";
            classFeats[1].Name = "classFeat2";

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, racialFeats, skills)).Returns(classFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("classFeat1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("classFeat2"));
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
            additionalFeats[0].Name = "feat1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[1].Frequency.Quantity = 42;
            additionalFeats[1].Frequency.TimePeriod = "fortnight";
            additionalFeats[1].Name = "feat2";

            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, It.IsAny<IEnumerable<Feat>>())).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllFeats()
        {
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "racialFeat1";
            racialFeats[0].Strength = 9266;

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "other focus";
            additionalFeats[0].Name = "feat1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 42;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Name = "classFeat1";
            classFeats[0].Strength = 9266;

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, racialFeats, skills)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, It.IsAny<IEnumerable<Feat>>())).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void IfIdAndFocusAndStrengthAndFrequencyTimePeriodAreEqual_CombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[2].Name = "feat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(6));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfIdsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat2";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFociDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 4;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus2";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 2;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(4));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus2"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(2));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfStrengthsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 3;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 42;
            racialFeats[1].Frequency.Quantity = 3;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFrequencyTimePeriodsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "day";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("day"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyTimePeriods_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            //INFO: There is still only 1 feat here because strengths will filter out the duplicate
            var first = feats.Single();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void IfIdsAndFocusAndStrengthAreEqual_ConstantWinsOut()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 5;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.Day;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.Constant;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void IfIdsAndFocusAndStrengthAreEqual_AtWillWinsOut()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 5;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.Day;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.AtWill;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.AtWill));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ConstantFrequencyBeatsAtWillFrequency()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 0;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.AtWill;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.Constant;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void IfNoFrequencyAndRestMatchesButUnequalStrengths_KeepHigherStrength()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[2].Name = "feat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfHasFrequencyAndRestMatchesButUnequalStrengths_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[0].Frequency.Quantity = 0;
            racialFeats[0].Frequency.TimePeriod = "time period";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = "time period";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyButStrengthsAndIdsDoNotMatch_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat2";
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyButStrengthAndFociDoNotMatch_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus2";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Focus, Is.EqualTo("focus2"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyAndFeatIdMatchAndFociMatchAndEqualStrength_RemoveDuplicateStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 42;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FeatsThatCanBeTakenMultipleTimesAreAllowed()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[1].Name = "feat1";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "feat1" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Count(), Is.EqualTo(2));
            Assert.That(feats.First().Name, Is.EqualTo("feat1"));
            Assert.That(feats.Last().Name, Is.EqualTo("feat1"));
        }

        [Test]
        public void IfFeatHasFocusOfAll_RemoveOtherInstancesOfTheFeat()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Focus = "focus";
            racialFeats[1].Name = "feat1";
            racialFeats[1].Focus = FeatConstants.Foci.All;
            racialFeats[2].Name = "feat2";
            racialFeats[3].Name = "feat3";
            racialFeats[3].Focus = "focus";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var feat1 = feats.First(f => f.Name == "feat1");
            var feat2 = feats.First(f => f.Name == "feat2");
            var feat3 = feats.First(f => f.Name == "feat3");

            Assert.That(feat1.Name, Is.EqualTo("feat1"));
            Assert.That(feat1.Focus, Is.EqualTo(FeatConstants.Foci.All));
            Assert.That(feat2.Name, Is.EqualTo("feat2"));
            Assert.That(feat2.Focus, Is.Empty);
            Assert.That(feat3.Name, Is.EqualTo("feat3"));
            Assert.That(feat3.Focus, Is.EqualTo("focus"));
            Assert.That(feats.Count(), Is.EqualTo(3));
        }
    }
}