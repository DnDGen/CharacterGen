﻿using CharacterGen.Common.Races;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Common.Races
{
    [TestFixture]
    public class RaceConstantsTests
    {
        [TestCase(RaceConstants.BaseRaces.Aasimar, "Aasimar")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "Bugbear")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "Deep Dwarf")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "Deep Halfling")]
        [TestCase(RaceConstants.BaseRaces.Derro, "Derro")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "Doppelganger")]
        [TestCase(RaceConstants.BaseRaces.Drow, "Drow")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "Duergar Dwarf")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "Forest Gnome")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "Gnoll")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "Goblin")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "Gray Elf")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "Half-Elf")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "Half-Orc")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "High Elf")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "Hill Dwarf")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "Hobgoblin")]
        [TestCase(RaceConstants.BaseRaces.Human, "Human")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "Kobold")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "Lightfoot Halfling")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "Lizardfolk")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "Mind Flayer")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "Minotaur")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "Mountain Dwarf")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "Ogre")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "Ogre Mage")]
        [TestCase(RaceConstants.BaseRaces.Orc, "Orc")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "Rock Gnome")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "Svirfneblin")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "Tallfellow Halfling")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "Tiefling")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "Troglodyte")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "Wild Elf")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "Wood Elf")]
        [TestCase(RaceConstants.BaseRaces.Animals.Badger, "Badger")]
        [TestCase(RaceConstants.BaseRaces.Animals.Camel, "Camel")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireRat, "Dire Rat")]
        [TestCase(RaceConstants.BaseRaces.Animals.Dog, "Dog")]
        [TestCase(RaceConstants.BaseRaces.Animals.RidingDog, "Riding Dog")]
        [TestCase(RaceConstants.BaseRaces.Animals.Eagle, "Eagle")]
        [TestCase(RaceConstants.BaseRaces.Animals.Hawk, "Hawk")]
        [TestCase(RaceConstants.BaseRaces.Animals.LightHorse, "Light Horse")]
        [TestCase(RaceConstants.BaseRaces.Animals.HeavyHorse, "Heavy Horse")]
        [TestCase(RaceConstants.BaseRaces.Animals.Owl, "Owl")]
        [TestCase(RaceConstants.BaseRaces.Animals.Pony, "Pony")]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallViperSnake, "Small Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.MediumViperSnake, "Medium Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.Wolf, "Wolf")]
        [TestCase(RaceConstants.BaseRaces.Animals.Ape, "Ape")]
        [TestCase(RaceConstants.BaseRaces.Animals.BlackBear, "Black Bear")]
        [TestCase(RaceConstants.BaseRaces.Animals.Bison, "Bison")]
        [TestCase(RaceConstants.BaseRaces.Animals.Boar, "Boar")]
        [TestCase(RaceConstants.BaseRaces.Animals.Cheetah, "Cheetah")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBadger, "Dire Badger")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBat, "Dire Bat")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireWeasel, "Dire Weasel")]
        [TestCase(RaceConstants.BaseRaces.Animals.Leopard, "Leopard")]
        [TestCase(RaceConstants.BaseRaces.Animals.MonitorLizard, "Monitor Lizard")]
        [TestCase(RaceConstants.BaseRaces.Animals.ConstrictorSnake, "Constrictor Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.LargeViperSnake, "Large Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.Wolverine, "Wolverine")]
        [TestCase(RaceConstants.BaseRaces.Animals.BrownBear, "Brown Bear")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireWolverine, "Dire Wolverine")]
        [TestCase(RaceConstants.BaseRaces.Animals.Deinonychus, "Deinonychus")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireApe, "Dire Ape")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBoar, "Dire Boar")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireWolf, "Dire Wolf")]
        [TestCase(RaceConstants.BaseRaces.Animals.Lion, "Lion")]
        [TestCase(RaceConstants.BaseRaces.Animals.Rhinoceras, "Rhinoceras")]
        [TestCase(RaceConstants.BaseRaces.Animals.HugeViperSnake, "Huge Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.Tiger, "Tiger")]
        [TestCase(RaceConstants.BaseRaces.Animals.PolarBear, "Polar Bear")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireLion, "Dire Lion")]
        [TestCase(RaceConstants.BaseRaces.Animals.Megaraptor, "Megaraptor")]
        [TestCase(RaceConstants.BaseRaces.Animals.GiantConstrictorSnake, "Giant Constrictor Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBear, "Dire Bear")]
        [TestCase(RaceConstants.BaseRaces.Animals.Elephant, "Elephant")]
        [TestCase(RaceConstants.BaseRaces.Animals.DireTiger, "Dire Tiger")]
        [TestCase(RaceConstants.BaseRaces.Animals.Triceratops, "Triceratops")]
        [TestCase(RaceConstants.BaseRaces.Animals.Tyrannosaurus, "Tyrannosaurus")]
        [TestCase(RaceConstants.BaseRaces.Animals.HeavyWarhorse, "Heavy Warhorse")]
        [TestCase(RaceConstants.BaseRaces.Animals.Warpony, "Warpony")]
        [TestCase(RaceConstants.BaseRaces.Animals.Bat, "Bat")]
        [TestCase(RaceConstants.BaseRaces.Animals.Cat, "Cat")]
        [TestCase(RaceConstants.BaseRaces.Animals.Lizard, "Lizard")]
        [TestCase(RaceConstants.BaseRaces.Animals.Rat, "Rat")]
        [TestCase(RaceConstants.BaseRaces.Animals.Raven, "Raven")]
        [TestCase(RaceConstants.BaseRaces.Animals.TinyViperSnake, "Tiny Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.Toad, "Toad")]
        [TestCase(RaceConstants.BaseRaces.Animals.Weasel, "Weasel")]
        [TestCase(RaceConstants.BaseRaces.Animals.ShockerLizard, "Shocker Lizard")]
        [TestCase(RaceConstants.BaseRaces.Animals.Stirge, "Stirge")]
        [TestCase(RaceConstants.BaseRaces.Animals.FormianWorker, "Formian Worker")]
        [TestCase(RaceConstants.BaseRaces.Animals.Imp, "Imp")]
        [TestCase(RaceConstants.BaseRaces.Animals.Pseudodragon, "Pseudodragon")]
        [TestCase(RaceConstants.BaseRaces.Animals.Quasit, "Quasit")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialBat, "Celestial Bat")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialCat, "Celestial Cat")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialHawk, "Celestial Hawk")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialLizard, "Celestial Lizard")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialOwl, "Celestial Owl")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialRat, "Celestial Rat")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialRaven, "Celestial Raven")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialTinyViperSnake, "Celestial Tiny Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialToad, "Celestial Toad")]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialWeasel, "Celestial Weasel")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishBat, "Fiendish Bat")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishCat, "Fiendish Cat")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishHawk, "Fiendish Hawk")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishLizard, "Fiendish Lizard")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishOwl, "Fiendish Owl")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishRat, "Fiendish Rat")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishRaven, "Fiendish Raven")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishTinyViperSnake, "Fiendish Tiny Viper Snake")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishToad, "Fiendish Toad")]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishWeasel, "Fiendish Weasel")]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallAirElemental, "Small Air Elemental")]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallEarthElemental, "Small Earth Elemental")]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallFireElemental, "Small Fire Elemental")]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallWaterElemental, "Small Water Elemental")]
        [TestCase(RaceConstants.BaseRaces.Animals.Homonculus, "Homonculus")]
        [TestCase(RaceConstants.BaseRaces.Animals.AirMephit, "Air Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.DustMephit, "Dust Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.EarthMephit, "Earth Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.FireMephit, "Fire Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.IceMephit, "Ice Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.MagmaMephit, "Magma Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.OozeMephit, "Ooze Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.SaltMephit, "Salt Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.SteamMephit, "Steam Mephit")]
        [TestCase(RaceConstants.BaseRaces.Animals.WaterMephit, "Water Mephit")]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, "Half-Celestial")]
        [TestCase(RaceConstants.Metaraces.HalfDragon, "Half-Dragon")]
        [TestCase(RaceConstants.Metaraces.HalfFiend, "Half-Fiend")]
        [TestCase(RaceConstants.Metaraces.Werebear, "Werebear")]
        [TestCase(RaceConstants.Metaraces.Wereboar, "Wereboar")]
        [TestCase(RaceConstants.Metaraces.Wererat, "Wererat")]
        [TestCase(RaceConstants.Metaraces.Weretiger, "Weretiger")]
        [TestCase(RaceConstants.Metaraces.Werewolf, "Werewolf")]
        [TestCase(RaceConstants.Metaraces.None, "")]
        [TestCase(RaceConstants.Sizes.Large, "Large")]
        [TestCase(RaceConstants.Sizes.Medium, "Medium")]
        [TestCase(RaceConstants.Sizes.Small, "Small")]
        [TestCase(RaceConstants.Metaraces.Species.Bronze, "Bronze")]
        [TestCase(RaceConstants.Metaraces.Species.Black, "Black")]
        [TestCase(RaceConstants.Metaraces.Species.Blue, "Blue")]
        [TestCase(RaceConstants.Metaraces.Species.Brass, "Brass")]
        [TestCase(RaceConstants.Metaraces.Species.Copper, "Copper")]
        [TestCase(RaceConstants.Metaraces.Species.Gold, "Gold")]
        [TestCase(RaceConstants.Metaraces.Species.Green, "Green")]
        [TestCase(RaceConstants.Metaraces.Species.Red, "Red")]
        [TestCase(RaceConstants.Metaraces.Species.Silver, "Silver")]
        [TestCase(RaceConstants.Metaraces.Species.White, "White")]
        [TestCase(RaceConstants.Ages.Adulthood, "Adulthood")]
        [TestCase(RaceConstants.Ages.MiddleAge, "Middle Age")]
        [TestCase(RaceConstants.Ages.Old, "Old")]
        [TestCase(RaceConstants.Ages.Venerable, "Venerable")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}