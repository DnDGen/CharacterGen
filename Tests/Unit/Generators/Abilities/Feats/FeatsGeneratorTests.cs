using System;
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
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
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
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<INameSelector> mockNameSelector;
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
            mockNameSelector = new Mock<INameSelector>();
            featsGenerator = new FeatsGenerator(mockRacialFeatsGenerator.Object, mockClassFeatsGenerator.Object, mockAdditionalFeatsGenerator.Object,
                mockCollectionsSelector.Object, mockNameSelector.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            baseAttack = new BaseAttack();
            racialFeats = new List<Feat>();

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race)).Returns(racialFeats);
        }

        [Test]
        public void GetRacialFeats()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "racialFeat1";
            racialFeats[0].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 42;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Name.Id = "racialFeat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("racialFeat1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("racialFeat2"));
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
            classFeats[0].Strength = 9266;
            classFeats[1].Frequency.Quantity = 42;
            classFeats[1].Frequency.TimePeriod = "fortnight";
            classFeats[1].Name.Id = "classFeat2";

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, stats, racialFeats)).Returns(classFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("classFeat1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("classFeat2"));
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
            additionalFeats[0].Strength = 9266;
            additionalFeats[1].Frequency.Quantity = 42;
            additionalFeats[1].Frequency.TimePeriod = "fortnight";
            additionalFeats[1].Name.Id = "feat2";

            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, It.IsAny<IEnumerable<Feat>>())).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("feat2"));
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

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, stats, racialFeats)).Returns(classFeats);
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
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[2].Name.Id = "feat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(6));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name.Id, Is.EqualTo("feat2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfIdsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat2";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat2"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFociDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 4;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus2";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 2;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(4));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus2"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(2));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfStrengthsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 3;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 42;
            racialFeats[1].Frequency.Quantity = 3;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFrequencyTimePeriodsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "day";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("day"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyTimePeriods_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            //INFO: There is still only 1 feat here because strengths will filter out the duplicate
            var first = feats.Single();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void IfIdsAndFocusAndStrengthAreEqual_ConstantWinsOut()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 5;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.Day;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.Constant;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void IfIdsAndFocusAndStrengthAreEqual_AtWillWinsOut()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 5;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.Day;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.AtWill;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.AtWill));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ConstantFrequencyBeatsAtWillFrequency()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 0;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.AtWill;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.Constant;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void IfNoFrequencyRestMatchesButUnequalStrengths_KeepHigherStrength()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[2].Name.Id = "feat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name.Id, Is.EqualTo("feat2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfHasFrequencyAndRestMatchesButUnequalStrengths_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[0].Frequency.Quantity = 0;
            racialFeats[0].Frequency.TimePeriod = "time period";
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = "time period";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyButStrengthsAndIdsDoNotMatch_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat2";
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Focus, Is.EqualTo("focus"));
            Assert.That(last.Name.Id, Is.EqualTo("feat2"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyButStrengthAndFociDoNotMatch_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus2";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Focus, Is.EqualTo("focus2"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyAndFeatIdMatchAndFociMatchAndEqualStrength_RemoveDuplicateStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Focus = "focus";
            racialFeats[1].Name.Id = "feat1";
            racialFeats[1].Strength = 42;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetNamesForFeats()
        {
            racialFeats.Add(new Feat());

            racialFeats[0].Focus = "focus";
            racialFeats[0].Name.Id = "racialFeat1";
            racialFeats[0].Strength = 9266;

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Focus = "other focus";
            additionalFeats[0].Name.Id = "feat1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 42;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Name.Id = "classFeat1";
            classFeats[0].Strength = 9266;

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, stats, racialFeats)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, It.IsAny<IEnumerable<Feat>>())).Returns(additionalFeats);

            mockNameSelector.Setup(s => s.Select("racialFeat1")).Returns("Racial feat 1");
            mockNameSelector.Setup(s => s.Select("feat1")).Returns("Additional feat 1");
            mockNameSelector.Setup(s => s.Select("classFeat1")).Returns("Class feat 1");

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.First(f => f.Name.Id == "racialFeat1").Name.Name, Is.EqualTo("Racial feat 1"));
            Assert.That(feats.First(f => f.Name.Id == "feat1").Name.Name, Is.EqualTo("Additional feat 1"));
            Assert.That(feats.First(f => f.Name.Id == "classFeat1").Name.Name, Is.EqualTo("Class feat 1"));
        }

        [Test]
        public void FeatsThatCanBeTakenMultipleTimesAreAllowed()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name.Id = "feat1";
            racialFeats[1].Name.Id = "feat1";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "feat1" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Count(), Is.EqualTo(2));
            Assert.That(feats.First().Name.Id, Is.EqualTo("feat1"));
            Assert.That(feats.Last().Name.Id, Is.EqualTo("feat1"));
        }
    }
}