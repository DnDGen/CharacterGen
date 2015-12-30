﻿using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class SavingThrowsGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private ISavingThrowsGenerator savingThrowsGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private Dictionary<string, Stat> stats;
        private List<string> allSaveFeats;
        private List<string> reflexSaveFeats;
        private List<string> fortitudeSaveFeats;
        private List<string> willSaveFeats;
        private List<string> strongReflex;
        private List<string> strongFortitude;
        private List<string> strongWill;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            savingThrowsGenerator = new SavingThrowsGenerator(mockCollectionsSelector.Object);
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            stats = new Dictionary<string, Stat>();
            allSaveFeats = new List<string>();
            reflexSaveFeats = new List<string>();
            fortitudeSaveFeats = new List<string>();
            willSaveFeats = new List<string>();
            strongFortitude = new List<string>();
            strongReflex = new List<string>();
            strongWill = new List<string>();

            stats[StatConstants.Constitution] = new Stat { Value = 10 };
            stats[StatConstants.Dexterity] = new Stat { Value = 10 };
            stats[StatConstants.Wisdom] = new Stat { Value = 10 };
            characterClass.ClassName = "class name";
            characterClass.Level = 600;
            allSaveFeats.Add("other feat");
            reflexSaveFeats.Add("other feat");
            fortitudeSaveFeats.Add("other feat");
            willSaveFeats.Add("other feat");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SavingThrows))
                .Returns(allSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, SavingThrowConstants.Fortitude))
                .Returns(fortitudeSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, SavingThrowConstants.Reflex))
                .Returns(reflexSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, SavingThrowConstants.Will))
                .Returns(willSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SavingThrowConstants.Fortitude))
                .Returns(strongFortitude);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SavingThrowConstants.Reflex))
                .Returns(strongReflex);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SavingThrowConstants.Will))
                .Returns(strongWill);
        }

        [Test]
        public void ApplyStatBonuses()
        {
            stats[StatConstants.Constitution].Value = 9266;
            stats[StatConstants.Dexterity].Value = 90210;
            stats[StatConstants.Wisdom].Value = -42;

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(4828));
            Assert.That(savingThrows.Reflex, Is.EqualTo(45300));
            Assert.That(savingThrows.Will, Is.EqualTo(174));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 4)]
        [TestCase(6, 5)]
        [TestCase(7, 5)]
        [TestCase(8, 6)]
        [TestCase(9, 6)]
        [TestCase(10, 7)]
        [TestCase(11, 7)]
        [TestCase(12, 8)]
        [TestCase(13, 8)]
        [TestCase(14, 9)]
        [TestCase(15, 9)]
        [TestCase(16, 10)]
        [TestCase(17, 10)]
        [TestCase(18, 11)]
        [TestCase(19, 11)]
        [TestCase(20, 12)]
        public void StrongSaveBonuses(int level, int saveBonus)
        {
            characterClass.Level = level;
            strongFortitude.Add(characterClass.ClassName);

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(saveBonus));
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(7, 2)]
        [TestCase(8, 2)]
        [TestCase(9, 3)]
        [TestCase(10, 3)]
        [TestCase(11, 3)]
        [TestCase(12, 4)]
        [TestCase(13, 4)]
        [TestCase(14, 4)]
        [TestCase(15, 5)]
        [TestCase(16, 5)]
        [TestCase(17, 5)]
        [TestCase(18, 6)]
        [TestCase(19, 6)]
        [TestCase(20, 6)]
        public void WeakSaveBonuses(int level, int saveBonus)
        {
            characterClass.Level = level;
            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(saveBonus));
        }

        [Test]
        public void ApplyStrongCharacterClassFortitudeBonus()
        {
            strongFortitude.Add(characterClass.ClassName);

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(302));
            Assert.That(savingThrows.Reflex, Is.EqualTo(200));
            Assert.That(savingThrows.Will, Is.EqualTo(200));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [Test]
        public void ApplyWeakCharacterClassFortitudeBonus()
        {
            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(200));
            Assert.That(savingThrows.Reflex, Is.EqualTo(200));
            Assert.That(savingThrows.Will, Is.EqualTo(200));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [Test]
        public void ApplyStrongCharacterClassReflexBonus()
        {
            strongReflex.Add(characterClass.ClassName);

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(200));
            Assert.That(savingThrows.Reflex, Is.EqualTo(302));
            Assert.That(savingThrows.Will, Is.EqualTo(200));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [Test]
        public void ApplyWeakCharacterClassReflexBonus()
        {
            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(200));
            Assert.That(savingThrows.Reflex, Is.EqualTo(200));
            Assert.That(savingThrows.Will, Is.EqualTo(200));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [Test]
        public void ApplyStrongCharacterClassWillBonus()
        {
            strongWill.Add(characterClass.ClassName);

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(200));
            Assert.That(savingThrows.Reflex, Is.EqualTo(200));
            Assert.That(savingThrows.Will, Is.EqualTo(302));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [Test]
        public void ApplyWeakCharacterClassWillBonus()
        {
            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(200));
            Assert.That(savingThrows.Reflex, Is.EqualTo(200));
            Assert.That(savingThrows.Will, Is.EqualTo(200));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        [Test]
        public void ApplyFeatBonuses()
        {
            SetUpFeats();

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(208));
            Assert.That(savingThrows.Reflex, Is.EqualTo(210));
            Assert.That(savingThrows.Will, Is.EqualTo(212));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }

        private void SetUpFeats()
        {
            for (var i = 0; i < 8; i++)
            {
                var feat = new Feat();
                feat.Name = string.Format("Feat{0}", i);
                feat.Strength = i + 1;

                feats.Add(feat);
            }

            feats[0].Foci = new[] { FeatConstants.Foci.All };
            feats[1].Foci = new[] { SavingThrowConstants.Fortitude };
            feats[2].Foci = new[] { SavingThrowConstants.Reflex };
            feats[3].Foci = new[] { SavingThrowConstants.Will };

            allSaveFeats.Add(feats[0].Name);
            allSaveFeats.Add(feats[1].Name);
            allSaveFeats.Add(feats[2].Name);
            allSaveFeats.Add(feats[3].Name);
            fortitudeSaveFeats.Add(feats[4].Name);
            reflexSaveFeats.Add(feats[5].Name);
            willSaveFeats.Add(feats[6].Name);
        }

        [Test]
        public void CircumstantialBonusIfFeatHaveQualifications()
        {
            SetUpFeats();

            feats[0].Foci = new[] { FeatConstants.Foci.All + " against thing" };
            feats[1].Foci = new[] { SavingThrowConstants.Fortitude + " against thing" };
            feats[2].Foci = new[] { SavingThrowConstants.Reflex + " against thing" };
            feats[3].Foci = new[] { SavingThrowConstants.Will + " against thing" };

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(205));
            Assert.That(savingThrows.Reflex, Is.EqualTo(206));
            Assert.That(savingThrows.Will, Is.EqualTo(207));
            Assert.That(savingThrows.CircumstantialBonus, Is.True);
        }

        [Test]
        public void DoNotOverwriteCircumstantialBonus()
        {
            SetUpFeats();

            feats[0].Foci = new[] { FeatConstants.Foci.All + " against thing" };

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(207));
            Assert.That(savingThrows.Reflex, Is.EqualTo(209));
            Assert.That(savingThrows.Will, Is.EqualTo(211));
            Assert.That(savingThrows.CircumstantialBonus, Is.True);
        }

        [Test]
        public void ApplyAllBonuses()
        {
            stats[StatConstants.Constitution].Value = 9266;
            stats[StatConstants.Dexterity].Value = 90210;
            stats[StatConstants.Wisdom].Value = -42;

            strongWill.Add(characterClass.ClassName);
            strongFortitude.Add(characterClass.ClassName);

            SetUpFeats();

            var savingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);
            Assert.That(savingThrows.Fortitude, Is.EqualTo(4938));
            Assert.That(savingThrows.Reflex, Is.EqualTo(45310));
            Assert.That(savingThrows.Will, Is.EqualTo(288));
            Assert.That(savingThrows.CircumstantialBonus, Is.False);
        }
    }
}
