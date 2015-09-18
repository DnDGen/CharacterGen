using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class LevelAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.LevelAdjustments; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WoodElf,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Werewolf,
                RaceConstants.BaseRaces.Animals.Ape,
                RaceConstants.BaseRaces.Animals.Badger,
                RaceConstants.BaseRaces.Animals.Bat,
                RaceConstants.BaseRaces.Animals.Bison,
                RaceConstants.BaseRaces.Animals.BlackBear,
                RaceConstants.BaseRaces.Animals.Boar,
                RaceConstants.BaseRaces.Animals.BrownBear,
                RaceConstants.BaseRaces.Animals.Camel,
                RaceConstants.BaseRaces.Animals.Cat,
                RaceConstants.BaseRaces.Animals.CelestialBat,
                RaceConstants.BaseRaces.Animals.CelestialCat,
                RaceConstants.BaseRaces.Animals.CelestialHawk,
                RaceConstants.BaseRaces.Animals.CelestialLizard,
                RaceConstants.BaseRaces.Animals.CelestialOwl,
                RaceConstants.BaseRaces.Animals.CelestialRat,
                RaceConstants.BaseRaces.Animals.CelestialRaven,
                RaceConstants.BaseRaces.Animals.CelestialTinyViperSnake,
                RaceConstants.BaseRaces.Animals.CelestialToad,
                RaceConstants.BaseRaces.Animals.CelestialWeasel,
                RaceConstants.BaseRaces.Animals.Cheetah,
                RaceConstants.BaseRaces.Animals.ConstrictorSnake,
                RaceConstants.BaseRaces.Animals.Deinonychus,
                RaceConstants.BaseRaces.Animals.DireApe,
                RaceConstants.BaseRaces.Animals.DireBadger,
                RaceConstants.BaseRaces.Animals.DireBat,
                RaceConstants.BaseRaces.Animals.DireBear,
                RaceConstants.BaseRaces.Animals.DireBoar,
                RaceConstants.BaseRaces.Animals.DireLion,
                RaceConstants.BaseRaces.Animals.DireRat,
                RaceConstants.BaseRaces.Animals.DireTiger,
                RaceConstants.BaseRaces.Animals.DireWeasel,
                RaceConstants.BaseRaces.Animals.DireWolf,
                RaceConstants.BaseRaces.Animals.DireWolverine,
                RaceConstants.BaseRaces.Animals.Dog,
                RaceConstants.BaseRaces.Animals.Eagle,
                RaceConstants.BaseRaces.Animals.Elephant,
                RaceConstants.BaseRaces.Animals.FiendishBat,
                RaceConstants.BaseRaces.Animals.FiendishCat,
                RaceConstants.BaseRaces.Animals.FiendishHawk,
                RaceConstants.BaseRaces.Animals.FiendishLizard,
                RaceConstants.BaseRaces.Animals.FiendishOwl,
                RaceConstants.BaseRaces.Animals.FiendishRat,
                RaceConstants.BaseRaces.Animals.FiendishRaven,
                RaceConstants.BaseRaces.Animals.FiendishTinyViperSnake,
                RaceConstants.BaseRaces.Animals.FiendishToad,
                RaceConstants.BaseRaces.Animals.FiendishWeasel,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.GiantConstrictorSnake,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.HeavyHorse,
                RaceConstants.BaseRaces.Animals.HeavyWarhorse,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.HugeViperSnake,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.LargeViperSnake,
                RaceConstants.BaseRaces.Animals.Leopard,
                RaceConstants.BaseRaces.Animals.LightHorse,
                RaceConstants.BaseRaces.Animals.Lion,
                RaceConstants.BaseRaces.Animals.Lizard,
                RaceConstants.BaseRaces.Animals.MediumViperSnake,
                RaceConstants.BaseRaces.Animals.Megaraptor,
                RaceConstants.BaseRaces.Animals.MonitorLizard,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.PolarBear,
                RaceConstants.BaseRaces.Animals.Pony,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
                RaceConstants.BaseRaces.Animals.Rat,
                RaceConstants.BaseRaces.Animals.Raven,
                RaceConstants.BaseRaces.Animals.Rhinoceras,
                RaceConstants.BaseRaces.Animals.RidingDog,
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallViperSnake,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.Tiger,
                RaceConstants.BaseRaces.Animals.TinyViperSnake,
                RaceConstants.BaseRaces.Animals.Toad,
                RaceConstants.BaseRaces.Animals.Triceratops,
                RaceConstants.BaseRaces.Animals.Tyrannosaurus,
                RaceConstants.BaseRaces.Animals.Warpony,
                RaceConstants.BaseRaces.Animals.Weasel,
                RaceConstants.BaseRaces.Animals.Wolf,
                RaceConstants.BaseRaces.Animals.Wolverine
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, -2)]
        [TestCase(RaceConstants.BaseRaces.Derro, -1)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, -3)]
        [TestCase(RaceConstants.BaseRaces.Drow, -1)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, -1)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, -1)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, -1)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, -1)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, -2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, -2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, -8)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, -4)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -2)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, -8)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, -1)]
        [TestCase(RaceConstants.Metaraces.Werebear, -2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, -1)]
        [TestCase(RaceConstants.Metaraces.Wererat, -1)]
        [TestCase(RaceConstants.Metaraces.Weretiger, -1)]
        [TestCase(RaceConstants.Metaraces.Werewolf, -1)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Badger, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Camel, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireRat, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Dog, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.RidingDog, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Eagle, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Hawk, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.LightHorse, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.HeavyHorse, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Owl, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Pony, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallViperSnake, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.MediumViperSnake, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Wolf, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Ape, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.BlackBear, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.Bison, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.Boar, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.Cheetah, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBadger, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBat, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireWeasel, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.Leopard, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.MonitorLizard, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.ConstrictorSnake, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.LargeViperSnake, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.Wolverine, -3)]
        [TestCase(RaceConstants.BaseRaces.Animals.BrownBear, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireWolverine, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Deinonychus, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireApe, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBoar, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireWolf, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Lion, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Rhinoceras, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.HugeViperSnake, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Tiger, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.PolarBear, -9)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireLion, -9)]
        [TestCase(RaceConstants.BaseRaces.Animals.Megaraptor, -9)]
        [TestCase(RaceConstants.BaseRaces.Animals.GiantConstrictorSnake, -9)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireBear, -12)]
        [TestCase(RaceConstants.BaseRaces.Animals.Elephant, -12)]
        [TestCase(RaceConstants.BaseRaces.Animals.DireTiger, -15)]
        [TestCase(RaceConstants.BaseRaces.Animals.Triceratops, -15)]
        [TestCase(RaceConstants.BaseRaces.Animals.Tyrannosaurus, -15)]
        [TestCase(RaceConstants.BaseRaces.Animals.Bat, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Cat, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Lizard, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Rat, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Raven, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.TinyViperSnake, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Toad, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.Weasel, 0)]
        [TestCase(RaceConstants.BaseRaces.Animals.ShockerLizard, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.Stirge, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.FormianWorker, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Imp, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Pseudodragon, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.Quasit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialBat, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialCat, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialHawk, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialLizard, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialOwl, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialRat, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialRaven, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialTinyViperSnake, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialToad, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.CelestialWeasel, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishBat, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishCat, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishHawk, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishLizard, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishOwl, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishRat, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishRaven, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishTinyViperSnake, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishToad, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.FiendishWeasel, -2)]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallAirElemental, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallEarthElemental, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallFireElemental, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.SmallWaterElemental, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.Homonculus, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.AirMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.DustMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.EarthMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.FireMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.IceMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.MagmaMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.OozeMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.SaltMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.SteamMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.WaterMephit, -6)]
        [TestCase(RaceConstants.BaseRaces.Animals.HeavyWarhorse, -4)]
        [TestCase(RaceConstants.BaseRaces.Animals.Warpony, -4)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}