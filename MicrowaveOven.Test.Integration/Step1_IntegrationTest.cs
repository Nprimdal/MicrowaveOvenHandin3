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
    public class Step1_IntegrationTest
    {
        private IOutput output;
        private ILight light;
        private IPowerTube powerTube;
        private IDisplay display;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            powerTube = new PowerTube(output);
            display = new Display(output);
        }

        [Test]
        public void TurnOn_WasOff_ExpectedOutputInConsole_LightIsTurnedOn()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            light.TurnOn();

            string expected = "Light is turned on\r\n"; //Arg.Is<string>(s => s.ToLower().Contains("on"));

            Assert.That(stringWriter.ToString().ToLower().Contains("on") && stringWriter.ToString().ToLower().Contains("light"));

        }
        [Test]
        public void TurnOff_WasOn_ExpectedOutputInConsole_LightIsTurnedOff()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            light.TurnOn();
            light.TurnOff();

            string expected = "Light is turned on\r\n"; //Arg.Is<string>(s => s.ToLower().Contains("on"));

            Assert.That(stringWriter.ToString().Contains("off"));
        }
        [Test]
        public void TurnOn_WasOn_ExpectedOutputInConsole_LightIsTurnedOn()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            light.TurnOn();
            light.TurnOn();

            string expected = "Light is turned on\r\n"; //Arg.Is<string>(s => s.ToLower().Contains("on"));

            Assert.That(stringWriter.ToString().Contains("on"));
        }
        [Test]
        public void TurnOff_WasOff_ExpectedNoOutputInConsole()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter); 
            ;
            light.TurnOff();

            string expected = "Light is turned on\r\n"; //Arg.Is<string>(s => s.ToLower().Contains("on"));

            Assert.That(stringWriter.ToString().IsNullOrEmpty());
        }
    }
}