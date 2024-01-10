# CharacterGen

Generates a random, equipped character for Dungeons and Dragons 3.X

[![Build Status](https://dev.azure.com/dndgen/DnDGen/_apis/build/status/DnDGen.CharacterGen?branchName=master)](https://dev.azure.com/dndgen/DnDGen/_build/latest?definitionId=19&branchName=master)

### Use

To use CharacterGen, simply use the CharacterGenerator.  Pass in the randomizers that you wish to use.  If you wish to generate leadership and followers for a character, use the Generate methods on the LeadershipGenerator.

```C#
var character = characterGenerator.GenerateWith(anyAlignmentRandomizer, anyPlayerClassNameRandomizer, anyLevelRandomizer, anyBaseRaceRandomizzer, anyMetaraceRandomizer, rawStatsRandomizer);
var leadership = leadershipGenerator.GenerateLeadership(6, 3, "Pseudodragon");
var cohort = leadershipGenerator.GenerateCohort(leadership.CohortScore, character.Class.Level, character.Alignment.Full, character.Class.ClassName);
var follower = leadershipGenerator.GenerateFollower(1, character.Alignment.Full, character.Class.ClassName);
```

### Getting the Generators

You can obtain generators via the Ninject module. Because the generators are very complex and are decorated in various ways, there is not a (recommended) way to build these generator manually. **Note:** This will also load the dependencies for CharacterGen, including TreasureGen, RollGen, and Infrastructure

```C#
var kernel = new StandardKernel();
characterGenModuleLoader.LoadModules(kernel);
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.)

### Getting the Randomizers

If you want a set randomizer, inject and instance of of the set randomizer interface (such as `ISetClassNameRandomizer`).  If you want a general randomizer, inject a named instance of the randomizer

```C#
kernel.Get<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.StandardBase);
```

### Installing CharacterGen

The project is on [Nuget](https://www.nuget.org/packages/CharacterGen). Install via the NuGet Package Manager.

    PM > Install-Package CharacterGen
