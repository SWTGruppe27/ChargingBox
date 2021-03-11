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
    }
}
