using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Castle.Core.Internal;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    public class Step1_IntegrationTest_Powertube
    {


        private IOutput output;
        private IPowerTube powerTube;
       
        [SetUp]
        public void Setup()
        {
            output = new Output();
            powerTube = new PowerTube(output);
            
        }

        [Test]
        public void NoCall_WasOff_ExpectedOutputInConsole_StringEmpty()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Assert.That(stringWriter.ToString().IsNullOrEmpty());

        }


        [TestCase(50)]
        [TestCase(100)]
        [TestCase(400)]
        [TestCase(500)]
        [TestCase(700)]
        public void TurnOn_WasOff_ExpectedOutputInConsole_Power(int power)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            powerTube.TurnOn(power);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{power}"));
        }

        

        [Test]
        public void TurnOff_WasOn_ExpectedOutputInConsole_Off()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            powerTube.TurnOn(300);
            powerTube.TurnOff();

            Assert.That(stringWriter.ToString().ToLower().Contains("off"));
        }


        [Test]
        public void TurnOff_WasOff_ExpectedOutputInConsole_NoOutput()
        {

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            powerTube.TurnOff();

            Assert.That(stringWriter.ToString().IsNullOrEmpty());

        }

        








    }
}
