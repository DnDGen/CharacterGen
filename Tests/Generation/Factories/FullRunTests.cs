using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D20Dice.Dice;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.Levels;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class FullRunTests
    {
        [Test]
        public void GenerateCharacter()
        {
            var random = new Random();
            var dice = DiceFactory.Create(random);
            var levelRandomizer = new AnyLevelRandomizer(dice);
            var streamLoader = new EmbeddedResourceStreamLoader();
            var xmlParser = new PercentileXmlParser(streamLoader);
            var percentileResultProvider = new PercentileResultProvider(xmlParser, dice);
            var classNameRandomizer = new AnyClassNameRandomizer(percentileResultProvider);
            var characterClassFactory = new CharacterClassFactory(dice, levelRandomizer, classNameRandomizer);

            var characterFactory = new CharacterFactory(characterClassFactory, alignmentFactory, raceFactory, randomizerVerifier, statsFactory,
                characterClassVerifier, baseRaceVerifier);
        }
    }
}