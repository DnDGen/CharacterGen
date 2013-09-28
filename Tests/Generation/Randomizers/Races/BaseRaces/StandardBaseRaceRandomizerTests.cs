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
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Aasimar);
        }

        [Test]
        public void BugbearIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Bugbear);
        }

        [Test]
        public void DeepDwarfIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.DeepDwarf);
        }

        [Test]
        public void DeepHalflingIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.DeepHalfling);
        }

        [Test]
        public void DerroDwarfIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.DerroDwarf);
        }

        [Test]
        public void DoppelgangerIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Doppelganger);
        }

        [Test]
        public void DrowIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Drow);
        }

        [Test]
        public void DuergarIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Duergar);
        }

        [Test]
        public void ForestGnomeIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.ForestGnome);
        }

        [Test]
        public void GnollIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Gnoll);
        }

        [Test]
        public void GoblinIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Goblin);
        }

        [Test]
        public void GrayElfIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.GrayElf);
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
        public void HobgoblinIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Hobgoblin);
        }

        [Test]
        public void HumanIsAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(RaceConstants.BaseRaces.HighElf);
        }

        [Test]
        public void KoboldIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Kobold);
        }

        [Test]
        public void LightfootHalflingIsAlwaysAllowed()
        {
            AssertRaceIsAlwaysAllowed(RaceConstants.BaseRaces.LightfootHalfling);
        }

        [Test]
        public void LizardfolkIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Lizardfolk);
        }

        [Test]
        public void MindFlayerIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.MindFlayer);
        }

        [Test]
        public void MinotaurIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Minotaur);
        }

        [Test]
        public void MountainDwarfIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.MountainDwarf);
        }

        [Test]
        public void OgreIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Ogre);
        }

        [Test]
        public void OgreMageIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.OgreMage);
        }

        [Test]
        public void OrcIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Orc);
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
        public void TallfellowHalflingIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.TallfellowHalfling);
        }

        [Test]
        public void TieflingIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Tiefling);
        }

        [Test]
        public void TroglodyteIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.Troglodyte);
        }

        [Test]
        public void WildElfIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.WildElf);
        }

        [Test]
        public void WoodElfIsNeverAllowed()
        {
            AssertRaceIsNeverAllowed(RaceConstants.BaseRaces.WoodElf);
        }
    }
}