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
            controlCase = RaceConstants.BaseRaces.Human;
        }

        [Test]
        public void AasimarIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Aasimar);
        }

        [Test]
        public void BugbearIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Bugbear);
        }

        [Test]
        public void DeepDwarfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.DeepDwarf);
        }

        [Test]
        public void DeepHalflingIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.DeepHalfling);
        }

        [Test]
        public void DerroDwarfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.DerroDwarf);
        }

        [Test]
        public void DoppelgangerIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Doppelganger);
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
        public void GrayElfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.GrayElf);
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
        public void LizardfolkIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Lizardfolk);
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
        public void MountainDwarfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.MountainDwarf);
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
        public void RockGnomeIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.RockGnome);
        }

        [Test]
        public void SvirfneblinIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.Svirfneblin);
        }

        [Test]
        public void TallfellowHalflingIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.TallfellowHalfling);
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
        public void WildElfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.WildElf);
        }

        [Test]
        public void WoodElfIsNeverAllowed()
        {
            AssertBaseRaceIsNeverAllowed(RaceConstants.BaseRaces.WoodElf);
        }
    }
}