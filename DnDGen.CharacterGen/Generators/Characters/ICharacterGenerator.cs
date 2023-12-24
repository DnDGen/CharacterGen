﻿using DnDGen.CharacterGen.Characters;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DnDGen.CharacterGen.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("DnDGen.CharacterGen.Tests.Integration")]
[assembly: InternalsVisibleTo("DnDGen.CharacterGen.Tests.Integration.IoC")]
[assembly: InternalsVisibleTo("DnDGen.CharacterGen.Tests.Integration.Tables")]
namespace DnDGen.CharacterGen.Generators.Characters
{
    public interface ICharacterGenerator
    {
        Character GenerateWith(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer,
            IAbilitiesRandomizer statsRandomizer);

        CharacterPrototype GeneratePrototypeWith(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer);
    }
}