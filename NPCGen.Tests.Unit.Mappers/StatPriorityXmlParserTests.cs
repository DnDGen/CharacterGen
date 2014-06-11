﻿using System;
using System.IO;
using System.Linq;
using Moq;
using NPCGen.Mappers;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers
{
    [TestFixture]
    public class StatPriorityXmlParserTests
    {
        private IStatPriorityXmlParser parser;
        private Mock<IStreamLoader> mockStreamLoader;
        private const String filename = "StatPriorityXmlParserTests.xml";

        [SetUp]
        public void Setup()
        {
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadStream(filename)).Returns(GetStream());

            parser = new StatPriorityXmlParser(mockStreamLoader.Object);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            var objects = parser.Parse(filename);

            Assert.That(objects.Count(), Is.EqualTo(1));
            Assert.That(objects.ContainsKey("class name"), Is.True);

            var firstElement = objects["class name"];
            Assert.That(firstElement.FirstPriority, Is.EqualTo("strength"));
            Assert.That(firstElement.SecondPriority, Is.EqualTo("wisdom"));
        }

        private Stream GetStream()
        {
            return new FileStream(filename, FileMode.Open);
        }

        private void MakeXmlFile()
        {
            var content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><statPriorities><object><className>class name</className><first>strength</first><second>wisdom</second></object></statPriorities>";
            File.WriteAllText(filename, content);
        }
    }
}