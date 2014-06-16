using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Tables.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public class EmbeddedResourceStreamLoaderTests : IntegrationTests
    {
        [Inject]
        public IStreamLoader StreamLoader { get; set; }

        [Test]
        public void GetsFileIfItIsAnEmbeddedResource()
        {
            var table = Load("AlignmentGoodness.xml");

            for (var i = 1; i <= 20; i++)
                Assert.That(table[i], Is.EqualTo(AlignmentConstants.Good));

            for (var i = 21; i <= 50; i++)
                Assert.That(table[i], Is.EqualTo(AlignmentConstants.Neutral));

            for (var i = 51; i <= 100; i++)
                Assert.That(table[i], Is.EqualTo(AlignmentConstants.Evil));
        }

        private Dictionary<Int32, String> Load(String filename)
        {
            var table = new Dictionary<Int32, String>();
            var xmlDocument = new XmlDocument();

            using (var stream = StreamLoader.LoadFor(filename))
                xmlDocument.Load(stream);

            var objects = xmlDocument.DocumentElement.ChildNodes;
            foreach (XmlNode node in objects)
            {
                var lower = Convert.ToInt32(node.SelectSingleNode("lower").InnerText);
                var upper = Convert.ToInt32(node.SelectSingleNode("upper").InnerText);
                var content = node.SelectSingleNode("content").InnerText;

                for (var i = lower; i <= upper; i++)
                    table.Add(i, content);
            }

            return table;
        }

        [Test]
        public void ThrowErrorIfFileIsNotFormattedCorrectly()
        {
            Assert.That(() => StreamLoader.LoadFor("invalid filename"), Throws.ArgumentException.With.Message.EqualTo("\"invalid filename\" is not a valid file"));
        }

        [Test]
        public void ThrowErrorIfFileIsNotAnEmbeddedResource()
        {
            Assert.That(() => StreamLoader.LoadFor("invalid filename.xml"), Throws.InstanceOf<FileNotFoundException>().With.Message.EqualTo("invalid filename.xml"));
        }

        [Test]
        public void MatchWholeFileName()
        {
            Assert.That(() => StreamLoader.LoadFor("Goodness.xml"), Throws.InstanceOf<FileNotFoundException>().With.Message.EqualTo("Goodness.xml"));
        }
    }
}