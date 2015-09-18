﻿using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Magics
{
    [TestFixture]
    public class AnimalsTests : CollectionTests
    {
        protected override String tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.Animals;
            }
        }

        public override void CollectionNames()
        {
            var names = new[]
            {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard,
                RaceConstants.Sizes.Large,
                RaceConstants.Sizes.Medium,
                RaceConstants.Sizes.Small,
                FeatConstants.ImprovedFamiliar
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin,
            RaceConstants.BaseRaces.Animals.HeavyWarhorse,
            RaceConstants.BaseRaces.Animals.Warpony)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void SorcererAnimals()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.Bat,
                RaceConstants.BaseRaces.Animals.Cat,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.Lizard,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.Rat,
                RaceConstants.BaseRaces.Animals.Raven,
                RaceConstants.BaseRaces.Animals.TinyViperSnake,
                RaceConstants.BaseRaces.Animals.Toad,
                RaceConstants.BaseRaces.Animals.Weasel,
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
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
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit
            };

            DistinctCollection(CharacterClassConstants.Sorcerer, animals);
        }

        [Test]
        public void WizardAnimals()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.Bat,
                RaceConstants.BaseRaces.Animals.Cat,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.Lizard,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.Rat,
                RaceConstants.BaseRaces.Animals.Raven,
                RaceConstants.BaseRaces.Animals.TinyViperSnake,
                RaceConstants.BaseRaces.Animals.Toad,
                RaceConstants.BaseRaces.Animals.Weasel,
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
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
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit
            };

            DistinctCollection(CharacterClassConstants.Wizard, animals);
        }

        [Test]
        public void ImprovedFamiliars()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
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
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit
            };

            DistinctCollection(FeatConstants.ImprovedFamiliar, animals);
        }

        [Test]
        public void DruidAnimals()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.Badger,
                RaceConstants.BaseRaces.Animals.Camel,
                RaceConstants.BaseRaces.Animals.DireRat,
                RaceConstants.BaseRaces.Animals.Dog,
                RaceConstants.BaseRaces.Animals.RidingDog,
                RaceConstants.BaseRaces.Animals.Eagle,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.LightHorse,
                RaceConstants.BaseRaces.Animals.HeavyHorse,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.Pony,
                RaceConstants.BaseRaces.Animals.SmallViperSnake,
                RaceConstants.BaseRaces.Animals.MediumViperSnake,
                RaceConstants.BaseRaces.Animals.Wolf,
                RaceConstants.BaseRaces.Animals.Ape,
                RaceConstants.BaseRaces.Animals.BlackBear,
                RaceConstants.BaseRaces.Animals.Bison,
                RaceConstants.BaseRaces.Animals.Boar,
                RaceConstants.BaseRaces.Animals.Cheetah,
                RaceConstants.BaseRaces.Animals.DireBadger,
                RaceConstants.BaseRaces.Animals.DireBat,
                RaceConstants.BaseRaces.Animals.DireWeasel,
                RaceConstants.BaseRaces.Animals.Leopard,
                RaceConstants.BaseRaces.Animals.MonitorLizard,
                RaceConstants.BaseRaces.Animals.ConstrictorSnake,
                RaceConstants.BaseRaces.Animals.LargeViperSnake,
                RaceConstants.BaseRaces.Animals.Wolverine,
                RaceConstants.BaseRaces.Animals.BrownBear,
                RaceConstants.BaseRaces.Animals.DireWolverine,
                RaceConstants.BaseRaces.Animals.Deinonychus,
                RaceConstants.BaseRaces.Animals.DireApe,
                RaceConstants.BaseRaces.Animals.DireBoar,
                RaceConstants.BaseRaces.Animals.DireWolf,
                RaceConstants.BaseRaces.Animals.Lion,
                RaceConstants.BaseRaces.Animals.Rhinoceras,
                RaceConstants.BaseRaces.Animals.HugeViperSnake,
                RaceConstants.BaseRaces.Animals.Tiger,
                RaceConstants.BaseRaces.Animals.PolarBear,
                RaceConstants.BaseRaces.Animals.DireLion,
                RaceConstants.BaseRaces.Animals.Megaraptor,
                RaceConstants.BaseRaces.Animals.GiantConstrictorSnake,
                RaceConstants.BaseRaces.Animals.DireBear,
                RaceConstants.BaseRaces.Animals.Elephant,
                RaceConstants.BaseRaces.Animals.DireTiger,
                RaceConstants.BaseRaces.Animals.Triceratops,
                RaceConstants.BaseRaces.Animals.Tyrannosaurus
            };

            DistinctCollection(CharacterClassConstants.Druid, animals);
        }

        [Test]
        public void AnimalsForSmallRaces()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.Badger,
                RaceConstants.BaseRaces.Animals.Camel,
                RaceConstants.BaseRaces.Animals.DireRat,
                RaceConstants.BaseRaces.Animals.Dog,
                RaceConstants.BaseRaces.Animals.RidingDog,
                RaceConstants.BaseRaces.Animals.Eagle,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.LightHorse,
                RaceConstants.BaseRaces.Animals.HeavyHorse,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.Pony,
                RaceConstants.BaseRaces.Animals.SmallViperSnake,
                RaceConstants.BaseRaces.Animals.MediumViperSnake,
                RaceConstants.BaseRaces.Animals.Wolf,
                RaceConstants.BaseRaces.Animals.Ape,
                RaceConstants.BaseRaces.Animals.BlackBear,
                RaceConstants.BaseRaces.Animals.Bison,
                RaceConstants.BaseRaces.Animals.Boar,
                RaceConstants.BaseRaces.Animals.Cheetah,
                RaceConstants.BaseRaces.Animals.DireBadger,
                RaceConstants.BaseRaces.Animals.DireBat,
                RaceConstants.BaseRaces.Animals.DireWeasel,
                RaceConstants.BaseRaces.Animals.Leopard,
                RaceConstants.BaseRaces.Animals.MonitorLizard,
                RaceConstants.BaseRaces.Animals.ConstrictorSnake,
                RaceConstants.BaseRaces.Animals.LargeViperSnake,
                RaceConstants.BaseRaces.Animals.Wolverine,
                RaceConstants.BaseRaces.Animals.BrownBear,
                RaceConstants.BaseRaces.Animals.DireWolverine,
                RaceConstants.BaseRaces.Animals.Deinonychus,
                RaceConstants.BaseRaces.Animals.DireApe,
                RaceConstants.BaseRaces.Animals.DireBoar,
                RaceConstants.BaseRaces.Animals.DireWolf,
                RaceConstants.BaseRaces.Animals.Lion,
                RaceConstants.BaseRaces.Animals.Rhinoceras,
                RaceConstants.BaseRaces.Animals.HugeViperSnake,
                RaceConstants.BaseRaces.Animals.Tiger,
                RaceConstants.BaseRaces.Animals.PolarBear,
                RaceConstants.BaseRaces.Animals.DireLion,
                RaceConstants.BaseRaces.Animals.Megaraptor,
                RaceConstants.BaseRaces.Animals.GiantConstrictorSnake,
                RaceConstants.BaseRaces.Animals.DireBear,
                RaceConstants.BaseRaces.Animals.Elephant,
                RaceConstants.BaseRaces.Animals.DireTiger,
                RaceConstants.BaseRaces.Animals.Triceratops,
                RaceConstants.BaseRaces.Animals.Tyrannosaurus,
                RaceConstants.BaseRaces.Animals.Warpony,
                RaceConstants.BaseRaces.Animals.Bat,
                RaceConstants.BaseRaces.Animals.Cat,
                RaceConstants.BaseRaces.Animals.Lizard,
                RaceConstants.BaseRaces.Animals.Rat,
                RaceConstants.BaseRaces.Animals.Raven,
                RaceConstants.BaseRaces.Animals.TinyViperSnake,
                RaceConstants.BaseRaces.Animals.Toad,
                RaceConstants.BaseRaces.Animals.Weasel,
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
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
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit
            };

            DistinctCollection(RaceConstants.Sizes.Small, animals);
        }

        [Test]
        public void AnimalsForMediumRaces()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.Badger,
                RaceConstants.BaseRaces.Animals.Camel,
                RaceConstants.BaseRaces.Animals.DireRat,
                RaceConstants.BaseRaces.Animals.Dog,
                RaceConstants.BaseRaces.Animals.RidingDog,
                RaceConstants.BaseRaces.Animals.Eagle,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.LightHorse,
                RaceConstants.BaseRaces.Animals.HeavyHorse,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.Pony,
                RaceConstants.BaseRaces.Animals.SmallViperSnake,
                RaceConstants.BaseRaces.Animals.MediumViperSnake,
                RaceConstants.BaseRaces.Animals.Wolf,
                RaceConstants.BaseRaces.Animals.Ape,
                RaceConstants.BaseRaces.Animals.BlackBear,
                RaceConstants.BaseRaces.Animals.Bison,
                RaceConstants.BaseRaces.Animals.Boar,
                RaceConstants.BaseRaces.Animals.Cheetah,
                RaceConstants.BaseRaces.Animals.DireBadger,
                RaceConstants.BaseRaces.Animals.DireBat,
                RaceConstants.BaseRaces.Animals.DireWeasel,
                RaceConstants.BaseRaces.Animals.Leopard,
                RaceConstants.BaseRaces.Animals.MonitorLizard,
                RaceConstants.BaseRaces.Animals.ConstrictorSnake,
                RaceConstants.BaseRaces.Animals.LargeViperSnake,
                RaceConstants.BaseRaces.Animals.Wolverine,
                RaceConstants.BaseRaces.Animals.BrownBear,
                RaceConstants.BaseRaces.Animals.DireWolverine,
                RaceConstants.BaseRaces.Animals.Deinonychus,
                RaceConstants.BaseRaces.Animals.DireApe,
                RaceConstants.BaseRaces.Animals.DireBoar,
                RaceConstants.BaseRaces.Animals.DireWolf,
                RaceConstants.BaseRaces.Animals.Lion,
                RaceConstants.BaseRaces.Animals.Rhinoceras,
                RaceConstants.BaseRaces.Animals.HugeViperSnake,
                RaceConstants.BaseRaces.Animals.Tiger,
                RaceConstants.BaseRaces.Animals.PolarBear,
                RaceConstants.BaseRaces.Animals.DireLion,
                RaceConstants.BaseRaces.Animals.Megaraptor,
                RaceConstants.BaseRaces.Animals.GiantConstrictorSnake,
                RaceConstants.BaseRaces.Animals.DireBear,
                RaceConstants.BaseRaces.Animals.Elephant,
                RaceConstants.BaseRaces.Animals.DireTiger,
                RaceConstants.BaseRaces.Animals.Triceratops,
                RaceConstants.BaseRaces.Animals.Tyrannosaurus,
                RaceConstants.BaseRaces.Animals.HeavyWarhorse,
                RaceConstants.BaseRaces.Animals.Bat,
                RaceConstants.BaseRaces.Animals.Cat,
                RaceConstants.BaseRaces.Animals.Lizard,
                RaceConstants.BaseRaces.Animals.Rat,
                RaceConstants.BaseRaces.Animals.Raven,
                RaceConstants.BaseRaces.Animals.TinyViperSnake,
                RaceConstants.BaseRaces.Animals.Toad,
                RaceConstants.BaseRaces.Animals.Weasel,
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
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
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit
            };

            DistinctCollection(RaceConstants.Sizes.Medium, animals);
        }

        [Test]
        public void AnimalsForLargeRaces()
        {
            var animals = new[]
            {
                RaceConstants.BaseRaces.Animals.Badger,
                RaceConstants.BaseRaces.Animals.Camel,
                RaceConstants.BaseRaces.Animals.DireRat,
                RaceConstants.BaseRaces.Animals.Dog,
                RaceConstants.BaseRaces.Animals.RidingDog,
                RaceConstants.BaseRaces.Animals.Eagle,
                RaceConstants.BaseRaces.Animals.Hawk,
                RaceConstants.BaseRaces.Animals.LightHorse,
                RaceConstants.BaseRaces.Animals.HeavyHorse,
                RaceConstants.BaseRaces.Animals.Owl,
                RaceConstants.BaseRaces.Animals.Pony,
                RaceConstants.BaseRaces.Animals.SmallViperSnake,
                RaceConstants.BaseRaces.Animals.MediumViperSnake,
                RaceConstants.BaseRaces.Animals.Wolf,
                RaceConstants.BaseRaces.Animals.Ape,
                RaceConstants.BaseRaces.Animals.BlackBear,
                RaceConstants.BaseRaces.Animals.Bison,
                RaceConstants.BaseRaces.Animals.Boar,
                RaceConstants.BaseRaces.Animals.Cheetah,
                RaceConstants.BaseRaces.Animals.DireBadger,
                RaceConstants.BaseRaces.Animals.DireBat,
                RaceConstants.BaseRaces.Animals.DireWeasel,
                RaceConstants.BaseRaces.Animals.Leopard,
                RaceConstants.BaseRaces.Animals.MonitorLizard,
                RaceConstants.BaseRaces.Animals.ConstrictorSnake,
                RaceConstants.BaseRaces.Animals.LargeViperSnake,
                RaceConstants.BaseRaces.Animals.Wolverine,
                RaceConstants.BaseRaces.Animals.BrownBear,
                RaceConstants.BaseRaces.Animals.DireWolverine,
                RaceConstants.BaseRaces.Animals.Deinonychus,
                RaceConstants.BaseRaces.Animals.DireApe,
                RaceConstants.BaseRaces.Animals.DireBoar,
                RaceConstants.BaseRaces.Animals.DireWolf,
                RaceConstants.BaseRaces.Animals.Lion,
                RaceConstants.BaseRaces.Animals.Rhinoceras,
                RaceConstants.BaseRaces.Animals.HugeViperSnake,
                RaceConstants.BaseRaces.Animals.Tiger,
                RaceConstants.BaseRaces.Animals.PolarBear,
                RaceConstants.BaseRaces.Animals.DireLion,
                RaceConstants.BaseRaces.Animals.Megaraptor,
                RaceConstants.BaseRaces.Animals.GiantConstrictorSnake,
                RaceConstants.BaseRaces.Animals.DireBear,
                RaceConstants.BaseRaces.Animals.Elephant,
                RaceConstants.BaseRaces.Animals.DireTiger,
                RaceConstants.BaseRaces.Animals.Triceratops,
                RaceConstants.BaseRaces.Animals.Tyrannosaurus,
                RaceConstants.BaseRaces.Animals.Bat,
                RaceConstants.BaseRaces.Animals.Cat,
                RaceConstants.BaseRaces.Animals.Lizard,
                RaceConstants.BaseRaces.Animals.Rat,
                RaceConstants.BaseRaces.Animals.Raven,
                RaceConstants.BaseRaces.Animals.TinyViperSnake,
                RaceConstants.BaseRaces.Animals.Toad,
                RaceConstants.BaseRaces.Animals.Weasel,
                RaceConstants.BaseRaces.Animals.ShockerLizard,
                RaceConstants.BaseRaces.Animals.Stirge,
                RaceConstants.BaseRaces.Animals.FormianWorker,
                RaceConstants.BaseRaces.Animals.Imp,
                RaceConstants.BaseRaces.Animals.Pseudodragon,
                RaceConstants.BaseRaces.Animals.Quasit,
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
                RaceConstants.BaseRaces.Animals.SmallAirElemental,
                RaceConstants.BaseRaces.Animals.SmallEarthElemental,
                RaceConstants.BaseRaces.Animals.SmallFireElemental,
                RaceConstants.BaseRaces.Animals.SmallWaterElemental,
                RaceConstants.BaseRaces.Animals.Homonculus,
                RaceConstants.BaseRaces.Animals.AirMephit,
                RaceConstants.BaseRaces.Animals.DustMephit,
                RaceConstants.BaseRaces.Animals.EarthMephit,
                RaceConstants.BaseRaces.Animals.FireMephit,
                RaceConstants.BaseRaces.Animals.IceMephit,
                RaceConstants.BaseRaces.Animals.MagmaMephit,
                RaceConstants.BaseRaces.Animals.OozeMephit,
                RaceConstants.BaseRaces.Animals.SaltMephit,
                RaceConstants.BaseRaces.Animals.SteamMephit,
                RaceConstants.BaseRaces.Animals.WaterMephit
            };

            DistinctCollection(RaceConstants.Sizes.Large, animals);
        }
    }
}