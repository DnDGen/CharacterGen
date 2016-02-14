# CharacterGen

Generates a random, equipped character for Dungeons and Dragons 3.X

[![Build Status](https://travis-ci.org/DnDGen/CharacterGen.svg?branch=master)](https://travis-ci.org/DnDGen/CharacterGen)

### Use

To use CharacterGen, simply use the CharacterGenerator.  Pass in the randomizers that you wish to use.  If you wish to generate leadership and followers for a character, use the Generate methods on the LeadershipGenerator.

```C#
var character = characterGenerator.GenerateWith(anyAlignmentRandomizer, anyPlayerClassNameRandomizer, anyLevelRandomizer, anyBaseRaceRandomizzer, anyMetaraceRandomizer, rawStatsRandomizer);
var leadership = leadershipGenerator.GenerateLeadership(6, 3, "Pseudodragon");
var cohort = leadershipGenerator.GenerateCohort(leadership.CohortScore, character.Class.Level, character.Alignment.Full, character.Class.ClassName);
var follower = leadershipGenerator.GenerateFollower(1, character.Alignment.Full, character.Class.ClassName);
```

### Getting the Generators

You can obtain generators from the bootstrapper project.  Because the generators are very complex and are decorated in various ways, there is not a (recommended) way to build these generator manually.  Please use the Bootstrapper package.  **Note:** if using the CharacterGen bootstrapper, be sure to also load modules for RollGen and TreasureGen, as it is dependent on those modules

```C#
var kernel = new StandardKernel();
var rollGenModuleLoader = new RollGenModuleLoader();
var treasureGenModuleLoader = new TreasureGenModuleLoader();
var characterGenModuleLoader = new CharacterGenModuleLoader();

rollGenModuleLoader.LoadModules(kernel);
treasureGenModuleLoader.LoadModules(kernel);
characterGenModuleLoader.LoadModules(kernel);
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.)

### Getting the Randomizers

If you want a set randomizer, inject and instance of of the set randomizer interface (such as `ISetClassNameRandomizer`).  If you want a general randomizer, inject a named instance of the randomizer

```C#
[Inject, Named(RaceRandomizerTypeConstants.BaseRace.StandardBase)]
public RaceRandomizer StandardBaseRaceRandomizer { get; set; }
```

### Installing CharacterGen

The project is on [Nuget](https://www.nuget.org/packages/CharacterGen). Install via the NuGet Package Manager.

    PM > Install-Package CharacterGen

#### There's CharacterGen and CharacterGen.Bootstrap - which do I install?

That depends on your project.  If you are making a library that will only **reference** CharacterGen, but does not expressly implement it (such as the EncounterGen project), then you only need the CharacterGen package.  If you actually want to run and implement the dice (such as on the DnDGenSite or in the tests for EncounterGen), then you need CharacterGen.Bootstrap, which will install CharacterGen as a dependency.
