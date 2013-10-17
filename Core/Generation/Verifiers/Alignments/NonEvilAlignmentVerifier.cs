using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class NonEvilAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatibility(IClassNameRandomizer classRandomizer)
        {
            return true;
        }

        public Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer)
        {
            if (baseRaceRandomizer is SetBaseRaceRandomizer)
            {
                var setBaseRaceRandomizer = baseRaceRandomizer as SetBaseRaceRandomizer;
                return setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Bugbear
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.DerroDwarf
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Drow
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Duergar
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Gnoll
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Goblin
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Hobgoblin
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Kobold
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.MindFlayer
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Minotaur
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Ogre
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.OgreMage
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Orc
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Tiefling
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Troglodyte;
            }

            return true;
        }

        public Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer)
        {
            if (metaraceRandomizer is NeutralMetaraceRandomizer)
            {
                var neutralMetaraceRandomizer = metaraceRandomizer as NeutralMetaraceRandomizer;
                return neutralMetaraceRandomizer.AllowNoMetarace;
            }
            else if (metaraceRandomizer is SetMetaraceRandomizer)
            {
                var setMetaraceRandomizer = metaraceRandomizer as SetMetaraceRandomizer;
                return setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.HalfFiend
                    && setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.Wererat
                    && setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.Werewolf;
            }
            
            return true;
        }
    }
}