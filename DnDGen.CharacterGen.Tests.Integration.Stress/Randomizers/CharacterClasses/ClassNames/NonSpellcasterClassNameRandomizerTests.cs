﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<string> allowedClassNames
        {
            get
            {
                return new[]
                {
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Rogue,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Barbarian
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.NonSpellcaster);
        }

        [Test]
        public void StressNonSpellcasterClassName()
        {
            stressor.Stress(AssertClassName);
        }
    }
}