using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonGoodBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonGoodBaseRaceRandomizer(mockPercentileResultProvider.Object);
            controlCase = RaceConstants.BaseRaces.Human;
        }

        [Test]
        public void AasimarIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Aasimar);
        }

        [Test]
        public void BugbearIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Bugbear, AlignmentConstants.Evil);
        }

        [Test]
        public void DeepDwarfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.DeepDwarf);
        }

        [Test]
        public void DeepHalflingIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.DeepHalfling);
        }

        [Test]
        public void DerroDwarfIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.DerroDwarf, AlignmentConstants.Evil);
        }

        [Test]
        public void DoppelgangerIsAllowedOnlyIfAlignmentIsNeutral()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Doppelganger, AlignmentConstants.Neutral);
        }

        [Test]
        public void DrowIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Drow, AlignmentConstants.Evil);
        }

        [Test]
        public void DuergarIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Duergar, AlignmentConstants.Evil);
        }

        [Test]
        public void ForestGnomeIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.ForestGnome);
        }

        [Test]
        public void GnollIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Gnoll, AlignmentConstants.Evil);
        }

        [Test]
        public void GoblinIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Goblin, AlignmentConstants.Evil);
        }

        [Test]
        public void GrayElfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.GrayElf);
        }

        [Test]
        public void HalfElfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HalfElf);
        }

        [Test]
        public void HalfOrcIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HalfOrc);
        }

        [Test]
        public void HighElfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void HillDwarfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.HillDwarf);
        }

        [Test]
        public void HobgoblinIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Hobgoblin, AlignmentConstants.Evil);
        }

        [Test]
        public void HumanIsAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void KoboldIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Kobold, AlignmentConstants.Evil);
        }

        [Test]
        public void LightfootHalflingIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.LightfootHalfling);
        }

        [Test]
        public void LizardfolkIsAllowedOnlyIfAlignmentIsNeutral()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Lizardfolk, AlignmentConstants.Neutral);
        }

        [Test]
        public void MindFlayerIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.MindFlayer, AlignmentConstants.Evil);
        }

        [Test]
        public void MinotaurIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Minotaur, AlignmentConstants.Evil);
        }

        [Test]
        public void MountainDwarfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.MountainDwarf);
        }

        [Test]
        public void OgreIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Ogre, AlignmentConstants.Evil);
        }

        [Test]
        public void OgreMageIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.OgreMage, AlignmentConstants.Evil);
        }

        [Test]
        public void OrcIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Orc, AlignmentConstants.Evil);
        }

        [Test]
        public void RockGnomeIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.RockGnome);
        }

        [Test]
        public void SvirfneblinIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Svirfneblin);
        }

        [Test]
        public void TallfellowHalflingIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.TallfellowHalfling);
        }

        [Test]
        public void TieflingIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Tiefling, AlignmentConstants.Evil);
        }

        [Test]
        public void TroglodyteIsAllowedOnlyIfAlignmentIsEvil()
        {
            AssertRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Troglodyte, AlignmentConstants.Evil);
        }

        [Test]
        public void WildElfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.WildElf);
        }

        [Test]
        public void WoodElfIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.WoodElf);
        }
    }
}