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
        public void StartCancelButtonPressed_StateSetTime_ExpectedResult_LightOn()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();

            light.Received(1).TurnOn();
        }

        [Test]
        public void StartCancelButtonPressed_StateSetTime_ExpectedResult_StartCooking()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();

            cookController.Received(1).StartCooking(50, 60);
        }

        [Test]
        public void StartCancelButtonPressed_StateSetTime_ExpectedResult_StartCookingCorrectly()
        {
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            timeButton.Press();
            timeButton.Press();

            startCancelButton.Press();

            cookController.Received(1).StartCooking(200, 120);
        }

        [Test]
        public void StartCancelButtonPressed2Times_StateSetTime_ExpectedResult_Stop()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();
            startCancelButton.Press();

            cookController.Received(1).Stop();
        }

        [Test]
        public void StartCancelButtonPressed2Times_StateSetTime_ExpectedResult_LightOff()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();
            startCancelButton.Press();

            light.Received(1).TurnOff();
        }

        [Test]
        public void StartCancelButtonPressed2Times_StateSetTime_ExpectedResult_DisplayCleared()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();
            startCancelButton.Press();

            display.Received(1).Clear();
        }



        [Test]
        public void timeButtonPressed_StateSetPower_ExpectedResult_DisplayShowTime()
        {
            powerButton.Press();
            // Now in SetPower
            timeButton.Press();

            display.Received(1).ShowTime(1,0);
        }

        [Test]
        public void timeButtonPressed4times_StateSetPower_ExpectedResult_DisplayShowTime()
        {
            powerButton.Press();
            // Now in SetPower
            timeButton.Press();
            timeButton.Press();
            timeButton.Press();
            timeButton.Press();

            display.Received(1).ShowTime(4, 0);
        }

        [Test]
        public void CookingIsDone_StateCooking_ExpectedResult_DisplayCleared()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();
            UI.CookingIsDone();

            display.Received(1).Clear();

        }

        [Test]
        public void CookingIsDone_StateCooking_ExpectedResult_LightTurnOff()
        {
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();
            UI.CookingIsDone();

            light.Received(1).TurnOff();

        }
    }
}

