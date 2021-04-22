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

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
            Console.SetOut(sw);
            string expected = "Light is turned on";
            Assert.AreEqual(expected, sw.ToString());
        }
    }
}