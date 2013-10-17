using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Alignments
{
    [TestFixture]
    public class NonEvilAlignmentVerifierTests : AlignmentVerifierTests
    {
        [SetUp]
        public void Setup()
        {
            verifier = new NonEvilAlignmentVerifier();
        }

        [Test]
        public void AnyClassNameRandomizerIsAllowed()
        {
            var randomizer = new AnyClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HealerClassRandomizerIsAllowed()
        {
            var randomizer = new HealerClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MageClassRandomizerIsAllowed()
        {
            var randomizer = new MageClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonSpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new SpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void StealthClassRandomizerIsAllowed()
        {
            var randomizer = new StealthClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WarriorClassRandomizerIsAllowed()
        {
            var randomizer = new WarriorClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void BarbarianIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Barbarian;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void BardIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Bard;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ClericIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Cleric;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void DruidIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Druid;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void FighterIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Fighter;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MonkIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Monk;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void PaladinIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Paladin;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void RangerIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Ranger;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void RogueIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Rogue;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SorcererIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Sorcerer;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WizardIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Wizard;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AnyBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new AnyBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void EvilBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new EvilBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GoodBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new GoodBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NeutralBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NeutralBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonEvilBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonEvilBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonGoodBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonGoodBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonNeutralBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonNeutralBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonStandardBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new NonStandardBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void StandardBaseRaceRandomizerIsAllowed()
        {
            var randomizer = new StandardBaseRaceRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AasimarIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Aasimar;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void BugbearIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Bugbear;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void DeepDwarfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.DeepDwarf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void DeepHalflingIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.DeepHalfling;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void DerroDwarfIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.DerroDwarf;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void DoppelgangerIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Doppelganger;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void DrowIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Drow;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void DuergarIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Duergar;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void ForestGnomeIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.ForestGnome;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GnollIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Gnoll;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void GoblinIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Goblin;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void GrayElfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.GrayElf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HalfElfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.HalfElf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HalfOrcIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.HalfOrc;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HighElfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.HighElf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HillDwarfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.HillDwarf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HobgoblinIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Hobgoblin;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void HumanIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Human;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void KoboldIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Kobold;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void LightfootHalflingIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.LightfootHalfling;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void LizardfolkIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Lizardfolk;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MindFlayerIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.MindFlayer;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void MinotaurIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Minotaur;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void MountainDwarfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.MountainDwarf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void OgreIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Ogre;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void OgreMageIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.OgreMage;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void OrcIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Orc;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void RockGnomeIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.RockGnome;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SvirfneblinIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Svirfneblin;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void TallfellowHalflingIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.TallfellowHalfling;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void TieflingIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Tiefling;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void TroglodyteIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Troglodyte;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void WildElfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.WildElf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WoodElfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.WoodElf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedAnyMetaraceRandomizerIsAllowed()
        {
            var randomizer = new AnyMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedAnyMetaraceRandomizerIsAllowed()
        {
            var randomizer = new AnyMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new EvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new EvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedGeneticMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GeneticMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedGeneticMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GeneticMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new GoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedLycanthropeMetaraceRandomizerIsAllowed()
        {
            var randomizer = new LycanthropeMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedLycanthropeMetaraceRandomizerIsAllowed()
        {
            var randomizer = new LycanthropeMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNeutralMetaraceRandomizerIsNotAllowed()
        {
            var randomizer = new NeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void AllowedNonEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNonEvilMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NoMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NoMetaraceRandomizer();
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNonGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonGoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNonGoodMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonGoodMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void AllowedNonNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = true;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForcedNonNeutralMetaraceRandomizerIsAllowed()
        {
            var randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.AllowNoMetarace = false;
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HalfCelestialIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.HalfCelestial;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HalfDragonIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.HalfDragon;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HalfFiendIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.HalfFiend;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void WerebearIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Werebear;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WereboarIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Wereboar;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WereratIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Wererat;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void WeretigerIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Weretiger;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WerewolfIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Werewolf;

            AssertRandomizerIsNotAllowed(randomizer);
        }
    }
}