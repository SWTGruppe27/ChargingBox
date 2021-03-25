using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public interface IDisplay
    {
        void ConnectPhone();
        void ScanRfid();
        void ConnectionToChargerFailed();
        void ChargingBoxLocked();
        void ChargeStationLockedNotAvalible();
        void RfidError();
        void RemovePhone();
        void DoorIsOpen();
        void PhoneFullCharge();
        void PhoneCharging();
        void PhoneChargingError();
    }
}
