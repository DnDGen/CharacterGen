using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonNeutralBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonNeutralBaseRaceRandomizer(mockPercentileResultProvider.Object);
            controlCase = RaceConstants.BaseRaces.Human;
        }

        [Test]
        public void AasimarIsAllowedOnlyIfAlignmentIsGood()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Aasimar, AlignmentConstants.Good);
        }

        [Test]
        public void BugbearIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Bugbear, AlignmentConstants.Evil);
        }

        [Test]
        public void DeepDwarfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.DeepDwarf);
        }

        [Test]
        public void DeepHalflingIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.DeepHalfling);
        }

        [Test]
        public void DerroDwarfIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.DerroDwarf, AlignmentConstants.Evil);
        }

        [Test]
        public void DoppelgangerIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Doppelganger);
        }

        [Test]
        public void DrowIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Drow, AlignmentConstants.Evil);
        }

        [Test]
        public void DuergarIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Duergar, AlignmentConstants.Evil);
        }

        [Test]
        public void ForestGnomeIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.ForestGnome);
        }

        [Test]
        public void GnollIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Gnoll, AlignmentConstants.Evil);
        }

        [Test]
        public void GoblinIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Goblin, AlignmentConstants.Evil);
        }

        [Test]
        public void GrayElfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.GrayElf);
        }

        [Test]
        public void HalfElfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HalfElf);
        }

        [Test]
        public void HalfOrcIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HalfOrc);
        }

        [Test]
        public void HighElfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void HillDwarfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HillDwarf);
        }

        [Test]
        public void HobgoblinIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Hobgoblin, AlignmentConstants.Evil);
        }

        [Test]
        public void HumanIsAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void KoboldIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Kobold, AlignmentConstants.Evil);
        }

        [Test]
        public void LightfootHalflingIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.LightfootHalfling);
        }

        [Test]
        public void LizardfolkIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Lizardfolk);
        }

        [Test]
        public void MindFlayerIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.MindFlayer, AlignmentConstants.Evil);
        }

        [Test]
        public void MinotaurIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Minotaur, AlignmentConstants.Evil);
        }

        [Test]
        public void MountainDwarfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.MountainDwarf);
        }

        [Test]
        public void OgreIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Ogre, AlignmentConstants.Evil);
        }

        [Test]
        public void OgreMageIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.OgreMage, AlignmentConstants.Evil);
        }

        [Test]
        public void OrcIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Orc, AlignmentConstants.Evil);
        }

        [Test]
        public void RockGnomeIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.RockGnome);
        }

        [Test]
        public void SvirfneblinIsAllowedOnlyIfAlignmentIsGood()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Svirfneblin, AlignmentConstants.Good);
        }

        [Test]
        public void TallfellowHalflingIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.TallfellowHalfling);
        }

        [Test]
        public void TieflingIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Tiefling, AlignmentConstants.Evil);
        }

        [Test]
        public void TroglodyteIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Troglodyte, AlignmentConstants.Evil);
        }

        [Test]
        public void WildElfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.WildElf);
        }

        [Test]
        public void WoodElfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.WoodElf);
        }
    }
}