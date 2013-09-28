using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class NonStandardBaseRaceRandomizer : BaseBaseRaceRandomizer
    {
        public NonStandardBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace, Alignment alignment)
        {
            switch (baseRace)
            {
                case RaceConstants.BaseRaces.LightfootHalfling:
                case RaceConstants.BaseRaces.Human:
                case RaceConstants.BaseRaces.HalfOrc:
                case RaceConstants.BaseRaces.HighElf:
                case RaceConstants.BaseRaces.HillDwarf:
                case RaceConstants.BaseRaces.HalfElf:
                case RaceConstants.BaseRaces.RockGnome: return false;
                case RaceConstants.BaseRaces.Svirfneblin:
                case RaceConstants.BaseRaces.Aasimar: return alignment.IsGood();
                case RaceConstants.BaseRaces.DerroDwarf:
                case RaceConstants.BaseRaces.Drow:
                case RaceConstants.BaseRaces.Duergar:
                case RaceConstants.BaseRaces.Goblin:
                case RaceConstants.BaseRaces.Hobgoblin:
                case RaceConstants.BaseRaces.Ogre:
                case RaceConstants.BaseRaces.OgreMage:
                case RaceConstants.BaseRaces.Orc:
                case RaceConstants.BaseRaces.Troglodyte:
                case RaceConstants.BaseRaces.MindFlayer:
                case RaceConstants.BaseRaces.Minotaur:
                case RaceConstants.BaseRaces.Gnoll:
                case RaceConstants.BaseRaces.Bugbear:
                case RaceConstants.BaseRaces.Tiefling:
                case RaceConstants.BaseRaces.Kobold: return alignment.IsEvil();
                case RaceConstants.BaseRaces.Doppelganger:
                case RaceConstants.BaseRaces.Lizardfolk: return alignment.Goodness == AlignmentConstants.Neutral;
                case RaceConstants.BaseRaces.MountainDwarf:
                case RaceConstants.BaseRaces.ForestGnome:
                case RaceConstants.BaseRaces.TallfellowHalfling:
                case RaceConstants.BaseRaces.WildElf:
                case RaceConstants.BaseRaces.DeepDwarf:
                case RaceConstants.BaseRaces.DeepHalfling:
                case RaceConstants.BaseRaces.GrayElf:
                case RaceConstants.BaseRaces.WoodElf: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}