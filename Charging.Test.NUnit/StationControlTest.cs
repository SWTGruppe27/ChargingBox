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
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() {DoorOpen = true});
            uutDisplay.Received(1).ConnectPhone();
        }

        [Test]
        public void StationControl_DoorClosed_DisplayScanRfid()
        {
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutDisplay.Received(1).ScanRfid();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_DisplayConnectionToChargerFailed()
        {
            uutChargeControl.IsConnected().Returns(false);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() {Id = 12});

            uutDisplay.Received(1).ConnectionToChargerFailed();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_DisplayChargingBoxLocked()
        {
            uutChargeControl.IsConnected().Returns(true); 
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDisplay.Received(1).ChargingBoxLocked();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_DoorLockedCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDoor.Received(1).LockDoor();
        }

        [Test]
        public void StationControl_RfidDetectedBoxAvailable_StartChargeCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void StationControl_RfidDetectedBoxLocked_StopChargeCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() {Id = 12});

            uutChargeControl.Received(1).StopCharge();
        }

        [Test]
        public void StationControl_RfidDetectedBoxLocked_DoorUnlockedCorrect()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDoor.Received(1).UnlockDoor();
        }

        [Test]
        public void StationControl_RfidDetectedBoxLocked_DisplayRemovePhone()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDisplay.Received(1).RemovePhone();
        }

        [TestCase(11)]
        [TestCase(13)]
        public void StationControl_RfidDetectedBoxLocked_DisplayRfidWrong(int rfid)
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = rfid });

            uutDisplay.Received(1).RfidError();
        }

        [Test]
        public void StationControl_DoorChangeState_DisplayChargeingBoxIsLocked()
        {
            uutChargeControl.IsConnected().Returns(true);
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = false });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = true });

            uutDisplay.Received(1).ChargeStationLockedNotAvalible();
        }

        [TestCase(true, StationControl.ChargingBoxState.DoorOpen)]
        [TestCase(false, StationControl.ChargingBoxState.Available)]
        public void StationControl_GetState_StateIsDoorOpen(bool doorState, StationControl.ChargingBoxState state)
        {
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = doorState });

            Assert.That(uut.GetChargingBoxState(), Is.EqualTo(state));
        }

        [Test]
        public void StationControl_GetDoor_DoorIsNotNull()
        {
            Assert.That(uut.Door, Is.Not.Null);
        }

        [Test]
        public void StationControl_GetRfidReader_RfidReaderIsNotNull()
        {
            Assert.That(uut.RfidReader, Is.Not.Null);
        }

        [Test]
        public void StationControl_RfidReaderDetected_DoorOpenState()
        {
            uutDoor.DoorChangedStateEvent += Raise.EventWith(new DoorChangedStateEventArgs() { DoorOpen = true });
            uutRfidReader.ReadIdEvent += Raise.EventWith(new ReadIdEventArgs() { Id = 12 });

            uutDisplay.Received(1).DoorIsOpen();
        }
    }
}