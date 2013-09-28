﻿using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class StandardBaseRaceRandomizer : BaseBaseRaceRandomizer
    {
        public StandardBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace, Alignment alignment)
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
                case RaceConstants.BaseRaces.DerroDwarf:
                case RaceConstants.BaseRaces.Doppelganger:
                case RaceConstants.BaseRaces.Drow:
                case RaceConstants.BaseRaces.Duergar:
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