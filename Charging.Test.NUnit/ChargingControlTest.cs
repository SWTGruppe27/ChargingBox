using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ChargingBox.Test.NUnit
{
    [TestFixture]
    class ChargingControlTest
    {
        private ChargeControl uut;
        private CurrentEventArgs _currentEvent = null;
        private IUsbCharger uutUsbCharger;
        private IDisplay uutDisplay;

        [SetUp]
        public void Setup()
        {
            uutUsbCharger = Substitute.For<IUsbCharger>();
            uutDisplay = Substitute.For<IDisplay>();
            uut = new ChargeControl(uutUsbCharger,uutDisplay);

            uutUsbCharger.CurrentValueEvent += (o, args) => { _currentEvent = args; };
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ChargerControl_IsConnected_CorrectValue(bool state)
        {
            uutUsbCharger.Connected.Returns(state);
            Assert.That(uut.IsConnected(), Is.EqualTo(state));
        }

        [Test]
        public void ChargeControl_StartCharge_Correct()
        {
            uut.StartCharge();
            uutUsbCharger.Received(1).StartCharge();
        }

        [Test]
        public void ChargeControl_StopCharge_Correct()
        {
            uut.StopCharge();
            uutUsbCharger.Received(1).StopCharge();
        }


    }
}
