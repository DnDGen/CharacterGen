using System;
using NPCGen.Common.Races;
using NPCGen.Generators.Providers.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class NonGoodBaseRaceRandomizer : BaseBaseRace
    {
        public NonGoodBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentProvider)
            : base(percentileResultProvider, levelAdjustmentProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            switch (baseRace)
            {
                case RaceConstants.BaseRaces.Aasimar:
                case RaceConstants.BaseRaces.Svirfneblin: return false;
                case RaceConstants.BaseRaces.Bugbear:
                case RaceConstants.BaseRaces.Derro:
                case RaceConstants.BaseRaces.Drow:
                case RaceConstants.BaseRaces.DuergarDwarf:
                case RaceConstants.BaseRaces.Gnoll:
                case RaceConstants.BaseRaces.Goblin:
                case RaceConstants.BaseRaces.Hobgoblin:
                case RaceConstants.BaseRaces.Kobold:
                case RaceConstants.BaseRaces.Ogre:
                case RaceConstants.BaseRaces.OgreMage:
                case RaceConstants.BaseRaces.Orc:
                case RaceConstants.BaseRaces.Troglodyte:
                case RaceConstants.BaseRaces.MindFlayer:
                case RaceConstants.BaseRaces.Minotaur:
                case RaceConstants.BaseRaces.Tiefling: return true;
                case RaceConstants.BaseRaces.Doppelganger:
                case RaceConstants.BaseRaces.Lizardfolk: return true;
                case RaceConstants.BaseRaces.DeepDwarf:
                case RaceConstants.BaseRaces.DeepHalfling:
                case RaceConstants.BaseRaces.ForestGnome:
                case RaceConstants.BaseRaces.GrayElf:
                case RaceConstants.BaseRaces.HalfElf:
                case RaceConstants.BaseRaces.HalfOrc:
                case RaceConstants.BaseRaces.HighElf:
                case RaceConstants.BaseRaces.HillDwarf:
                case RaceConstants.BaseRaces.Human:
                case RaceConstants.BaseRaces.LightfootHalfling:
                case RaceConstants.BaseRaces.MountainDwarf:
                case RaceConstants.BaseRaces.RockGnome:
                case RaceConstants.BaseRaces.TallfellowHalfling:
                case RaceConstants.BaseRaces.WildElf:
                case RaceConstants.BaseRaces.WoodElf: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}