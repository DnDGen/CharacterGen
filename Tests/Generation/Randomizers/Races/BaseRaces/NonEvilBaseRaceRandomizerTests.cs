using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonEvilBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonEvilBaseRaceRandomizer(mockPercentileResultProvider.Object);
            controlCase = RaceConstants.BaseRaces.Human;
        }

        [Test]
        public void AasimarIsAllowedOnlyIfAlignmentIsGood()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Aasimar, AlignmentConstants.Good);
        }

        [Test]
        public void BugbearIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Bugbear);
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
        public void DerroDwarfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.DerroDwarf);
        }

        [Test]
        public void DoppelgangerIsAllowedOnlyIfAlignmentIsNeutral()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Doppelganger, AlignmentConstants.Neutral);
        }

        [Test]
        public void DrowIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Drow);
        }

        [Test]
        public void DuergarIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Duergar);
        }

        [Test]
        public void ForestGnomeIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.ForestGnome);
        }

        [Test]
        public void GnollIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Gnoll);
        }

        [Test]
        public void GoblinIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Goblin);
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
        public void HobgoblinIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Hobgoblin);
        }

        [Test]
        public void HumanIsAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void KoboldIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Kobold);
        }

        [Test]
        public void LightfootHalflingIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.LightfootHalfling);
        }

        [Test]
        public void LizardfolkIsAllowedOnlyIfAlignmentIsNeutral()
        {
            AssertBaseRaceIsAllowedOnlyForAlignment(RaceConstants.BaseRaces.Lizardfolk, AlignmentConstants.Neutral);
        }

        [Test]
        public void MindFlayerIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.MindFlayer);
        }

        [Test]
        public void MinotaurIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Minotaur);
        }

        [Test]
        public void MountainDwarfIsAlwaysAllowed()
        {
            AssertBaseRaceIsAlwaysAllowed(RaceConstants.BaseRaces.MountainDwarf);
        }

        [Test]
        public void OgreIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Ogre);
        }

        [Test]
        public void OgreMageIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.OgreMage);
        }

        [Test]
        public void OrcIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Orc);
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
        public void TieflingIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Tiefling);
        }

        [Test]
        public void TroglodyteIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Troglodyte);
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