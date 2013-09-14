using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;
using NPCGen.Core.Characters.Data.Races;
using NPCGen.Core.Characters.Generation.Xml;
using System;
using System.Linq;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRaceRandomizer : IBaseRaceRandomizer
    {
        private IDice dice;
        private IPercentileXmlParser percentileXmlParser;

        public BaseBaseRaceRandomizer(IDice dice, IPercentileXmlParser percentileXmlParser)
        {
            this.dice = dice;
            this.percentileXmlParser = percentileXmlParser;
        }

        public String Randomize(Alignment alignment, String className)
        {
            var filename = String.Format("{0}{1}BaseRaces", alignment.GetGoodnessString(), className);
            var table = percentileXmlParser.Parse(filename);
            var baseRace = String.Empty;

            do
            {
                var roll = dice.Percentile();
                var result = table.FirstOrDefault(o => RollIsInRange(roll, o));

                if (result != null)
                    baseRace = result.Content;
            } while (!RaceIsAllowed(baseRace, alignment, className));

            return baseRace;
        }

        private Boolean RollIsInRange(Int32 roll, PercentileObject percentileObject)
        {
            return percentileObject.LowerLimit <= roll && percentileObject.UpperLimit >= roll;
        }

        private String GoodRogueRace()
        {
            var roll = dice.Percentile();

            if (roll <= 5)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 6)
                return RaceConstants.BaseRaces.MountainDwarf;
            else if (roll <= 19)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 20)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 25)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 35)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 60)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 66)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 72)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 77)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 96)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Svirfneblin;

            return GoodRogueRace();
        }

        private String GoodRangerRace()
        {
            var roll = dice.Percentile();

            if (roll <= 5)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 20)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 21)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 36)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 41)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 42)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 57)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 58)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 59)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 64)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;

            return GoodRangerRace();
        }

        private String PaladinRace()
        {
            var roll = dice.Percentile();

            if (roll <= 10)
                return RaceConstants.BaseRaces.Aasimar;
            else if (roll <= 20)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 21)
                return RaceConstants.BaseRaces.MountainDwarf;
            else if (roll <= 22)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 27)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 28)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 29)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 30)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;

            return PaladinRace();
        }

        private String GoodMonkRace()
        {
            var roll = dice.Percentile();

            if (roll <= 2)
                return RaceConstants.BaseRaces.Aasimar;
            else if (roll <= 3)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 13)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 18)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 19)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 20)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 25)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;

            return GoodMonkRace();
        }

        private String GoodFighterRace()
        {
            var roll = dice.Percentile();

            if (roll <= 3)
                return RaceConstants.BaseRaces.DeepDwarf;
            else if (roll <= 33)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 41)
                return RaceConstants.BaseRaces.MountainDwarf;
            else if (roll <= 42)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 47)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 48)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 50)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 51)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 52)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 57)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;

            return GoodFighterRace();
        }

        private String GoodDruidRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 11)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 21)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 31)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 36)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 37)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 46)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 47)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 48)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 49)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 99)
                return RaceConstants.BaseRaces.Human;

            return GoodDruidRace();
        }

        private String GoodClericRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.Aasimar;
            else if (roll <= 2)
                return RaceConstants.BaseRaces.DeepDwarf;
            else if (roll <= 22)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 24)
                return RaceConstants.BaseRaces.MountainDwarf;
            else if (roll <= 25)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 35)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 40)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 41)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 42)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 51)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 56)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 66)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 67)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 69)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 70)
                return RaceConstants.BaseRaces.HalfOrc;
            else if (roll <= 95)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 96)
                return RaceConstants.BaseRaces.Svirfneblin;

            return GoodClericRace();
        }

        private String GoodBardRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.Aasimar;
            else if (roll <= 6)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 11)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 36)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 37)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 38)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 39)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 44)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 53)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 54)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 55)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 57)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Svirfneblin;

            return GoodBardRace();
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