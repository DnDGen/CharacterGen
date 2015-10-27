using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnimalBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.AnimalBase)]
        public override RaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.Animals.AirMephit,
                    RaceConstants.BaseRaces.Animals.Ape,
                    RaceConstants.BaseRaces.Animals.Badger,
                    RaceConstants.BaseRaces.Animals.Bat,
                    RaceConstants.BaseRaces.Animals.Bison,
                    RaceConstants.BaseRaces.Animals.BlackBear,
                    RaceConstants.BaseRaces.Animals.Boar,
                    RaceConstants.BaseRaces.Animals.BrownBear,
                    RaceConstants.BaseRaces.Animals.Cat,
                    RaceConstants.BaseRaces.Animals.Camel,
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
                    RaceConstants.BaseRaces.Animals.DustMephit,
                    RaceConstants.BaseRaces.Animals.Eagle,
                    RaceConstants.BaseRaces.Animals.EarthMephit,
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
                    RaceConstants.BaseRaces.Animals.FireMephit,
                    RaceConstants.BaseRaces.Animals.FormianWorker,
                    RaceConstants.BaseRaces.Animals.GiantConstrictorSnake,
                    RaceConstants.BaseRaces.Animals.Hawk,
                    RaceConstants.BaseRaces.Animals.HeavyHorse,
                    RaceConstants.BaseRaces.Animals.HeavyWarhorse,
                    RaceConstants.BaseRaces.Animals.Homonculus,
                    RaceConstants.BaseRaces.Animals.HugeViperSnake,
                    RaceConstants.BaseRaces.Animals.IceMephit,
                    RaceConstants.BaseRaces.Animals.Imp,
                    RaceConstants.BaseRaces.Animals.LargeViperSnake,
                    RaceConstants.BaseRaces.Animals.Leopard,
                    RaceConstants.BaseRaces.Animals.LightHorse,
                    RaceConstants.BaseRaces.Animals.Lion,
                    RaceConstants.BaseRaces.Animals.Lizard,
                    RaceConstants.BaseRaces.Animals.MagmaMephit,
                    RaceConstants.BaseRaces.Animals.MediumViperSnake,
                    RaceConstants.BaseRaces.Animals.Megaraptor,
                    RaceConstants.BaseRaces.Animals.MonitorLizard,
                    RaceConstants.BaseRaces.Animals.OozeMephit,
                    RaceConstants.BaseRaces.Animals.Owl,
                    RaceConstants.BaseRaces.Animals.PolarBear,
                    RaceConstants.BaseRaces.Animals.Pony,
                    RaceConstants.BaseRaces.Animals.Pseudodragon,
                    RaceConstants.BaseRaces.Animals.Rat,
                    RaceConstants.BaseRaces.Animals.Quasit,
                    RaceConstants.BaseRaces.Animals.Raven,
                    RaceConstants.BaseRaces.Animals.Rhinoceras,
                    RaceConstants.BaseRaces.Animals.RidingDog,
                    RaceConstants.BaseRaces.Animals.SaltMephit,
                    RaceConstants.BaseRaces.Animals.ShockerLizard,
                    RaceConstants.BaseRaces.Animals.SmallAirElemental,
                    RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                    RaceConstants.BaseRaces.Animals.SmallFireElemental,
                    RaceConstants.BaseRaces.Animals.SmallViperSnake,
                    RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                    RaceConstants.BaseRaces.Animals.SteamMephit,
                    RaceConstants.BaseRaces.Animals.Stirge,
                    RaceConstants.BaseRaces.Animals.Tiger,
                    RaceConstants.BaseRaces.Animals.TinyViperSnake,
                    RaceConstants.BaseRaces.Animals.Toad,
                    RaceConstants.BaseRaces.Animals.Triceratops,
                    RaceConstants.BaseRaces.Animals.Tyrannosaurus,
                    RaceConstants.BaseRaces.Animals.Warpony,
                    RaceConstants.BaseRaces.Animals.WaterMephit,
                    RaceConstants.BaseRaces.Animals.Weasel,
                    RaceConstants.BaseRaces.Animals.Wolf,
                    RaceConstants.BaseRaces.Animals.Wolverine,
                    String.Empty
                };
            }
        }

        [TestCase("AnimalBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}
