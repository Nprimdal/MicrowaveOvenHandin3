using System;
using System.IO;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using IOutput = Microwave.Classes.Interfaces.IOutput;

namespace MicrowaveOven.Test.Integration
{
    public class Step1_IntegrationTest_Light
    {
        private IOutput output;
        private ILight light;
        

        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            
        }

        [Test]
        public void NoCall_WasOff_ExpectedOutputInConsole_StringEmpty()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Assert.That(stringWriter.ToString().IsNullOrEmpty());

        }

        [Test]
        public void TurnOn_WasOff_ExpectedOutputInConsole_LightIsTurnedOn()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            light.TurnOn();


            Assert.That(stringWriter.ToString().ToLower().Contains("on") && stringWriter.ToString().ToLower().Contains("light"));

        }
        [Test]
        public void TurnOff_WasOn_ExpectedOutputInConsole_LightIsTurnedOff()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            light.TurnOn();
            light.TurnOff();

            Assert.That(stringWriter.ToString().ToLower().Contains("off") && stringWriter.ToString().ToLower().Contains("light"));
        }
        [Test]
        public void TurnOn_WasOn_ExpectedOutputInConsole_LightIsTurnedOn()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            light.TurnOn();
            light.TurnOn();

            Assert.That(stringWriter.ToString().ToLower().Contains("on") && stringWriter.ToString().ToLower().Contains("light"));
        }
        [Test]
        public void TurnOff_WasOff_ExpectedNoOutputInConsole()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            light.TurnOff();

            Assert.That(stringWriter.ToString().IsNullOrEmpty());
        }
    }
}