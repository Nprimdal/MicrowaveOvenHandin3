using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

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
        public void Test1()
        {
            light.TurnOn();

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Assert.That(stringWriter.ToString(), Is.EqualTo("Light is turned on"));
            //Arg.Is<string>(s => s.ToLower().Contains("on")
        }
    }
}