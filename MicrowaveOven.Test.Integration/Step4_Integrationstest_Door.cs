using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    public class Step4_Integrationstest_Door
    {
        private IDoor door;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IUserInterface UI;
        private ILight light;
        private IDisplay display;
        private ICookController cookController;


        [SetUp]
        public void Setup()
        {
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            cookController = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();
            door = new Door();

            UI = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);

        }

        [Test]
        public void DoorOpened_StateSetPower_ExpectedResultDisplayCleared()
        {
            powerButton.Press();
            door.Open();

            display.Received(1).Clear();
        }
    }
}
