using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    public class Step2_IntegrationTest_CookController
    {

        private IOutput output;
        private IPowerTube powerTube;
        private IDisplay display;
        private ITimer timer;

        private CookController cookController;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            powerTube = new PowerTube(output);
            display = new Display(output);
            timer = Substitute.For<ITimer>();

            cookController = new CookController(timer, display, powerTube);
        }
        
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(595)]
        [TestCase(700)]
        public void StartCooking_ValidParameters_PowerTubeStarted(int power)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            cookController.StartCooking(power, 60);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{power}"));
        }
    }
}
