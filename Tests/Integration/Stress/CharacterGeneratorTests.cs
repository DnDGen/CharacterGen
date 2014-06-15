using System;
using System.Linq;
using EquipmentGen.Common.Items;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterFactory { get; set; }
        [Inject]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public ILevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public IStatsRandomizer StatsRandomizer { get; set; }

        [Test]
        public void StressCharacterGenerator()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();
            var classes = CharacterClassConstants.GetClassNames();
            var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
            var metaraces = RaceConstants.Metaraces.GetMetaraces();

            while (TestShouldKeepRunning())
            {
                var character = CharacterFactory.CreateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer,
                    MetaraceRandomizer, StatsRandomizer);

                Assert.That(goodnesses, Contains.Item(character.Alignment.Goodness));
                Assert.That(lawfulnesses, Contains.Item(character.Alignment.Lawfulness));
                Assert.That(classes, Contains.Item(character.Class.ClassName));
                Assert.That(character.Class.Level, Is.GreaterThan(0));
                Assert.That(character.ArmorClass, Is.AtLeast(5));
                Assert.That(character.Class.BaseAttack.BaseAttackBonus, Is.AtLeast(0));
                Assert.That(character.Familiar, Is.Not.Null);
                Assert.That(character.Feats.Count(), Is.AtLeast(1));
                Assert.That(character.HitPoints, Is.AtLeast(character.Class.Level));
                Assert.That(character.InterestingTrait, Is.Not.Null);
                Assert.That(character.Languages.Count(), Is.AtLeast(1));
                Assert.That(baseRaces, Contains.Item(character.Race.BaseRace));
                Assert.That(metaraces, Contains.Item(character.Race.Metarace));
                Assert.That(character.Skills.Count(), Is.AtLeast(1));
                Assert.That(character.Stats.Count, Is.EqualTo(6));
                Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Charisma));
                Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Constitution));
                Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Dexterity));
                Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Intelligence));
                Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Strength));
                Assert.That(character.Stats.Keys, Contains.Item(StatConstants.Wisdom));
                Assert.That(character.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
                Assert.That(character.Armor.Name, Is.Not.Empty);
                Assert.That(character.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
                Assert.That(character.PrimaryHand.Name, Is.Not.Empty);

                if (!String.IsNullOrEmpty(character.OffHand.Name))
                {
                    Assert.That(character.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Armor).Or.EqualTo(ItemTypeConstants.Weapon));

                    if (character.OffHand.ItemType == ItemTypeConstants.Armor)
                        Assert.That(character.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
                }

                foreach (var level in character.Spells.Keys)
                    Assert.That(character.Spells[level], Is.Not.Empty, level.ToString());
            }

            AssertIterations();
        }
    }
}