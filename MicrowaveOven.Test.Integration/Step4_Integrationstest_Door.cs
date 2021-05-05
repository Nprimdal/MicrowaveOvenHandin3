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

        [Test]
        public void DoorOpened_StateSetPower_ExpectedResultLightTurnOn()
        {
            powerButton.Press();
            door.Open();

            light.Received(1).TurnOn();
        }

        [Test]
        public void DoorOpened_StateReady_ExpectedResultLightTurnOn()
        {
            door.Open();
            
            light.Received(1).TurnOn();
        }

        [Test]
        public void DoorOpened_StateSetTime_ExpectedResultDisplayCleared()
        {
            powerButton.Press();
            timeButton.Press();
            door.Open();

            display.Received(1).Clear();
        }



        [Test]
        public void DoorOpened_StateSetTime_ExpectedResultLightTurnOn()
        {
            powerButton.Press();
            timeButton.Press();
            door.Open();

            light.Received(1).TurnOn();
        }


        [Test]
        public void DoorOpened_StateCooking_ExpectedResultDisplayClear()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            door.Open();

            display.Received(1).Clear();
            
        }

        [Test]
        public void DoorOpened_StateCooking_ExpectedResultCookingStop()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            door.Open();

            cookController.Received(1).Stop();

        }

        [Test]
        public void Doorclosed_StateDoorOpen_ExpectedResultLightTurnOff()
        {
            door.Open();
            door.Close();

            light.Received(1).TurnOff();

        }


        [Test]
        public void Doorclosed_StateReady_ExpectedResultNoCall()
        {
            door.Close();

            light.DidNotReceive().TurnOff();

        }


        [Test]
        public void Doorclosed_StateReady_ExpectedResult2LightTurnOn()
        {
            door.Open();
            door.Close();
            door.Open();


            light.Received(2).TurnOn();

        }

    }
}
