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
        public void StationControl_DoorOpen_DisplayConnectPhone()
        {
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() {_doorOpen = true});
            uutDisplay.Received(1).ConnectPhone();
        }

        [Test]
        public void StationControl_DoorClosed_DisplayScanRfid()
        {
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutDisplay.Received(1).ScanRfid();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_DisplayConnectionToChargerFailed()
        {
            uutChargeControl.IsConnected().Returns(false);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() {Id = 12});

            uutDisplay.Received(1).ConnectionToChargerFailed();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_DisplayChargingBoxLocked()
        {
            uutChargeControl.IsConnected().Returns(true); 
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDisplay.Received(1).ChargingBoxLocked();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_DoorLockedCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDoor.Received(1).LockDoor();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_StartChargeCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void StationControl_RfidDetectedBoxLocked_StopChargeCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() {Id = 12});

            uutChargeControl.Received(1).StopCharge();
        }

        [Test]
        public void StationControl_RfidDetectedBoxLocked_DoorUnlockdCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDoor.Received(1).UnlockDoor();
        }

        [Test]
        public void StationControl_RfidDetectedBoxLocked_DisplayRemovePhone()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDisplay.Received(1).RemovePhone();
        }

        [TestCase(11)]
        [TestCase(13)]
        public void StationControl_RfidDetectedBoxLocked_DisplayRfidWrong(int rfid)
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { _doorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = rfid });

            uutDisplay.Received(1).RfidError();
        }
    }
}