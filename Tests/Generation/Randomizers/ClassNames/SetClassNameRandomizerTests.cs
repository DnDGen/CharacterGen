using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
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