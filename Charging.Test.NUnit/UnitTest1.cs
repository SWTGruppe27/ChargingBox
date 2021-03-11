using NUnit.Framework;

namespace ChargingBox.Test.NUnit
{
    public class Tests
    {
        private StationControl uut;
        private IChargeControl uutChargeControl;
        private IDisplay uutDisplay;
        private IDoor uutDoor;
        private IRfidReader uutRfidReader;
        private IUsbCharger uutUsbCharger;  

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}