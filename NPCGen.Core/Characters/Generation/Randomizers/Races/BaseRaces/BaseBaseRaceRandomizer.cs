using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Data.Races;
using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRaceRandomizer : IBaseRaceRandomizer
    {
        private IDice dice;

        public BaseBaseRaceRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        public String Randomize(Alignment alignment, String className)
        {
            var baseRace = String.Empty;

            do
            {
                if (alignment.Goodness == 1)
                    baseRace = GoodRace(className);
                else if (alignment.Goodness == 0)
                    baseRace = NeutralRace(className);
                else if (alignment.Goodness == -1)
                    baseRace = EvilRace(className);
            } while (!RaceIsAllowed(baseRace, alignment, className));

            return baseRace;
        }

        private String GoodRace(String className)
        {
            switch (className)
            {
                case ClassConstants.BARBARIAN: return GoodBarbarianRace();
                case ClassConstants.BARD: return GoodBardRace();
                case ClassConstants.CLERIC: return GoodClericRace();
                case ClassConstants.DRUID: return GoodDruidRace();
                case ClassConstants.FIGHTER: return GoodFighterRace();
                case ClassConstants.MONK: return GoodMonkRace();
                case ClassConstants.PALADIN: return PaladinRace();
                case ClassConstants.RANGER: return GoodRangerRace();
                case ClassConstants.ROGUE: return GoodRogueRace();
                case ClassConstants.SORCERER: return GoodSorcererRace();
                case ClassConstants.WIZARD: return GoodWizardRace();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private String GoodWizardRace()
        {
            var roll = dice.Percentile();

            if (roll <= 1)
                return RaceConstants.BaseRaces.Aasimar;
            else if (roll <= 2)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 7)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 41)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 42)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 43)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 48)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 58)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 63)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 64)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 67)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 68)
                return RaceConstants.BaseRaces.Halforc;
            else if (roll <= 96)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Svirfneblin;

            return GoodWizardRace();
        }

        private String GoodSorcererRace()
        {
            var roll = dice.Percentile();

            if (roll <= 2)
                return RaceConstants.BaseRaces.Aasimar;
            else if (roll <= 3)
                return RaceConstants.BaseRaces.DeepDwarf;
            else if (roll <= 5)
                return RaceConstants.BaseRaces.HillDwarf;
            else if (roll <= 6)
                return RaceConstants.BaseRaces.MountainDwarf;
            else if (roll <= 8)
                return RaceConstants.BaseRaces.GrayElf;
            else if (roll <= 11)
                return RaceConstants.BaseRaces.HighElf;
            else if (roll <= 36)
                return RaceConstants.BaseRaces.WildElf;
            else if (roll <= 37)
                return RaceConstants.BaseRaces.WoodElf;
            else if (roll <= 38)
                return RaceConstants.BaseRaces.ForestGnome;
            else if (roll <= 40)
                return RaceConstants.BaseRaces.RockGnome;
            else if (roll <= 45)
                return RaceConstants.BaseRaces.HalfElf;
            else if (roll <= 54)
                return RaceConstants.BaseRaces.LightfootHalfling;
            else if (roll <= 55)
                return RaceConstants.BaseRaces.DeepHalfling;
            else if (roll <= 56)
                return RaceConstants.BaseRaces.TallfellowHalfling;
            else if (roll <= 58)
                return RaceConstants.BaseRaces.Halforc;
            else if (roll <= 95)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 96)
                return RaceConstants.BaseRaces.Svirfneblin;

            return GoodSorcererRace();
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
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
                return RaceConstants.BaseRaces.Halforc;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Human;

            return GoodBarbarianRace();
        }

        private String NeutralRace(String className)
        {
            switch (className)
            {
                case ClassConstants.BARBARIAN: return NeutralBarbarianRace();
                case ClassConstants.BARD: return NeutralBardRace();
                case ClassConstants.CLERIC: return NeutralClericRace();
                case ClassConstants.DRUID: return NeutralDruidRace();
                case ClassConstants.FIGHTER: return NeutralFighterRace();
                case ClassConstants.MONK: return NeutralMonkRace();
                case ClassConstants.RANGER: return NeutralRangerRace();
                case ClassConstants.ROGUE: return NeutralRogueRace();
                case ClassConstants.SORCERER: return NeutralSorcererRace();
                case ClassConstants.WIZARD: return NeutralWizardRace();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private string NeutralWizardRace()
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
                return RaceConstants.BaseRaces.Halforc;
            else if (roll <= 97)
                return RaceConstants.BaseRaces.Human;
            else if (roll <= 98)
                return RaceConstants.BaseRaces.Doppelganger;

            return NeutralWizardRace();
        }

        private string NeutralSorcererRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralRogueRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralRangerRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralMonkRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralFighterRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralDruidRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralClericRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralBardRace()
        {
            throw new NotImplementedException();
        }

        private string NeutralBarbarianRace()
        {
            throw new NotImplementedException();
        }

        private String EvilRace(String className)
        {
            switch (className)
            {
                case ClassConstants.BARBARIAN: return EvilBarbarianRace();
                case ClassConstants.BARD: return EvilBardRace();
                case ClassConstants.CLERIC: return EvilClericRace();
                case ClassConstants.DRUID: return EvilDruidRace();
                case ClassConstants.FIGHTER: return EvilFighterRace();
                case ClassConstants.MONK: return EvilMonkRace();
                case ClassConstants.RANGER: return EvilRangerRace();
                case ClassConstants.ROGUE: return EvilRogueRace();
                case ClassConstants.SORCERER: return EvilSorcererRace();
                case ClassConstants.WIZARD: return EvilWizardRace();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private string EvilWizardRace()
        {
            throw new NotImplementedException();
        }

        private string EvilSorcererRace()
        {
            throw new NotImplementedException();
        }

        private string EvilRogueRace()
        {
            throw new NotImplementedException();
        }

        private string EvilRangerRace()
        {
            throw new NotImplementedException();
        }

        private string EvilMonkRace()
        {
            throw new NotImplementedException();
        }

        private string EvilFighterRace()
        {
            throw new NotImplementedException();
        }

        private string EvilDruidRace()
        {
            throw new NotImplementedException();
        }

        private string EvilClericRace()
        {
            throw new NotImplementedException();
        }

        private string EvilBardRace()
        {
            throw new NotImplementedException();
        }

        private string EvilBarbarianRace()
        {
            throw new NotImplementedException();
        }

        protected abstract Boolean RaceIsAllowed(String baseRace, Alignment alignment, String className);
    }
}