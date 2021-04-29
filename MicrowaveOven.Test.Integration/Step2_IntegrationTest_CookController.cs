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
        private IUserInterface UI;

        private CookController cookController;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            powerTube = new PowerTube(output);
            display = new Display(output);
            timer = Substitute.For<ITimer>();
            UI = Substitute.For<IUserInterface>();

            cookController = new CookController(timer, display, powerTube, UI);
        }
        
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(595)]
        [TestCase(700)]
        public void StartCooking_ParametersInRange_PowerTubeStarted(int power)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            cookController.StartCooking(power, 60);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{power}"));
        }
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(595)]
        [TestCase(700)]
        public void Cooking_TimerExpired_RaiseEvent_PowerTubeOff(int power)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            cookController.StartCooking(power, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            Assert.That(stringWriter.ToString().ToLower().Contains($"off")); 
        }

        [TestCase(50)]
        [TestCase(100)]
        [TestCase(595)]
        [TestCase(700)]
        public void StopCooking_Expected_PowerTubeOff(int power)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            cookController.StartCooking(power, 60);
            cookController.Stop();

            Assert.That(stringWriter.ToString().ToLower().Contains($"off"));
        }

       
       
        
        [TestCase(100)]
        [TestCase(178)]
        [TestCase(266)]
        [TestCase(532)]
        public void StartCooking_DifferentTimeRemaining_TimerTick_DisplayCalled(int timeRemaining)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            cookController.StartCooking(250, 60);

            timer.TimeRemaining.Returns(timeRemaining);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{timeRemaining/60}") && stringWriter.ToString().ToLower().Contains($"{timeRemaining%60}"));
        }
        
    }
}
