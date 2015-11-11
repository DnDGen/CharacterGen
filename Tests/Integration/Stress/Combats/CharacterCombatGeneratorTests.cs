using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class CharacterCombatGeneratorTests : StressTests
    {
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator TreasureGenerator { get; set; }

        [TearDown]
        public void TearDown()
        {
            LevelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            StatsRandomizer = GetNewInstanceOf<IStatsRandomizer>(StatsRandomizerTypeConstants.Raw);
        }

        [TestCase("CharacterCombatGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);

            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.Not.Negative);

            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = TreasureGenerator.GenerateWith(ability.Feats, characterClass, race);

            var combat = CombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);
            Assert.That(combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(combat.ArmorClass.Full, Is.Positive);
            Assert.That(combat.ArmorClass.Touch, Is.Positive);
            Assert.That(combat.HitPoints, Is.AtLeast(characterClass.Level));
            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
            Assert.That(combat.AdjustedDexterityBonus, Is.AtMost(ability.Stats[StatConstants.Dexterity].Bonus));
        }

        [Test]
        public void InitiativeBonusTakesImprovedInitiativeFeatIntoAccount()
        {
            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = 1;
            LevelRandomizer = setLevelRandomizer;

            var setBaseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            setBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Human;
            BaseRaceRandomizer = setBaseRaceRandomizer;

            var setStatsRandomizer = GetNewInstanceOf<ISetStatsRandomizer>();
            setStatsRandomizer.SetCharisma = 10;
            setStatsRandomizer.SetConstitution = 10;
            setStatsRandomizer.SetDexterity = 10;
            setStatsRandomizer.SetIntelligence = 10;
            setStatsRandomizer.SetStrength = 10;
            setStatsRandomizer.SetWisdom = 10;
            StatsRandomizer = setStatsRandomizer;

            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);

            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.Not.Negative);

            var ability = Generate(() => AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack),
                a => a.Feats.Any(f => f.Name == FeatConstants.ImprovedInitiative));

            var equipment = TreasureGenerator.GenerateWith(ability.Feats, characterClass, race);

            var combat = CombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);

            Assert.That(combat.InitiativeBonus, Is.EqualTo(4));
        }
    }
}