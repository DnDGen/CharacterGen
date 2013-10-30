using System.Linq;
using NPCGen.Core.Data;
using NUnit.Framework;

namespace NPCGen.Tests.Data
{
    [TestFixture]
    public class LanguageConstantsTests
    {
        [Test]
        public void AbyssalConstant()
        {
            Assert.That(LanguageConstants.Abyssal, Is.EqualTo("Abyssal"));
        }
        [Test]
        public void AquanConstant()
        {
            Assert.That(LanguageConstants.Aquan, Is.EqualTo("Aquan"));
        }
        [Test]
        public void AuranConstant()
        {
            Assert.That(LanguageConstants.Auran, Is.EqualTo("Auran"));
        }
        [Test]
        public void CelestialConstant()
        {
            Assert.That(LanguageConstants.Celestial, Is.EqualTo("Celestial"));
        }
        [Test]
        public void CommonConstant()
        {
            Assert.That(LanguageConstants.Common, Is.EqualTo("Common"));
        }
        [Test]
        public void DraconicConstant()
        {
            Assert.That(LanguageConstants.Draconic, Is.EqualTo("Draconic"));
        }
        [Test]
        public void DruidicConstant()
        {
            Assert.That(LanguageConstants.Druidic, Is.EqualTo("Druidic"));
        }
        [Test]
        public void DwarvenConstant()
        {
            Assert.That(LanguageConstants.Dwarven, Is.EqualTo("Dwarven"));
        }
        [Test]
        public void ElvenConstant()
        {
            Assert.That(LanguageConstants.Elven, Is.EqualTo("Elven"));
        }
        [Test]
        public void GiantConstant()
        {
            Assert.That(LanguageConstants.Giant, Is.EqualTo("Giant"));
        }
        [Test]
        public void GnollConstant()
        {
            Assert.That(LanguageConstants.Gnoll, Is.EqualTo("Gnoll"));
        }
        [Test]
        public void GnomeConstant()
        {
            Assert.That(LanguageConstants.Gnome, Is.EqualTo("Gnome"));
        }
        [Test]
        public void GoblinConstant()
        {
            Assert.That(LanguageConstants.Goblin, Is.EqualTo("Goblin"));
        }
        [Test]
        public void HalflingConstant()
        {
            Assert.That(LanguageConstants.Halfling, Is.EqualTo("Halfling"));
        }
        [Test]
        public void IgnanConstant()
        {
            Assert.That(LanguageConstants.Ignan, Is.EqualTo("Ignan"));
        }
        [Test]
        public void InfernalConstant()
        {
            Assert.That(LanguageConstants.Infernal, Is.EqualTo("Infernal"));
        }
        [Test]
        public void OrcConstant()
        {
            Assert.That(LanguageConstants.Orc, Is.EqualTo("Orc"));
        }
        [Test]
        public void SylvanConstant()
        {
            Assert.That(LanguageConstants.Sylvan, Is.EqualTo("Sylvan"));
        }
        [Test]
        public void TerranConstant()
        {
            Assert.That(LanguageConstants.Terran, Is.EqualTo("Terran"));
        }
        [Test]
        public void UndercommonConstant()
        {
            Assert.That(LanguageConstants.Undercommon, Is.EqualTo("Undercommon"));
        }

        [Test]
        public void GetLanguages()
        {
            var languages = LanguageConstants.GetLanguages();

            Assert.That(languages.Contains(LanguageConstants.Abyssal), Is.True, LanguageConstants.Abyssal);
            Assert.That(languages.Contains(LanguageConstants.Aquan), Is.True, LanguageConstants.Aquan);
            Assert.That(languages.Contains(LanguageConstants.Auran), Is.True, LanguageConstants.Auran);
            Assert.That(languages.Contains(LanguageConstants.Celestial), Is.True, LanguageConstants.Celestial);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True, LanguageConstants.Common);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.True, LanguageConstants.Draconic);
            Assert.That(languages.Contains(LanguageConstants.Druidic), Is.True, LanguageConstants.Druidic);
            Assert.That(languages.Contains(LanguageConstants.Dwarven), Is.True, LanguageConstants.Dwarven);
            Assert.That(languages.Contains(LanguageConstants.Elven), Is.True, LanguageConstants.Elven);
            Assert.That(languages.Contains(LanguageConstants.Giant), Is.True, LanguageConstants.Giant);
            Assert.That(languages.Contains(LanguageConstants.Gnoll), Is.True, LanguageConstants.Gnoll);
            Assert.That(languages.Contains(LanguageConstants.Gnome), Is.True, LanguageConstants.Gnome);
            Assert.That(languages.Contains(LanguageConstants.Goblin), Is.True, LanguageConstants.Goblin);
            Assert.That(languages.Contains(LanguageConstants.Halfling), Is.True, LanguageConstants.Halfling);
            Assert.That(languages.Contains(LanguageConstants.Ignan), Is.True, LanguageConstants.Ignan);
            Assert.That(languages.Contains(LanguageConstants.Infernal), Is.True, LanguageConstants.Infernal);
            Assert.That(languages.Contains(LanguageConstants.Orc), Is.True, LanguageConstants.Orc);
            Assert.That(languages.Contains(LanguageConstants.Sylvan), Is.True, LanguageConstants.Sylvan);
            Assert.That(languages.Contains(LanguageConstants.Terran), Is.True, LanguageConstants.Terran);
            Assert.That(languages.Contains(LanguageConstants.Undercommon), Is.True, LanguageConstants.Undercommon);
            Assert.That(languages.Count(), Is.EqualTo(20));
        }
    }
}