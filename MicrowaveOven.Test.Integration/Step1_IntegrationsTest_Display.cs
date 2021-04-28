using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Castle.Core.Internal;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    public class Step1_IntegrationsTest_Display
    {
        private IOutput output;
        private IDisplay display;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            display = new Display(output);
        }

        [Test]
        public void NoCall_NoOutputInConsoleExpected()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Assert.That(stringWriter.ToString().IsNullOrEmpty());

        }

        [Test]
        public void ShowTime_ZeroMinuteZeroSeconds_CorrectOutputInConsole()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.ShowTime(0, 0);

            Assert.That(stringWriter.ToString().ToLower().Contains("00:00"));
        }

        [TestCase(0,5)]
        [TestCase(0, 10)]
        [TestCase(0, 30)]
        [TestCase(0, 50)]
        public void ShowTime_ZeroMinuteSomeSeconds_CorrectOutputInConsole(int minutes, int seconds)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.ShowTime(minutes, seconds);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{minutes}") && stringWriter.ToString().ToLower().Contains($"{seconds}"));
        }

        [TestCase(5, 0)]
        [TestCase(10, 0)]
        [TestCase(30, 0)]
        [TestCase(50, 0)]
        public void ShowTime_SomeMinutesZeroSeconds_CorrectOutputInConsole(int minutes, int seconds)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.ShowTime(minutes, seconds);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{minutes}") && stringWriter.ToString().ToLower().Contains($"{seconds}"));
        }

        [TestCase(5, 8)]
        [TestCase(10, 20)]
        [TestCase(30, 30)]
        [TestCase(50, 40)]
        public void ShowTime_SomeMinutesSomeSeconds_CorrectOutputInConsole(int minutes, int seconds)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.ShowTime(minutes, seconds);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{minutes}") && stringWriter.ToString().ToLower().Contains($"{seconds}"));
        }

        [TestCase(0)]
        [TestCase(50)]
        [TestCase(200)]
        [TestCase(300)]
        public void ShowPower_DifferentValues_CorrectOutputInConsole(int power)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.ShowPower(power);

            Assert.That(stringWriter.ToString().ToLower().Contains($"{power}"));
        }

        [Test]
        public void Clear_ExpectedOutputInConsole_Correct()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.Clear();

            Assert.That(stringWriter.ToString().ToLower().Contains("cleared"));

        }


    }
}
