using System;

namespace CharacterGen.Common.Races
{
    public class RaceConstants
    {
        public static class Ages
        {
            public const String Adulthood = "Adulthood";
            public const String MiddleAge = "Middle Age";
            public const String Old = "Old";
            public const String Venerable = "Venerable";
        }

        public static class Sizes
        {
            public const String Large = "Large";
            public const String Medium = "Medium";
            public const String Small = "Small";
        }

        public static class BaseRaces
        {
            public const String Aasimar = "Aasimar";
            public const String Bugbear = "Bugbear";
            public const String DeepDwarf = "Deep Dwarf";
            public const String DeepHalfling = "Deep Halfling";
            public const String Derro = "Derro";
            public const String Doppelganger = "Doppelganger";
            public const String Drow = "Drow";
            public const String DuergarDwarf = "Duergar Dwarf";
            public const String ForestGnome = "Forest Gnome";
            public const String Goblin = "Goblin";
            public const String Gnoll = "Gnoll";
            public const String GrayElf = "Gray Elf";
            public const String HalfElf = "Half-Elf";
            public const String HalfOrc = "Half-Orc";
            public const String HighElf = "High Elf";
            public const String HillDwarf = "Hill Dwarf";
            public const String Hobgoblin = "Hobgoblin";
            public const String Human = "Human";
            public const String Kobold = "Kobold";
            public const String LightfootHalfling = "Lightfoot Halfling";
            public const String Lizardfolk = "Lizardfolk";
            public const String MindFlayer = "Mind Flayer";
            public const String Minotaur = "Minotaur";
            public const String MountainDwarf = "Mountain Dwarf";
            public const String Ogre = "Ogre";
            public const String OgreMage = "Ogre Mage";
            public const String Orc = "Orc";
            public const String RockGnome = "Rock Gnome";
            public const String Svirfneblin = "Svirfneblin";
            public const String TallfellowHalfling = "Tallfellow Halfling";
            public const String Tiefling = "Tiefling";
            public const String Troglodyte = "Troglodyte";
            public const String WildElf = "Wild Elf";
            public const String WoodElf = "Wood Elf";

            public static class Animals
            {
                public const String Badger = "Badger";
                public const String Camel = "Camel";
                public const String DireRat = "Dire Rat";
                public const String Dog = "Dog";
                public const String RidingDog = "Riding Dog";
                public const String Eagle = "Eagle";
                public const String Hawk = "Hawk";
                public const String LightHorse = "Light Horse";
                public const String HeavyHorse = "Heavy Horse";
                public const String Owl = "Owl";
                public const String Pony = "Pony";
                public const String SmallViperSnake = "Small Viper Snake";
                public const String MediumViperSnake = "Medium Viper Snake";
                public const String Wolf = "Wolf";
                public const String Ape = "Ape";
                public const String BlackBear = "Black Bear";
                public const String Bison = "Bison";
                public const String Boar = "Boar";
                public const String Cheetah = "Cheetah";
                public const String DireBadger = "Dire Badger";
                public const String DireBat = "Dire Bat";
                public const String DireWeasel = "Dire Weasel";
                public const String Leopard = "Leopard";
                public const String MonitorLizard = "Monitor Lizard";
                public const String ConstrictorSnake = "Constrictor Snake";
                public const String LargeViperSnake = "Large Viper Snake";
                public const String Wolverine = "Wolverine";
                public const String BrownBear = "Brown Bear";
                public const String DireWolverine = "Dire Wolverine";
                public const String Deinonychus = "Deinonychus";
                public const String DireApe = "Dire Ape";
                public const String DireBoar = "Dire Boar";
                public const String DireWolf = "Dire Wolf";
                public const String Lion = "Lion";
                public const String Rhinoceras = "Rhinoceras";
                public const String HugeViperSnake = "Huge Viper Snake";
                public const String Tiger = "Tiger";
                public const String PolarBear = "Polar Bear";
                public const String DireLion = "Dire Lion";
                public const String Megaraptor = "Megaraptor";
                public const String GiantConstrictorSnake = "Giant Constrictor Snake";
                public const String DireBear = "Dire Bear";
                public const String Elephant = "Elephant";
                public const String DireTiger = "Dire Tiger";
                public const String Triceratops = "Triceratops";
                public const String Tyrannosaurus = "Tyrannosaurus";
                public const String HeavyWarhorse = "Heavy Warhorse";
                public const String Warpony = "Warpony";
                public const String Bat = "Bat";
                public const String Cat = "Cat";
                public const String Lizard = "Lizard";
                public const String Rat = "Rat";
                public const String Raven = "Raven";
                public const String TinyViperSnake = "Tiny Viper Snake";
                public const String Toad = "Toad";
                public const String Weasel = "Weasel";
                public const String ShockerLizard = "Shocker Lizard";
                public const String Stirge = "Stirge";
                public const String FormianWorker = "Formian Worker";
                public const String Imp = "Imp";
                public const String Pseudodragon = "Pseudodragon";
                public const String Quasit = "Quasit";
                public const String CelestialBat = "Celestial Bat";
                public const String CelestialCat = "Celestial Cat";
                public const String CelestialHawk = "Celestial Hawk";
                public const String CelestialLizard = "Celestial Lizard";
                public const String CelestialOwl = "Celestial Owl";
                public const String CelestialRat = "Celestial Rat";
                public const String CelestialRaven = "Celestial Raven";
                public const String CelestialTinyViperSnake = "Celestial Tiny Viper Snake";
                public const String CelestialToad = "Celestial Toad";
                public const String CelestialWeasel = "Celestial Weasel";
                public const String FiendishBat = "Fiendish Bat";
                public const String FiendishCat = "Fiendish Cat";
                public const String FiendishHawk = "Fiendish Hawk";
                public const String FiendishLizard = "Fiendish Lizard";
                public const String FiendishOwl = "Fiendish Owl";
                public const String FiendishRat = "Fiendish Rat";
                public const String FiendishRaven = "Fiendish Raven";
                public const String FiendishTinyViperSnake = "Fiendish Tiny Viper Snake";
                public const String FiendishToad = "Fiendish Toad";
                public const String FiendishWeasel = "Fiendish Weasel";
                public const String SmallAirElemental = "Small Air Elemental";
                public const String SmallEarthElemental = "Small Earth Elemental";
                public const String SmallFireElemental = "Small Fire Elemental";
                public const String SmallWaterElemental = "Small Water Elemental";
                public const String Homonculus = "Homonculus";
                public const String AirMephit = "Air Mephit";
                public const String DustMephit = "Dust Mephit";
                public const String EarthMephit = "Earth Mephit";
                public const String FireMephit = "Fire Mephit";
                public const String IceMephit = "Ice Mephit";
                public const String MagmaMephit = "Magma Mephit";
                public const String OozeMephit = "Ooze Mephit";
                public const String SaltMephit = "Salt Mephit";
                public const String SteamMephit = "Steam Mephit";
                public const String WaterMephit = "Water Mephit";
            }
        }

        public static class Metaraces
        {
            public const String HalfCelestial = "Half-Celestial";
            public const String HalfDragon = "Half-Dragon";
            public const String HalfFiend = "Half-Fiend";
            public const String Werebear = "Werebear";
            public const String Wereboar = "Wereboar";
            public const String Weretiger = "Weretiger";
            public const String Wererat = "Wererat";
            public const String Werewolf = "Werewolf";
            public const String Vampire = "Vampire";
            public const String Ghost = "Ghost";
            public const String Lich = "Lich";
            public const String None = "";

            public static class Species
            {
                public const String Black = "Black";
                public const String Blue = "Blue";
                public const String Brass = "Brass";
                public const String Bronze = "Bronze";
                public const String Copper = "Copper";
                public const String Green = "Green";
                public const String Gold = "Gold";
                public const String Red = "Red";
                public const String Silver = "Silver";
                public const String White = "White";
            }
        }
    }
}