using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class EvilAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatibility(IClassNameRandomizer classNameRandomizer)
        {
            if (classNameRandomizer is SetClassNameRandomizer)
            {
                var setClassNameRandomizer = classNameRandomizer as SetClassNameRandomizer;
                return setClassNameRandomizer.ClassName != CharacterClassConstants.Paladin;
            }

            return true;
        }

        public Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer)
        {
            if (baseRaceRandomizer is SetBaseRaceRandomizer)
            {
                var setBaseRaceRandomizer = baseRaceRandomizer as SetBaseRaceRandomizer;
                return setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Aasimar
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Doppelganger
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.ForestGnome
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.GrayElf
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.MountainDwarf
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.RockGnome
                    && setBaseRaceRandomizer.BaseRace != RaceConstants.BaseRaces.Svirfneblin;
            }

            return true;
        }

        public Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer)
        {
            if (metaraceRandomizer is SetMetaraceRandomizer)
            {
                var setMetaraceRandomizer = metaraceRandomizer as SetMetaraceRandomizer;
                return setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.HalfCelestial
                    && setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.Werebear
                    && setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.Wereboar
                    && setMetaraceRandomizer.Metarace != RaceConstants.Metaraces.Weretiger;
            }
            else if (metaraceRandomizer is NeutralMetaraceRandomizer)
            {
                var neutralMetaraceRandomizer = metaraceRandomizer as NeutralMetaraceRandomizer;
                return neutralMetaraceRandomizer.AllowNoMetarace;
            }

            return true;
        }
    }
}