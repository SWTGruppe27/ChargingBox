using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ChargingBox.Test.NUnit
{
    [TestFixture]
    class DoorTest
    {
        private Door uut;
        private DoorChangedStateEventArgs _doorEventArgs = null;

        [SetUp]
        public void Setup()
        {
            uut = new Door();

            uut.DoorChangedStateEvent += (o, args) => { _doorEventArgs = args; };
        }


        [Test]
        public void DoorOpen_EventFired()
        {
            uut.ChangeDoorState(true);
            Assert.That(_doorEventArgs,Is.Not.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DoorOpen_EventFired_CorrectValue(bool state)
        {
            uut.ChangeDoorState(state);
            Assert.That(_doorEventArgs.DoorOpen, Is.EqualTo(state));
        }

        [Test]
        public void DoorOpen_UnlockDoor_False()
        {
            uut.UnlockDoor();
            Assert.That(uut.DoorLocked, Is.False);
        }

        [Test]
        public void DoorOpen_LockDoor_True()
        {
            uut.LockDoor();
            Assert.That(uut.DoorLocked, Is.True);
        }
    }
}
