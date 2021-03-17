using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChargingBox.Test.NUnit
{
    [TestFixture]
    class RfidReaderTest
    {
        private RfidReader uut;
        private ReadIdEventArgs _rfIdEventArgs = null;

        [SetUp]
        public void Setup()
        {
            uut = new RfidReader();
            uut.ReadIdEvent += (o, args) => { _rfIdEventArgs = args; };
        }

        [Test]
        public void RfidReader_EventFired()
        {
            uut.SetId(12);
            Assert.That(_rfIdEventArgs, Is.Not.Null);
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(0)]
        public void RfidReader_EventFired_CorrectValue(int id)
        {
            uut.SetId(id);
            Assert.That(_rfIdEventArgs.Id, Is.EqualTo(id));
        }

        [Test]
        public void NegativeId_ExceptionIsThrown()
        {
            Assert.That(() => uut.SetId(-10), Throws.TypeOf<RfidReader.NegativeIdException>());
        }
    }
}
