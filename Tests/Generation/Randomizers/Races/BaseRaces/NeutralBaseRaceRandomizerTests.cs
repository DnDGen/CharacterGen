using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NeutralBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NeutralBaseRaceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void AasimarIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Aasimar);
        }

        [Test]
        public void BugbearIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Bugbear);
        }

        [Test]
        public void DeepDwarfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.DeepDwarf);
        }

        [Test]
        public void DeepHalflingIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.DeepHalfling);
        }

        [Test]
        public void DerroDwarfIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.DerroDwarf);
        }

        [Test]
        public void DoppelgangerIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.Doppelganger);
        }

        [Test]
        public void DrowIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Drow);
        }

        [Test]
        public void DuergarIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Duergar);
        }

        [Test]
        public void ForestGnomeIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.ForestGnome);
        }

        [Test]
        public void GnollIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Gnoll);
        }

        [Test]
        public void GoblinIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Goblin);
        }

        [Test]
        public void GrayElfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.GrayElf);
        }

        [Test]
        public void HalfElfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.HalfElf);
        }

        [Test]
        public void HalfOrcIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.HalfOrc);
        }

        [Test]
        public void HighElfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void HillDwarfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.HillDwarf);
        }

        [Test]
        public void HobgoblinIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Hobgoblin);
        }

        [Test]
        public void HumanIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.Human);
        }

        [Test]
        public void KoboldIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Kobold);
        }

        [Test]
        public void LightfootHalflingIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.LightfootHalfling);
        }

        [Test]
        public void LizardfolkIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.Lizardfolk);
        }

        [Test]
        public void MindFlayerIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.MindFlayer);
        }

        [Test]
        public void MinotaurIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Minotaur);
        }

        [Test]
        public void MountainDwarfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.MountainDwarf);
        }

        [Test]
        public void OgreIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Ogre);
        }

        [Test]
        public void OgreMageIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.OgreMage);
        }

        [Test]
        public void OrcIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Orc);
        }

        [Test]
        public void RockGnomeIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.RockGnome);
        }

        [Test]
        public void SvirfneblinIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Svirfneblin);
        }

        [Test]
        public void TallfellowHalflingIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.TallfellowHalfling);
        }

        [Test]
        public void TieflingIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Tiefling);
        }

        [Test]
        public void TroglodyteIsNotAllowed()
        {
            AssertRaceIsNotAllowed(RaceConstants.BaseRaces.Troglodyte);
        }

        [Test]
        public void WildElfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.WildElf);
        }

        [Test]
        public void WoodElfIsAllowed()
        {
            AssertRaceIsAllowed(RaceConstants.BaseRaces.WoodElf);
        }
    }
}