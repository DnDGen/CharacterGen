using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests
    {
        [Test]
        public void ReturnSetAlignment()
        {
            var classNameRandomizer = new SetClassNameRandomizer();
            classNameRandomizer.ClassName = "class name";

            var alignment = new Alignment();
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(classNameRandomizer.ClassName));
        }
    }
}