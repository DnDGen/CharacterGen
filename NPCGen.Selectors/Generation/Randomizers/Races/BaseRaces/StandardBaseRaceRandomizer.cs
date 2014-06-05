using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class StandardBaseRaceRandomizer : BaseBaseRace
    {
        public StandardBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentProvider)
            : base(percentileResultProvider, levelAdjustmentProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            switch (baseRace)
            {
                case RaceConstants.BaseRaces.RockGnome:
                case RaceConstants.BaseRaces.HalfElf:
                case RaceConstants.BaseRaces.HalfOrc:
                case RaceConstants.BaseRaces.HighElf:
                case RaceConstants.BaseRaces.HillDwarf:
                case RaceConstants.BaseRaces.Human:
                case RaceConstants.BaseRaces.LightfootHalfling: return true;
                case RaceConstants.BaseRaces.Aasimar:
                case RaceConstants.BaseRaces.Bugbear:
                case RaceConstants.BaseRaces.DeepDwarf:
                case RaceConstants.BaseRaces.DeepHalfling:
                case RaceConstants.BaseRaces.Derro:
                case RaceConstants.BaseRaces.Doppelganger:
                case RaceConstants.BaseRaces.Drow:
                case RaceConstants.BaseRaces.DuergarDwarf:
                case RaceConstants.BaseRaces.Gnoll:
                case RaceConstants.BaseRaces.Goblin:
                case RaceConstants.BaseRaces.GrayElf:
                case RaceConstants.BaseRaces.Hobgoblin:
                case RaceConstants.BaseRaces.Kobold:
                case RaceConstants.BaseRaces.Lizardfolk:
                case RaceConstants.BaseRaces.MindFlayer:
                case RaceConstants.BaseRaces.Minotaur:
                case RaceConstants.BaseRaces.MountainDwarf:
                case RaceConstants.BaseRaces.Ogre:
                case RaceConstants.BaseRaces.OgreMage:
                case RaceConstants.BaseRaces.Orc:
                case RaceConstants.BaseRaces.ForestGnome:
                case RaceConstants.BaseRaces.Svirfneblin:
                case RaceConstants.BaseRaces.TallfellowHalfling:
                case RaceConstants.BaseRaces.Tiefling:
                case RaceConstants.BaseRaces.Troglodyte:
                case RaceConstants.BaseRaces.WildElf:
                case RaceConstants.BaseRaces.WoodElf: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}