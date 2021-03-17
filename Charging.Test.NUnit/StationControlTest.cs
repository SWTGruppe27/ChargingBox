using NSubstitute;
using NUnit.Framework;

namespace ChargingBox.Test.NUnit
{
    [TestFixture]
    public class StationControlTest
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
            uutChargeControl = Substitute.For<IChargeControl>();
            uutDisplay = Substitute.For<IDisplay>();
            uutDoor = Substitute.For<IDoor>();
            uutRfidReader = Substitute.For<IRfidReader>();
            uutUsbCharger = Substitute.For<IUsbCharger>();

            uut = new StationControl(uutDoor, uutRfidReader, uutChargeControl, uutDisplay, uutChargeControl);
        }

        [Test]
        public void StationControl()
        {

        }
    }
}