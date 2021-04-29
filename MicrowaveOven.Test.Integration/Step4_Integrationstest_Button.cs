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
    class Step4_Integrationstest_Button
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
        public void PowerButtonNotPressed_ExpectedResultNoCall()
        {
            display.DidNotReceive().ShowPower(50);

        }

        [Test]
        public void PowerButtonPressed_ExpectedResult_default()
        {
            powerButton.Press();
            display.Received(1).ShowPower(Arg.Is<int>(50));

        }

        [Test]
        public void PowerButtonPressed_3Times_ExpectedResult_PowerIs150()
        {
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            display.Received(1).ShowPower(Arg.Is<int>(150));
        }

        [Test]
        public void PowerButtonPressed_14Times_PowerIs700()
        {
            for (int i = 1; i <= 14; i++)
            {
                powerButton.Press();
            }
            display.Received(1).ShowPower(Arg.Is<int>(700));
        }


        [Test]
        public void PowerButtonPressed_16Times_PowerIs100()
        {
            for (int i = 1; i <= 16; i++)
            {
                powerButton.Press();
            }
            display.Received(2).ShowPower(Arg.Is<int>(100));
        }

        [Test]
        public void StartCancelButtonPressed_StateSetPower_ExpectedResultDisplayCleared()
        {
            powerButton.Press();
            startCancelButton.Press();

            display.Received(1).Clear();
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

    }
}
