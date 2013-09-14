using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRaceRandomizer : IBaseRaceRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(Alignment alignment, String className)
        {
            var filename = String.Format("{0}{1}BaseRaces", alignment.GetGoodnessString(), className);
            var baseRace = String.Empty;

            do baseRace = percentileResultProvider.GetPercentileResult(filename);
            while (!RaceIsAllowed(baseRace, alignment, className));

            return baseRace;
        }

        private String GoodBarbarianRace()
        {
            var roll = dice.Percentile();

            if (roll <= 2)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 32)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 34)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 35)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 36)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 61)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Human;

            return GoodBarbarianRace();
        }

        private String NeutralWizardRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 26)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 28)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 29)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 44)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 47)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 49)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 50)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Doppelganger;

            return NeutralWizardRace();
        }

        private String NeutralSorcererRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 2)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 12)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 15)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 16)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 31)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 41)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 42)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 43)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 48)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 95)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Lizardfolk;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Doppelganger;

            return NeutralSorcererRace();
        }

        private String NeutralRogueRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.DeepDwarf;
            else if (roll <= 4)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 8)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 9)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 10)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 25)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 53)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 58)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 63)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 73)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Doppelganger;

            return NeutralRogueRace();
        }

        private String NeutralRangerRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 6)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 7)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 36)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 37)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 38)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 55)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 56)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 57)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 67)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 96)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Lizardfolk;

            return NeutralRangerRace();
        }

        protected abstract Boolean RaceIsAllowed(String baseRace, Alignment alignment, String className);
    }
}