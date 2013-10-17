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
    public class EvilAlignmentVerifierTests : AlignmentVerifierTests
    {
        [SetUp]
        public void Setup()
        {
            verifier = new EvilAlignmentVerifier();
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
        public void PaladinIsNotAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Paladin;

            AssertRandomizerIsNotAllowed(randomizer);
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
        public void AasimarIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Aasimar;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void BugbearIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Bugbear;

            AssertRandomizerIsAllowed(randomizer);
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
        public void DerroDwarfIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.DerroDwarf;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void DoppelgangerIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Doppelganger;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void DrowIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Drow;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void DuergarIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Duergar;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void ForestGnomeIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.ForestGnome;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void GnollIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Gnoll;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GoblinIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Goblin;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void GrayElfIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.GrayElf;

            AssertRandomizerIsNotAllowed(randomizer);
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
        public void HobgoblinIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Hobgoblin;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HumanIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Human;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void KoboldIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Kobold;

            AssertRandomizerIsAllowed(randomizer);
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
        public void MindFlayerIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.MindFlayer;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MinotaurIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Minotaur;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MountainDwarfIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.MountainDwarf;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void OgreIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Ogre;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void OgreMageIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.OgreMage;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void OrcIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Orc;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void RockGnomeIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.RockGnome;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void SvirfneblinIsNotAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Svirfneblin;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void TallfellowHalflingIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.TallfellowHalfling;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void TieflingIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Tiefling;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void TroglodyteIsAllowed()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = RaceConstants.BaseRaces.Troglodyte;

            AssertRandomizerIsAllowed(randomizer);
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
        public void HalfCelestialIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.HalfCelestial;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void HalfDragonIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.HalfDragon;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HalfFiendIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.HalfFiend;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WerebearIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Werebear;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void WereboarIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Wereboar;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void WereratIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Wererat;

            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WeretigerIsNotAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Weretiger;

            AssertRandomizerIsNotAllowed(randomizer);
        }

        [Test]
        public void WerewolfIsAllowed()
        {
            var randomizer = new SetMetaraceRandomizer();
            randomizer.Metarace = RaceConstants.Metaraces.Werewolf;

            AssertRandomizerIsAllowed(randomizer);
        }
    }
}