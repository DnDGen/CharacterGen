using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class StandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new StandardBaseRaceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void AasimarIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Aasimar);
        }

        [Test]
        public void BugbearIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Bugbear);
        }

        [Test]
        public void DeepDwarfIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.DeepDwarf);
        }

        [Test]
        public void DeepHalflingIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.DeepHalfling);
        }

        [Test]
        public void DerroDwarfIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.DerroDwarf);
        }

        [Test]
        public void DoppelgangerIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Doppelganger);
        }

        [Test]
        public void DrowIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Drow);
        }

        [Test]
        public void DuergarIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Duergar);
        }

        [Test]
        public void ForestGnomeIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.ForestGnome);
        }

        [Test]
        public void GnollIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Gnoll);
        }

        [Test]
        public void GoblinIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Goblin);
        }

        [Test]
        public void GrayElfIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.GrayElf);
        }

        [Test]
        public void HalfElfIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.HalfElf);
        }

        [Test]
        public void HalfOrcIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.HalfOrc);
        }

        [Test]
        public void HighElfIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void HillDwarfIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.HillDwarf);
        }

        [Test]
        public void HobgoblinIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Hobgoblin);
        }

        [Test]
        public void HumanIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.Human);
        }

        [Test]
        public void KoboldIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Kobold);
        }

        [Test]
        public void LightfootHalflingIsAllowed()
        {
            AssertBaseRaceIsAllowed(RaceConstants.BaseRaces.LightfootHalfling);
        }

        [Test]
        public void LizardfolkIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Lizardfolk);
        }

        [Test]
        public void MindFlayerIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.MindFlayer);
        }

        [Test]
        public void MinotaurIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Minotaur);
        }

        [Test]
        public void MountainDwarfIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.MountainDwarf);
        }

        [Test]
        public void OgreIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Ogre);
        }

        [Test]
        public void OgreMageIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.OgreMage);
        }

        [Test]
        public void OrcIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Orc);
        }

        [Test]
        public void RockGnomeIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.RockGnome);
        }

        [Test]
        public void SvirfneblinIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Svirfneblin);
        }

        [Test]
        public void TallfellowHalflingIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.TallfellowHalfling);
        }

        [Test]
        public void TieflingIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Tiefling);
        }

        [Test]
        public void TroglodyteIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.Troglodyte);
        }

        [Test]
        public void WildElfIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.WildElf);
        }

        [Test]
        public void WoodElfIsNotAllowed()
        {
            AssertBaseRaceIsNotAllowed(RaceConstants.BaseRaces.WoodElf);
        }
    }
}